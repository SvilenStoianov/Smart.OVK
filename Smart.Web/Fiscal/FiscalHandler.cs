using Newtonsoft.Json;
using Smart.Datecs.Fiscal;
using Smart.Dto;
using Smart.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Smart.Datecs;
using LinqToDB.Data;

namespace Smart.Web.Fiscal
{
    /// <summary>
    /// Хандлер за всички операции свързани с ФУ (печат на бон, X/Z отчети, печат от КЛЕН, служебно въвеждане/извеждане и други)
    /// </summary>
    public class FiscalHandler
    {
        private OVKDb Db;

        private MainLogger Log;

        public FiscalHandler(OVKDb db, MainLogger log)
        {
            this.Db = db;
            this.Log = log;
        }

        /// <summary>
        /// Разпечатва фискален/сторно бон по ID 
        /// </summary>
        [BindRequest(typeof(FiscalPrintBondRequest), typeof(FiscalPrintBondResponse))]
        public async Task<GatewayResult> FiscalPrint(FiscalPrintBondRequest req)
        {
            //проверка дали имаме конфигуриран фискален принтер
            var pocoFiscal = this.Db.Poco.s_printers
                .Where(p => p.printer_id == 1)
                .FirstOrDefault();
            if (pocoFiscal == null)
            {
                return GatewayResult.FromErrorMessage("Няма конфигуриран фискален принтер!");
            }

            //проверка дали имае такъв фискален бон
            var receipt = this.Db.Poco.s_cash_receipts
                .Where(p => p.receiptid == req.ReceiptID)
                .FirstOrDefault();
            if (receipt == null)
            {
                return GatewayResult.FromErrorMessage($"Няма фискален бон с ReceiptID {req.ReceiptID} !");
            }

            var recDetails = this.Db.Poco.s_cash_receipts_details
                .Where(p => p.receiptid == req.ReceiptID)
                .ToList();
            if (recDetails.Count == 0)
            {
                return GatewayResult.FromErrorMessage($"Няма редове към фискалния бон с ReceiptID {req.ReceiptID}!");
            }

            //вдигаме инстанция на принтера
            using (var printer = new DatecsFiscalPrinter())
            {
                //конфигурираме Host/Port и др.
                printer.Configure(pocoFiscal);

                printer.Start();

                //проверка дали имаме връзка с принтера
                var info = printer.GetDeviceInfo();
                if (!info.Success)
                {
                    return GatewayResult.FromErrorMessage(info.Message);
                }

                //проверка на ЕИК-а на принтера, ак осе разминават проблем
                if (info.Result.EIK != pocoFiscal.eik)
                {
                    string msg = $"Конфигурираният фискален принтер за това юридическо лице не отговаря на зададения в принтера:\r\n\r\nЛЗ ЕИК: {pocoFiscal.eik}\r\nПринтер: {info.Result.EIK}\r\n\r\nМоля, обърнете се към администратор!";
                    return GatewayResult.FromErrorMessage(msg);
                }

                //проверка на начина на плащане
                if (receipt.receiptpaymenttype < 1 || receipt.receiptpaymenttype > 2)
                {
                    return GatewayResult.FromErrorMessage($"Начина на плащане на фискалния бон е невалиден! 1 - \"В брой\", 2 - \"С карта\", ReceiptPaymentType: {receipt.receiptpaymenttype}");
                }
                var bond = new Bond();
                bond.PaymentType = (receipt.receiptpaymenttype == 1 ? PaymentType.Cash : PaymentType.DebitCard);
                bond.Cashier = new Cashier();
                bond.Cashier.FullName = receipt.cashier_fullname;//пълното име на касиера (Тошко Данчев Андреев)
                bond.Cashier.UserNumber = receipt.cashier_usernumber;//номер на касиера в принтера (обикновенно 1)
                bond.Cashier.Username = receipt.cashier_username;//потребителско име в принтера (обикновенно "ovk")
                bond.Cashier.Password = receipt.cashier_password;//парола за достъп за потребитле (обикновенно 0000)
                bond.Cashier.CashDeskNumber = receipt.vault_number;//номер на касата (обикновенно 1)

                //взимаме УНП-то
                var unp = this.Db.Poco.s_unps
                    .Where(p => p.unpid == receipt.unpid)
                    .FirstOrDefault();
                bond.UNP = unp.unp;
                bond.StornoReason = StornoReason.None;

                //ако е сторно бон сетваме и другите параметри
                //1 - Фискален бон, 2 - Сторно бон
                if (receipt.receipt_type == 2)
                {
                    if (string.IsNullOrWhiteSpace(receipt.storno_document_number))
                    {
                        return GatewayResult.FromErrorMessage("Липсва номера на документа който сторнираме! storno_document_number");
                    }
                    if (receipt.storno_document_datetime.HasValue == false)
                    {
                        return GatewayResult.FromErrorMessage("Липсва дата и час на разпечатване на документа който сторнираме! storno_document_datetime");
                    }
                    if (string.IsNullOrWhiteSpace(receipt.storno_fiscal_memory_number))
                    {
                        return GatewayResult.FromErrorMessage("Липсва номера на фискалната памет на ФУ от което е разпечатан документа който сторнираме! storno_fiscal_memory_number");
                    }
                    int reasonType = receipt.storno_reason_type.GetValueOrDefault();
                    if (reasonType < 1 || reasonType > 3)
                    {
                        return GatewayResult.FromErrorMessage("Липсва коректна причина за анулирането! storno_reason_type");
                    }

                    bond.StornoReason = (StornoReason)reasonType;
                    bond.DocumentNumber = receipt.storno_document_number;
                    bond.DocumentDateTime = receipt.storno_document_datetime.Value;
                    bond.FiscalMemoryNumber = receipt.storno_fiscal_memory_number;
                }

                //въртим детайлите/редовете на фискалния бон
                foreach (var d in recDetails)
                {
                    //Типа на реда; 1 - Артикул/Услуга, 2 - Свободен текст, 3 - Баркод
                    if (d.article_type == 1)
                    {
                        //Артикул
                        var det = new Article();
                        det.Name = d.article_name;//име на артикула/услугата
                        det.Quantity = d.quantity;//бройка
                        det.Unit = d.unit;//мерна единица, обикновенно бр.
                        det.Price = d.unit_price;//единична цена (за брой)
                        det.TaxRate = (TaxRate)(d.tax_rate);//данъчна група
                        det.DiscountAmount = d.discount_amount;//отстъпка - стойност за реда
                        bond.Articles.Add(det);
                    }
                    else if (d.article_type == 2)
                    {
                        //Свободен текст
                        bond.AddFreeText(d.article_name);
                    }
                    else if (d.article_type == 3)
                    {
                        //Баркод
                        bond.AddBarcode(d.article_name);
                    }
                    else
                    {
                        return GatewayResult.FromErrorMessage($"Непознат тип на реда: ReceiptID: {d.receiptid} ReceiptDetailID: {d.receipt_detail_id} ArticleType: {d.article_type}");
                    }
                }

                //печатаме бона и проверяваме дали е минал успешно
                var printResult = printer.PrintFiscalBond(bond);
                if (!printResult.Success)
                {
                    return printResult.ToGatewayResult();
                }

                DataConnectionTransaction tx = null;
                try
                {
                    tx = await this.Db.BeginTransactionAsync();
                    //запазваме данните на бона
                    receipt.isprint = 1;
                    receipt.receiptdatetime = DateTime.Now;//дата и час на разпечатване на бона
                    receipt.document_number = printResult.Result.DocumentNumber;//номер на фискалния бон
                    receipt.fiscal_memory_number = info.Result.FiscalMemoryNumber;//номер на фискалната памет където е записан бона
                    receipt.serial_number = info.Result.SerialNumber;//сериен номер на фискалния принтер който го е отпечатъл
                    await this.Db.UpdateAsync(receipt);//запазваме в базата
                    await tx.CommitAsync();
                }
                catch (Exception ex)
                {
                    await tx?.RollbackAsync();
                    return GatewayResult.FromErrorMessage(ex.ToString());
                }

                return GatewayResult.SuccessfulResult(new FiscalPrintBondResponse()
                {
                    ReceiptID = req.ReceiptID,
                    ReceiptDateTime = receipt.receiptdatetime,
                    DocumentNumber = receipt.document_number,
                    FiscalMemoryNumber = receipt.fiscal_memory_number,
                    SerialNumber = receipt.serial_number
                });
            }
        }

