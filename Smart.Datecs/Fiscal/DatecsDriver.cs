using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Smart.Datecs.Fiscal
{
    public class DatecsDriver : IDisposable
    {
        public PrinterConnection Connection { get; set; }
        public TcpSerialPort RS232 { get; set; }
        public Encoding Enc;

        private const char LineFeed = (char)10;
        private const char Tab = (char)9;
        private static byte SequenceNumber = 1;
        private Dictionary<PaymentType, string> PaymentMap = new Dictionary<PaymentType, string>();
        private Dictionary<TaxRate, string> TaxMap = new Dictionary<TaxRate, string>();

        public Action<string> OnErrorLog;
        public Action<string> OnDebugLog;

        public DatecsDriver()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.Enc = Encoding.GetEncoding(1251);

            //payments
            this.PaymentMap.Add(PaymentType.Cash, "P");
            this.PaymentMap.Add(PaymentType.DebitCard, "D");

            //tax rates
            this.TaxMap.Add(TaxRate.Percent0, "A");
            this.TaxMap.Add(TaxRate.Percent20, "B");
        }

        ~DatecsDriver()
        {
            if (RS232 != null)
            {
                try
                {
                    WriteDebug(" ~Driver порта " + Connection.HostUrl ?? Connection.HostUrl);
                    RS232.Dispose();
                    RS232 = null;
                }
                catch { }
            }
        }

        public bool Start()
        {
            if (RS232 != null && RS232.IsOpen) return true;

            if (RS232 != null && RS232.IsOpen)
                RS232.Close();

            if (string.IsNullOrWhiteSpace(Connection.HostUrl))
            {
                throw new ApplicationException("Няма конфигуриран IP/Host адрес на фискалния принтер!");
            }

            RS232 = new TcpSerialPort(Connection.HostUrl, Connection.HostPort);

            RS232.OnErrorLog = this.OnErrorLog;
            RS232.OnDebugLog = this.OnDebugLog;

            bool success = RS232.Open();
            if (!success)
            {
                RS232.Close();
                return false;
            }

            WriteDebug("Отворихме порта" + Connection.HostUrl);

            return success;
        }

        private void WriteError(string message)
        {
            OnErrorLog?.Invoke(message);
        }
        private void WriteDebug(string message)
        {
            OnDebugLog?.Invoke(message);
        }

        public bool Stop()
        {
            WriteDebug("Затворихме порта" + Connection.HostUrl);

            if (RS232 != null && RS232.IsOpen)
            {
                RS232.Close();
                return true;
            }

            return false;
        }

        private bool IsOpen()
        {
            return (RS232 != null && RS232.IsOpen);
        }

        #region Utility commands
        public CommandReply GetEIK()
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.Utility.GetEIK);

            RS232.Write(cmd, 0, cmd.Length);
            CommandReply reply = RS232.ReadCommand();

            return reply;
        }

        public CommandReply GetDateTime()
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.GetDateTime);

            RS232.Write(cmd, 0, cmd.Length);
            CommandReply reply = RS232.ReadCommand();

            return reply;

        }

        public CommandReply SetDateTime(DateTime dateTime)
        {
            if (!IsOpen())
                return CommandReply.Empty;

            string dateTimeString = dateTime.ToString("dd-MM-yy HH:mm:ss");
            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.SetDateTime, dateTimeString);

            RS232.Write(cmd, 0, cmd.Length);
            CommandReply reply = RS232.ReadCommand();

            return reply;
        }

        public CommandReply FeedPaper(int lines = 1)
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.Utility.FeedPaper, lines.ToString());

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply CutPaper()
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.Utility.CutPaper);

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply PrintDiagnosticInfo()
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.Utility.PrintDiagInfo);

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply GetDiagnosticInfo()
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.Utility.GetDiagInfo, Enc.GetBytes("*1"));

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply GetLastDocNumber()
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.Utility.GetLastDocNumber);

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply SetOperatorPassword(int operatorNumber, string oldPassword, string newPassword)
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.Utility.SetOperatorPassword,
                operatorNumber.ToString(),
                oldPassword,
                newPassword);

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply SetOperatorName(int operatorNumber, string operatorPassword, string name)
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = this.CreateMessage(Commands.Utility.SetOperatorName,
                operatorNumber.ToString(),
                operatorPassword,
                name);

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply ExecuteCommand(byte command, params byte[] data)
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = CreateMessage(command, data);
            //System.IO.File.WriteAllBytes("ff.bin", cmd);
            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }
        #endregion

        #region Fiscal commands
        public CommandReply PrintLastFiscalBond()
        {
            if (!IsOpen())
            {
                WriteError("Печат на последен фискален бон порта е затворен" + Connection.HostUrl);
                return CommandReply.Empty;
            }


            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.FiscalCommands.PrintLastFiscalBond);

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }
        public CommandReply OpenFiscalBond(int operatorNumber, string operatorPassword, int cashDeskNumber, string unp)
        {
            if (!IsOpen())
            {
                WriteError("Отваряне на бон порта е затворен" + Connection.HostUrl);
                return CommandReply.Empty;
            }

            var ops = new List<string>();
            ops.Add(operatorNumber.ToString());
            ops.Add(operatorPassword);
            ops.Add(cashDeskNumber.ToString());
            if (!string.IsNullOrWhiteSpace(unp))
            {
                ops.Add(unp);
            }

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.FiscalCommands.OpenFiscalBond, ops.ToArray());

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }
        public CommandReply CloseFiscalBond()
        {
            if (!IsOpen())
            {
                WriteError("Затваряне на бон порта е затворен" + Connection.HostUrl);
                return CommandReply.Empty;
            }

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.FiscalCommands.CloseFiscalBond);

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }
        public CommandReply CancelFiscalBond()
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.FiscalCommands.CancelFiscalBond);

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        /// <summary>
        /// Регистрира артикул към касовата бележка
        /// </summary>
        /// <param name="articleDescription">42 символа описание на артикула</param>
        /// <param name="price">Цена на артикула</param>
        /// <param name="quantity">Количество на артикула</param>
        /// <param name="unit">Мерна единица</param>
        /// <param name="taxRate">Процент отстъпка или надценка на артикула</param>
        public CommandReply FiscalRegisterArticle(string articleDescription, decimal price, decimal discountAmount = 0m, decimal quantity = 1.0m, string unit = "бр.", TaxRate taxRate = TaxRate.Percent0)
        {
            //L1	Текст до 42 байта съдържащ ред, описващ продажбата
            //TaxCd	Един байт съдържащ буквата показваща видът на данъка ('А', 'Б', 'В', ...).  
            //    Има ограничение зависещо от параметъра Enabled_Taxes, който се установява при задаването на данъчните ставки 
            //    в команда 83.
            //Price	Това е единичната цена и е до 8 значещи цифри. Ако ще правите войд - цената трябва да е със знак "-",т.е. по-малка 
            //    от нула.
            //Qwan	Параметър, задаващ количеството на стоката. По подразбиране е 1.000. 
            //    Дължина до 8 значещи цифри (не повече от 3 след десетичната точка). 
            //    Произведението Price*Qwan се закръгля от принтера до зададения брой десетични знаци и също не трябва да надхвърля 
            //    8 значещи цифри.

            if (!IsOpen())
            {
                WriteError("Регистриране на артикул порта е затворен" + Connection.HostUrl);
                return CommandReply.Empty;
            }

            if (articleDescription.Length > 41)
                articleDescription = articleDescription.Substring(0, 41);

            List<byte> data = new List<byte>();
            data.AddRange(this.Enc.GetBytes(articleDescription + DatecsDriver.Tab));//L1
            data.AddRange(this.Enc.GetBytes(this.TaxMap[taxRate]));//TaxCd
            data.AddRange(this.Enc.GetBytes(price.ToString("0.00", CultureInfo.InvariantCulture)));//Price
            data.AddRange(this.Enc.GetBytes("*" + quantity.ToString("0.000", CultureInfo.InvariantCulture)));//Qwan
            data.AddRange(this.Enc.GetBytes("#" + unit));
            if (discountAmount != 0)
            {
                data.AddRange(this.Enc.GetBytes(";" + discountAmount.ToString()));
            }

            byte[] cmd = this.CreateMessage(
                DatecsDriver.Commands.FiscalCommands.RegisterArticle,
                data.ToArray());

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }


        public CommandReply FiscalIntermediateAmount()
        {
            if (!IsOpen())
            {
                WriteError("FiscalIntermediateAmount порта е затворен" + Connection.HostUrl);
                return CommandReply.Empty;
            }

            byte[] cmd = this.CreateMessage(
                DatecsDriver.Commands.FiscalCommands.IntermediateAmount,
                this.Enc.GetBytes("11"));//1 - Print; 1 - ShowDisplay

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }


        /// <summary>
        /// Отпечатва тотала на отвореният фискален бон
        /// </summary>
        /// <param name="paidMode">Начин на плащане</param>
        /// <param name="total">Тотал за плащане</param>
        public CommandReply FiscalTotalAmount(PaymentType paymentType = PaymentType.Cash)
        {
            if (!IsOpen())
            {
                WriteError("Последна сума на бона порта е затворен" + Connection.HostUrl);
                return CommandReply.Empty;
            }

            List<byte> data = new List<byte>();
            data.AddRange(this.Enc.GetBytes(DatecsDriver.Tab + this.PaymentMap[paymentType]));

            byte[] cmd = this.CreateMessage(
                DatecsDriver.Commands.FiscalCommands.TotalAmount,
                data.ToArray());

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        /// <summary>
        /// Отпечатва свободен текст към фискалният бон
        /// </summary>
        /// <param name="text">46 символа текст</param>
        public CommandReply FiscalFreeText(string text)
        {
            if (!IsOpen())
            {
                WriteError("FiscalFreeText е затворен" + Connection.HostUrl);
                return CommandReply.Empty;
            }

            var chunks = DatecsDriver.Chunk(text, 46);
            foreach (var ch in chunks)
            {
                byte[] cmd = this.CreateMessage(
                    DatecsDriver.Commands.FiscalCommands.FreeText,
                    ch);

                RS232.Write(cmd, 0, cmd.Length);
                var reply = RS232.ReadCommand();
                if (reply.StatusBits.HasErrors)
                    return reply;
            }

            return CommandReply.Success;
        }

        public CommandReply FiscalBarcode(string barcode)
        {
            if (!IsOpen())
            {
                WriteError("FiscalBarcode е затворен" + Connection.HostUrl);
                return CommandReply.Empty;
            }

            byte[] cmd = this.CreateMessage(Commands.FiscalCommands.Barcode, "3;" + barcode);

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply StornoFiscalBond(int operatorNumber, string operatorPassword, int cashDeskNumber,
            string unp, StornoReason stornoReason, string docNum)
        {
            if (!IsOpen())
                return CommandReply.Empty;

            string reason = "E";
            if (stornoReason == StornoReason.ReturnExchange)
            {
                reason = "R";
            }
            else if (stornoReason == StornoReason.TaxReduction)
            {
                reason = "T";
            }

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.FiscalCommands.StornoBond,
                operatorNumber.ToString(),
                operatorPassword,
                cashDeskNumber.ToString(),
                unp,
                reason + docNum);

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply OpenStornoFiscalBond(int operatorNumber, string operatorPassword, int cashDeskNumber,
            string unp, string stornoUnp, StornoReason stornoReason, string docNum, DateTime docDateTime, string fmNum)
        {
            if (!IsOpen())
                return CommandReply.Empty;

            string reason = "E";
            if (stornoReason == StornoReason.ReturnExchange)
            {
                reason = "R";
            }
            else if (stornoReason == StornoReason.TaxReduction)
            {
                reason = "T";
            }

            var parameters = new List<string>();
            parameters.Add(operatorNumber.ToString());
            parameters.Add(operatorPassword);
            parameters.Add(cashDeskNumber.ToString());
            if (!string.IsNullOrWhiteSpace(unp))
            {
                parameters.Add(unp);
            }
            parameters.Add(reason + docNum);
            parameters.Add(stornoUnp);
            parameters.Add(docDateTime.ToString("ddMMyyHHmmss"));
            parameters.Add(fmNum);

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.FiscalCommands.StornoBond,
                parameters.ToArray());

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply TransferMoney(decimal money)
        {
            if (!IsOpen())
            {
                WriteError("TransferMoney е затворен" + Connection.HostUrl);
                return CommandReply.Empty;
            }

            money = decimal.Round(money, 2);

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.FiscalCommands.TransferMoney, money.ToString());

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }
        #endregion

        #region Reports
        /// <summary>
        /// Принтира дневен финансов отчет
        /// </summary>
        /// <param name="report">Тип на отчета</param>
        /// <param name="IsZReport">Дали е Z или X отчет (Z приключва и нулира касата)</param>
        public CommandReply ReportXZ(XZReport report = XZReport.XZReport, bool IsZReport = false)
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = this.CreateMessage(
                (byte)report,
                this.Enc.GetBytes((IsZReport ? "0" : "2")));

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply ReportFiscalMemory(DateTime fromDate, DateTime toDate, bool isDetailedReport)
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte command = (isDetailedReport ?
                DatecsDriver.Commands.Reports.DetailedMonthlyReport :
                DatecsDriver.Commands.Reports.ShortMonthlyReport);
            byte[] cmd = this.CreateMessage(command,
                fromDate.ToString("ddMMyy"),
                toDate.ToString("ddMMyy"));

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply KLENPrint(KLENDocType docType, DateTime fromDateTime, DateTime toDateTime)
        {
            if (!IsOpen())
                return CommandReply.Empty;

            byte[] cmd = this.CreateMessage(DatecsDriver.Commands.Reports.KLENCommand,
                "P",
                ">" + docType,
                fromDateTime.ToString("ddMMyyHHmmss"),
                toDateTime.ToString("ddMMyyHHmmss"));

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }

        public CommandReply KLENPrint(KLENDocType docType, int fromNumber, int toNumber = 0)
        {
            if (!IsOpen())
                return CommandReply.Empty;

            var ops = new List<string>();
            ops.Add("P");
            ops.Add(">#" + docType);
            ops.Add(fromNumber.ToString());
            if (toNumber != 0)
            {
                ops.Add(toNumber.ToString());
            }
            byte[] cmd = this.CreateMessage(Commands.Reports.KLENCommand, ops.ToArray());

            RS232.Write(cmd, 0, cmd.Length);
            return RS232.ReadCommand();
        }
        #endregion

        public byte[] CreateMessage(byte command, params string[] parameters)
        {
            string concat = string.Join<string>(",", parameters);
            byte[] data = this.Enc.GetBytes(concat);

            return this.CreateMessage(command, data);
        }

        public byte[] CreateMessage(byte command, byte[] data)
        {
            byte length = (byte)(4 + data.Length + 32);

            List<byte> cmd = new List<byte>();
            cmd.Add(1);//<01> preamble
            cmd.Add(length);//<LEN> дължина на командата
            if (SequenceNumber == 224)
            {
                SequenceNumber = 0;
            }
            cmd.Add((byte)(32 + SequenceNumber++));//<SEQ> пореден номер на съобщението
            cmd.Add(command);//<CMD> команда

            if (data.Length != 0)
                cmd.AddRange(data);//<DATA> параметри към командата

            cmd.Add(5);//<05> postamble

            byte[] hash = CalculateHash(cmd);
            cmd.AddRange(hash);//<BCC>
            cmd.Add(3);//<03>

            return cmd.ToArray();
        }

        private byte[] CalculateHash(List<byte> command)
        {
            ushort sum = 0;
            for (int i = 1; i < command.Count; i++)
                sum += command[i];

            byte[] hash = BitConverter.GetBytes(sum);
            Array.Reverse(hash, 0, hash.Length);

            string hex = BitConverter.ToString(hash).Replace("-", string.Empty);
            byte[] align = new byte[4];
            align[0] = (byte)(Convert.ToByte(hex[0].ToString(), 16) + 48);
            align[1] = (byte)(Convert.ToByte(hex[1].ToString(), 16) + 48);
            align[2] = (byte)(Convert.ToByte(hex[2].ToString(), 16) + 48);
            align[3] = (byte)(Convert.ToByte(hex[3].ToString(), 16) + 48);

            return align;
        }

        public static List<string> Chunk(string str, int chunkSize)
        {
            if (str.Length <= chunkSize)
            {
                return new List<string>(new string[1] { str });
            }

            var list = new List<string>();
            int pos = 0;
            while (pos < str.Length)
            {
                char[] chars = str.Skip(pos).Take(chunkSize).ToArray();
                pos += chars.Length;
                list.Add(new string(chars));
            }

            list.RemoveAll(p => string.IsNullOrWhiteSpace(p));
            return list;
        }

        public void Dispose()
        {
            try
            {
                WriteDebug("Dispose на порта" + Connection.HostUrl);


                RS232.Dispose();
                RS232 = null;
            }
            catch { }
        }


        public class Commands
        {
            public class Utility
            {
                public const byte GetEIK = 99;
                public const byte FeedPaper = 44;
                public const byte CutPaper = 45;
                public const byte PrintDiagInfo = 71;
                public const byte GetDiagInfo = 90;
                public const byte GetLastDocNumber = 113;

                public const byte SetOperatorPassword = 101;
                public const byte SetOperatorName = 102;
            }

            public class FiscalCommands
            {
                public const byte StornoBond = 46;
                public const byte PrintLastFiscalBond = 109;
                public const byte OpenFiscalBond = 48;
                public const byte CloseFiscalBond = 56;
                public const byte CancelFiscalBond = 60;
                public const byte RegisterArticle = 49;

                public const byte IntermediateAmount = 51;
                public const byte TotalAmount = 53;
                public const byte FreeText = 54;
                public const byte Barcode = 84;
                public const byte TransferMoney = 70;//Служебен внос/износ на пари
            }

            public class Reports
            {
                public const byte ShortMonthlyReport = 79;
                public const byte DetailedMonthlyReport = 94;
                public const byte KLENCommand = 119;
            }

            public const byte GetDateTime = 62;
            public const byte SetDateTime = 61;
        }


    }
}
