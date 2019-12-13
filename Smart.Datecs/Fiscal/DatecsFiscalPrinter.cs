using System;
using System.Globalization;

namespace Smart.Datecs.Fiscal
{
    public interface IFiscalPrinter : IDisposable
    {
        PrinterConnection Connection { get; set; }
        string EIK { get; set; }

        bool Start();
        bool Stop();

        #region Utility

        event EventHandler<ReportArg> OnErrorLog;
        event EventHandler<ReportArg> OnDebugLog;

        OperationResult<DateTime> GetDateTime();
        OperationResult SetDateTime(DateTime dateTime);
        OperationResult FeedPaper(int lines = 1);
        OperationResult CutPaper();
        OperationResult PrintDiagnosticInfo();
        OperationResult<DeviceInfo> GetDeviceInfo();
        OperationResult ConfigureOperator(Cashier cashier);
        #endregion

        #region Fiscal
        OperationResult<BondInfo> PrintFiscalBond(Bond fiscalBond);
        OperationResult PrintLastFiscalBond();
        OperationResult CancelFiscalBond();
        OperationResult<decimal> TransferMoney(decimal money);
        #endregion

        #region Reports
        /// <summary>
        /// Принтира дневен отчет (с или без приключаване на касата)
        /// </summary>
        /// <param name="completeDay">Дали да приключи касата (Z отчет)</param>
        /// <returns></returns>
        OperationResult ReportDaily(bool completeDay);
        /// <summary>
        /// Принтира месечен отчет за период от дата до дата
        /// </summary>
        /// <param name="fromDate">От дата</param>
        /// <param name="toDate">До дата</param>
        /// <param name="isDetailedReport">Дали отчета да бъде детайлен или съкратен</param>
        /// <returns></returns>
        OperationResult ReportFiscalMemory(DateTime fromDate, DateTime toDate, bool isDetailedReport);

        /// <summary>
        /// Печата дубликати на всички клиентски фискални бонове в периода
        /// </summary>
        /// <param name="fromDateTime">От дата и час</param>
        /// <param name="toDateTime">До дата и час</param>
        /// <returns></returns>
        OperationResult KLENPrint(DateTime fromDateTime, DateTime toDateTime);

        /// <summary>
        /// Печата дубликати на всички клиентски фискални бонове от номер до номер
        /// </summary>
        /// <param name="fromNumber">От номер</param>
        /// <param name="toNumber">Опционален до номер</param>
        /// <returns></returns>
        OperationResult KLENPrint(int fromNumber, int toNumber = 0);
        #endregion
    }

    public class ReportArg : EventArgs
    {
        public string Message { get; set; }
    }

    public class DatecsFiscalPrinter : IFiscalPrinter
    {
        public PrinterConnection Connection { get; set; }
        public string EIK { get; set; }
        public DatecsDriver Printer { get; set; }

        public event EventHandler<ReportArg> OnErrorLog;
        public event EventHandler<ReportArg> OnDebugLog;

        public DatecsFiscalPrinter()
        {
            this.Printer = new DatecsDriver();
            this.Printer.OnErrorLog += (msg) => { WriteError(msg); };
            this.Printer.OnDebugLog += (msg) => { WriteDebug(msg); };
        }

        private void WriteError(string message)
        {
            this.OnErrorLog?.Invoke(this, new ReportArg() { Message = message });
        }
        private void WriteDebug(string message)
        {
            this.OnDebugLog?.Invoke(this, new ReportArg() { Message = message });
        }

        public bool Start()
        {
            try
            {
                WriteDebug("Пускаме устройството!");
                Printer.Connection = this.Connection;
                return Printer.Start();
            }
            catch (Exception ex)
            {
                OnErrorLog?.Invoke(this, new ReportArg() { Message = ex.ToString() });
                return false;
            }
        }
        public bool Stop()
        {
            try
            {
                WriteDebug("Спираме устройството!");

                return Printer.Stop();
            }
            catch (Exception ex)
            {
                OnErrorLog?.Invoke(this, new ReportArg() { Message = ex.ToString() });
                return false;
            }
        }