        /// <summary>
        /// Проверява състоянието на фискалното устройство
        /// </summary>
        [BindRequest(typeof(FiscalStatusRequest), typeof(FiscalStatusResponse))]
        public GatewayResult FiscalStatus()
        {
            //проверка дали имаме конфигуриран фискален принтер
            var pocoFiscal = this.Db.Poco.s_printers
                .Where(p => p.printer_id == 1)
                .FirstOrDefault();
            if (pocoFiscal == null)
            {
                return GatewayResult.FromErrorMessage("Няма конфигуриран фискален принтер!");
            }

            //проверка дали имаме настроен код и име на търговския обект както и код на работното място
            bool pointCode = string.IsNullOrWhiteSpace(pocoFiscal.sell_point_code);
            bool pointName = string.IsNullOrWhiteSpace(pocoFiscal.sell_point_name);
            bool workplaceCode = string.IsNullOrWhiteSpace(pocoFiscal.sell_workplace_code);
            if (pointCode || pointName || workplaceCode)
            {
                string msg = "";
                if (pointCode)
                {
                    msg += "Не е конфигуриран код на търговския обект (SellPointCode) !\r\n";
                }
                if (pointName)
                {
                    msg += "Не е конфигурирано име на търговския обект (SellPointName) !\r\n";
                }
                if (workplaceCode)
                {
                    msg += "Не е конфигуриран код на работното място (SellWorkplaceCode) !\r\n";
                }

                return GatewayResult.FromErrorMessage(msg);
            }

            //вдигаме инстанция на фискалния принтер и ще проверим състоянието му
            using (var printer = new DatecsFiscalPrinter())
            {
                //конфигуриране на принтера с Host/Port
                printer.Configure(pocoFiscal);

                //проверка за връзка/пинг
                if (!printer.Start())
                {
                    return GatewayResult.FromErrorMessage("Неуспешно свързване към фискалния принтер!");
                }

                //проверка на конфигурирани данни в принтера (ЕИК, дата и час, фирмуер и др.)
                var info = printer.GetDeviceInfo();
                if (!info.Success)
                {
                    return info.ToGatewayResult();
                }

                //проверка дали ЕИК-а съвпада с конфигурираното в базата
                if (info.Result.EIK != pocoFiscal.eik)
                {
                    string msg = $"Конфигурираният фискален принтер за това юридическо лице не отговаря на зададения в принтера:\r\n\r\nЛЗ ЕИК: {pocoFiscal.eik}\r\nПринтер: {info.Result.EIK}\r\n\r\nМоля, обърнете се към администратор!";
                    return GatewayResult.FromErrorMessage(msg);
                }

                //проверка на времето на принтера
                var dateTime = printer.GetDateTime();
                if (!dateTime.Success)
                {
                    return dateTime.ToGatewayResult();
                }

                //ако се разминава времето с повече от 10 секунди проблем
                var serverTime = DateTime.Now;
                var timeSpan = serverTime - dateTime.Result;
                if (timeSpan.Seconds > 10)
                {
                    return GatewayResult.FromErrorMessage("Моля, синхронизирайте часовника на фискалния принтер!\r\n" +
                        $"Сървърно време: {serverTime:dd.MM.yyyy HH:mm:ss}\r\n" +
                        $"ФУ време: {dateTime.Result:dd.MM.yyyy HH:mm:ss}");
                }

                //ако всичко е минало връщаме информация за принтера
                var result = new FiscalStatusResponse();
                result.EIK = info.Result.EIK;
                result.SerialNumber = info.Result.SerialNumber;
                result.SellPointCode = printer.Connection.SellPointCode;
                result.SellPointName = printer.Connection.SellPointName;
                result.SellWorkplaceCode = printer.Connection.SellWorkplaceCode;

                return GatewayResult.SuccessfulResult(result);
            }
        }

        /// <summary>
        /// Печат на дневен финансов отчет, X или Z отчет (с нулиране)
        /// </summary>
        [BindRequest(typeof(FiscalPrintDailyReportRequest), typeof(GatewayResult))]
        public GatewayResult FiscalDailyReport(FiscalPrintDailyReportRequest req)
        {
            //проверка дали имаме конфигуриран фискален принтер
            var pocoFiscal = this.Db.Poco.s_printers
                .Where(p => p.printer_id == 1)
                .FirstOrDefault();
            if (pocoFiscal == null)
            {
                return GatewayResult.FromErrorMessage("Няма конфигуриран фискален принтер!");
            }

            using (var printer = new DatecsFiscalPrinter())
            {
                printer.Configure(pocoFiscal);
                printer.Start();

                return printer.ReportDaily(req.IsZ).ToGatewayResult();
            }
        }

        /// <summary>
        /// Печат на фискалната дата (съкратено или детайлно)
        /// </summary>
        [BindRequest(typeof(FiscalPrintFiscalMemoryRequest), typeof(GatewayResult))]
        public GatewayResult FiscalMemoryReport(FiscalPrintFiscalMemoryRequest req)
        {
            //проверка дали имаме конфигуриран фискален принтер
            var pocoFiscal = this.Db.Poco.s_printers
                .Where(p => p.printer_id == 1)
                .FirstOrDefault();
            if (pocoFiscal == null)
            {
                return GatewayResult.FromErrorMessage("Няма конфигуриран фискален принтер!");
            }

            if (req.FromDate.Date > req.ToDate.Date)
            {
                return GatewayResult.FromErrorMessage("Въведената \"От дата\" не може да бъде след \"До дата\"!");
            }

            using (var printer = new DatecsFiscalPrinter())
            {
                printer.Configure(pocoFiscal);
                printer.Start();

                return printer.ReportFiscalMemory(req.FromDate.Date, req.ToDate.Date, req.IsDetailedReport).ToGatewayResult();
            }
        }