        #region Utility
        public OperationResult<DateTime> GetDateTime()
        {
            var reply = Printer.GetDateTime();
            if (reply == CommandReply.Empty)
                return new OperationResult<DateTime>("Няма връзка към принтера GetDateTime");

            DateTime date = DateTime.MinValue;
            string dateTime = Printer.Enc.GetString(reply.DATA);
            if (!string.IsNullOrWhiteSpace(dateTime))
                date = DateTime.ParseExact(dateTime, "dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture);

            return reply.ToOperationResult<DateTime>(date);
        }

        public OperationResult SetDateTime(DateTime dateTime)
        {
            var reply = Printer.SetDateTime(dateTime);
            if (reply == CommandReply.Empty)
                return new OperationResult("Няма връзка към принтера SetDateTime");

            return reply.ToOperationResult();
        }

        public OperationResult FeedPaper(int lines = 1)
        {
            var reply = Printer.FeedPaper(lines);
            if (reply == CommandReply.Empty)
                return new OperationResult("Няма връзка към принтера FeedPaper");

            return reply.ToOperationResult();
        }

        public OperationResult CutPaper()
        {
            var reply = Printer.CutPaper();
            if (reply == CommandReply.Empty)
                return new OperationResult("Няма връзка към принтера CutPaper");

            return reply.ToOperationResult();
        }

        public OperationResult PrintDiagnosticInfo()
        {
            var reply = Printer.PrintDiagnosticInfo();
            if (reply == CommandReply.Empty)
                return new OperationResult("Няма връзка към принтера PrintDiagnosticInfo");

            return reply.ToOperationResult();
        }

        public OperationResult<DeviceInfo> GetDeviceInfo()
        {
            var reply = Printer.GetEIK();
            if (reply.StatusBits.HasErrors)
                return reply.ToOperationResult<DeviceInfo>();

            string cmdReply = Printer.Enc.GetString(reply.DATA);
            string[] temp = cmdReply.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (temp.Length != 2)
                return new OperationResult<DeviceInfo>();

            string eik = temp[0];

            reply = Printer.GetDiagnosticInfo();
            if (reply.StatusBits.HasErrors)
                return reply.ToOperationResult<DeviceInfo>();

            DeviceInfo info = new DeviceInfo(reply.DATA);
            info.EIK = eik;

            return new OperationResult<DeviceInfo>(info);
        }

        public OperationResult ConfigureOperator(Cashier cashier)
        {
            var reply = Printer.SetOperatorPassword(cashier.UserNumber, "0000", cashier.Password);
            if (reply.StatusBits.HasErrors)
                return reply.ToOperationResult();

            return Printer.SetOperatorName(cashier.UserNumber, cashier.Password, cashier.Username).ToOperationResult();
        }
        #endregion

        #region Fiscal
        public OperationResult<BondInfo> PrintFiscalBond(Bond fiscalBond)
        {
            var reply = Printer.GetDateTime();
            if (reply == CommandReply.Empty)
                return new OperationResult<BondInfo>("Няма връзка към принтера Грешка PrintFiscalBond!");
            if (reply.StatusBits.HasErrors)
                return reply.ToOperationResult<BondInfo>();

            //служебно анулиране на заседнал фискален бон (ако има такъв)
            if (reply.StatusBits.General.FiscalBondOpen)
            {
                reply = Printer.CancelFiscalBond();
                if (reply.StatusBits.HasErrors)
                    return reply.ToOperationResult<BondInfo>();
            }

            //фискален бон
            if (fiscalBond.StornoReason == StornoReason.None)
            {
                reply = Printer.OpenFiscalBond(
                    fiscalBond.Cashier.UserNumber,
                    fiscalBond.Cashier.Password,
                    fiscalBond.Cashier.CashDeskNumber,
                    fiscalBond.UNP);
            }
            //сторно бон
            else
            {
                reply = Printer.OpenStornoFiscalBond(
                    fiscalBond.Cashier.UserNumber,
                    fiscalBond.Cashier.Password,
                    fiscalBond.Cashier.CashDeskNumber,
                    fiscalBond.UNP, //и двете УНП-та са на фискалния бон
                    fiscalBond.UNP, //и двете УНП-та са на фискалния бон
                    fiscalBond.StornoReason,
                    fiscalBond.DocumentNumber,
                    fiscalBond.DocumentDateTime,
                    fiscalBond.FiscalMemoryNumber);
            }

            if (reply.StatusBits.HasErrors)
                return reply.ToOperationResult<BondInfo>();

            reply = Printer.FiscalFreeText("Каса: " + fiscalBond.Cashier.CashDeskNumber);
            if (reply.StatusBits.HasErrors)
                return reply.ToOperationResult<BondInfo>();
            reply = Printer.FiscalFreeText("Касиер: " + fiscalBond.Cashier.UserNumber + " " + fiscalBond.Cashier.FullName);
            if (reply.StatusBits.HasErrors)
                return reply.ToOperationResult<BondInfo>();

            //печатаме редовете
            foreach (var item in fiscalBond.Articles)
            {
                //артикул
                if (item.ArticleType == ArticleType.Article)
                {
                    string unit = "бр.";
                    if (!string.IsNullOrWhiteSpace(item.Unit))
                        unit = item.Unit;

                    reply = Printer.FiscalRegisterArticle(item.Name, item.Price, item.DiscountAmount, item.Quantity, unit, item.TaxRate);
                    if (reply.StatusBits.HasErrors)
                        return reply.ToOperationResult<BondInfo>();
                }
                //свободен текст
                else if (item.ArticleType == ArticleType.FreeText)
                {
                    reply = Printer.FiscalFreeText(item.Name);
                    if (reply.StatusBits.HasErrors)
                        return reply.ToOperationResult<BondInfo>();
                }
                //баркод
                else if (item.ArticleType == ArticleType.Barcode)
                {
                    reply = Printer.FiscalBarcode(item.Name);
                    if (reply.StatusBits.HasErrors)
                        return reply.ToOperationResult<BondInfo>();
                }
            }

            //payment
            reply = Printer.FiscalTotalAmount(fiscalBond.PaymentType);
            if (reply.StatusBits.HasErrors)
                return reply.ToOperationResult<BondInfo>();

            //finish
            reply = Printer.CloseFiscalBond();
            if (reply.StatusBits.HasErrors)
                return reply.ToOperationResult<BondInfo>();

            var info = new BondInfo();
            reply = Printer.GetLastDocNumber();
            if (reply.StatusBits.HasErrors)
                return reply.ToOperationResult(info);

            info.DocumentNumber = Printer.Enc.GetString(reply.DATA);

            return reply.ToOperationResult(info);
        }
        public OperationResult PrintLastFiscalBond()
        {
            return Printer.PrintLastFiscalBond().ToOperationResult();
        }
        public OperationResult CancelFiscalBond()
        {
            return Printer.CancelFiscalBond().ToOperationResult();
        }
        
        //Внос/износ на пари, връща наличността след изпълнение на командата
        public OperationResult<decimal> TransferMoney(decimal money)
        {
            var reply = Printer.TransferMoney(money);
            if (reply.StatusBits.HasErrors)
            {
                return reply.ToOperationResult<decimal>();
            }

            string res = Printer.Enc.GetString(reply.DATA);
            string[] tmp = res.Split(',');
            decimal nalichnost = 0;
            if (tmp[0] == "F")
            {
                return new OperationResult<decimal>("Заявката е отказана");
            }

            string nal = tmp[1].Substring(0, tmp[1].Length - 2);
            nal += "." + tmp[1].Substring(tmp[1].Length - 2, 2);

            decimal.TryParse(nal, out nalichnost);
            return reply.ToOperationResult<decimal>(nalichnost);
        }
        #endregion

        #region Reports
        /// <summary>
        /// Принтира дневен отчет (с или без приключаване на касата)
        /// </summary>
        /// <param name="completeDay">Дали да приключи касата (Z отчет)</param>
        /// <returns></returns>
        public OperationResult ReportDaily(bool completeDay)
        {
            return Printer.ReportXZ(XZReport.XZReportWithDepartments, completeDay).ToOperationResult();
        }

        /// <summary>
        /// Принтира месечен отчет за период от дата до дата
        /// </summary>
        /// <param name="fromDate">От дата</param>
        /// <param name="toDate">До дата</param>
        /// <param name="isDetailedReport">Дали отчета да бъде детайлен или съкратен</param>
        /// <returns></returns>
        public OperationResult ReportFiscalMemory(DateTime fromDate, DateTime toDate, bool isDetailedReport)
        {
            return Printer.ReportFiscalMemory(fromDate, toDate, isDetailedReport).ToOperationResult();
        }

        public OperationResult KLENPrint(DateTime fromDateTime, DateTime toDateTime)
        {
            return Printer.KLENPrint(KLENDocType.F, fromDateTime, toDateTime).ToOperationResult();
        }

        public OperationResult KLENPrint(int fromNumber, int toNumber = 0)
        {
            return Printer.KLENPrint(KLENDocType.F, fromNumber, toNumber).ToOperationResult();
        }
        #endregion

        public void Dispose()
        {
            try
            {
                WriteDebug("Dispose устройството!");
                Printer.Dispose();
            }
            catch { }
        }
    }
}