        /// <summary>
        /// Печатане на фискални бонове по период от КЛЕН
        /// </summary>
        [BindRequest(typeof(FiscalPrintKLENByDatesRequest), typeof(GatewayResult))]
        public GatewayResult PrintKLENByDates(FiscalPrintKLENByDatesRequest req)
        {
            //проверка дали имаме конфигуриран фискален принтер
            var pocoFiscal = this.Db.Poco.s_printers
                .Where(p => p.printer_id == 1)
                .FirstOrDefault();
            if (pocoFiscal == null)
            {
                return GatewayResult.FromErrorMessage("Няма конфигуриран фискален принтер!");
            }

            if (req.FromDateTime > req.ToDateTime)
            {
                return GatewayResult.FromErrorMessage("Въведената КЛЕН \"От дата\" не може да бъде след \"До дата\"!");
            }

            using (var printer = new DatecsFiscalPrinter())
            {
                printer.Configure(pocoFiscal);
                printer.Start();

                return printer.KLENPrint(req.FromDateTime, req.ToDateTime).ToGatewayResult();
            }
        }

        /// <summary>
        /// Печатане на фискални бонове по номера от КЛЕН
        /// </summary>
        [BindRequest(typeof(FiscalPrintKLENByNumbersRequest), typeof(GatewayResult))]
        public GatewayResult PrintKLENByNumbers(FiscalPrintKLENByNumbersRequest req)
        {
            //проверка дали имаме конфигуриран фискален принтер
            var pocoFiscal = this.Db.Poco.s_printers
                .Where(p => p.printer_id == 1)
                .FirstOrDefault();
            if (pocoFiscal == null)
            {
                return GatewayResult.FromErrorMessage("Няма конфигуриран фискален принтер!");
            }

            if (req.FromNumber == 0)
            {
                return GatewayResult.FromErrorMessage("Въведения КЛЕН \"От\" номер е невалиден!");
            }

            if (req.ToNumber.GetValueOrDefault() != 0 && req.ToNumber.Value < req.FromNumber)
            {
                return GatewayResult.FromErrorMessage("Въведения КЛЕН \"До\" номер е по-малък от \"От\" номер!");
            }

            using (var printer = new DatecsFiscalPrinter())
            {
                printer.Configure(pocoFiscal);
                printer.Start();

                return printer.KLENPrint(req.FromNumber, req.ToNumber.GetValueOrDefault()).ToGatewayResult();
            }
        }

        /// <summary>
        /// Печата дубликат на последния бон
        /// </summary>
        [BindRequest(typeof(FiscalPrintDuplicateRequest), typeof(GatewayResult))]
        public GatewayResult PrintDuplicate()
        {
            //проверка дали имаме конфигуриран фискален принтер
            var pocoFiscal = this.Db.Poco.s_printers
                .Where(p => p.printer_id == 1)
                .FirstOrDefault();
            if (pocoFiscal == null)
            {
                return GatewayResult.FromErrorMessage("Няма конфигуриран фискален принтер!");
            }

            using (var printer = new DatecsFiscalPrinter())
            {
                printer.Configure(pocoFiscal);
                printer.Start();

                return printer.PrintLastFiscalBond().ToGatewayResult();
            }
        }

        /// <summary>
        /// Внос/Износ на пари (наличност)
        /// </summary>
        [BindRequest(typeof(FiscalTransferMoneyRequest), typeof(FiscalTransferMoneyResponse))]
        public GatewayResult TransferMoney(FiscalTransferMoneyRequest req)
        {
            //проверка дали имаме конфигуриран фискален принтер
            var pocoFiscal = this.Db.Poco.s_printers
                .Where(p => p.printer_id == 1)
                .FirstOrDefault();
            if (pocoFiscal == null)
            {
                return GatewayResult.FromErrorMessage("Няма конфигуриран фискален принтер!");
            }

            if (req.Amount == 0m)
            {
                return GatewayResult.FromErrorMessage("Сумата на може да бъде 0!");
            }

            using (var printer = new DatecsFiscalPrinter())
            {
                printer.Configure(pocoFiscal);
                printer.Start();

                var result = printer.TransferMoney(req.Amount);
                printer.Stop();

                if (!result.Success)
                {
                    return result.ToGatewayResult();
                }

                return GatewayResult.SuccessfulResult(new FiscalTransferMoneyResponse() { Balance = result.Result });
            }
        }

        /// <summary>
        /// Синхронизира часовника на фискалния принтер с актуална дата и час от сървъра
        /// </summary>
        [BindRequest(typeof(FiscalSyncDateTimeRequest), typeof(GatewayResult))]
        public GatewayResult SyncDateTime()
        {
            //проверка дали имаме конфигуриран фискален принтер
            var pocoFiscal = this.Db.Poco.s_printers
                .Where(p => p.printer_id == 1)
                .FirstOrDefault();
            if (pocoFiscal == null)
            {
                return GatewayResult.FromErrorMessage("Няма конфигуриран фискален принтер!");
            }

            using (var printer = new DatecsFiscalPrinter())
            {
                printer.Configure(pocoFiscal);
                printer.Start();

                return printer.SetDateTime(DateTime.Now).ToGatewayResult();
            }
        }

        /// <summary>
        /// Конфигурира оператора/потребителя с който се печата
        /// </summary>
        [BindRequest(typeof(FiscalConfigureOperatorReqeust), typeof(GatewayResult))]
        public GatewayResult ConfigureOperator(FiscalConfigureOperatorReqeust req)
        {
            //проверка дали имаме конфигуриран фискален принтер
            var pocoFiscal = this.Db.Poco.s_printers
                .Where(p => p.printer_id == 1)
                .FirstOrDefault();
            if (pocoFiscal == null)
            {
                return GatewayResult.FromErrorMessage("Няма конфигуриран фискален принтер!");
            }

            using (var printer = new DatecsFiscalPrinter())
            {
                printer.Configure(pocoFiscal);
                printer.Start();

                var cashier = new Cashier();
                cashier.UserNumber = req.UserNumber;
                cashier.Username = req.Username;
                cashier.Password = req.Password;

                return printer.ConfigureOperator(cashier).ToGatewayResult();
            }
        }
    }

    public static class DatecsExtensions
    {
        /// <summary>
        /// Конфигурира фискалния принтер (Host и Port и SellPoint кодовете)
        /// </summary>
        public static void Configure(this DatecsFiscalPrinter printer, s_printers config)
        {
            printer.EIK = config.eik;
            printer.Connection = new Datecs.PrinterConnection()
            {
                HostUrl = config.host_url,
                HostPort = config.host_port,
                SellPointCode = config.sell_point_code,
                SellPointName = config.sell_point_name,
                SellWorkplaceCode = config.sell_workplace_code
            };
        }

        /// <summary>
        /// Преобразува OperationResult към GatewayResult, ако има грешки ги копира иначе празен GatewayResult
        /// </summary>
        public static GatewayResult ToGatewayResult(this OperationResult o)
        {
            if (o.Success)
            {
                return new GatewayResult();
            }

            return GatewayResult.FromErrorMessage(o.Message);
        }

        /// <summary>
        /// Преобразува OperationResult към GatewayResult, ако има грешки ги копира иначе празен GatewayResult
        /// </summary>
        public static GatewayResult ToGatewayResult<T>(this OperationResult<T> o)
        {
            if (o.Success)
            {
                return new GatewayResult();
            }

            return GatewayResult.FromErrorMessage(o.Message);
        }
    }
}
