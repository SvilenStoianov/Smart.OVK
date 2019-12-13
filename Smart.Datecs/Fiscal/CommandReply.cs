using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Smart.Datecs.Fiscal
{
    public class CommandReply
    {
        public static readonly CommandReply Empty = new CommandReply(new byte[0]);
        public static readonly CommandReply Success = new CommandReply(new byte[] { 1, 52, 62, 56, 48, 49, 52, 57, 44, 48, 49, 52, 57, 4, 136, 128, 128, 236, 134, 154, 5, 48, 54, 48, 63, 3 });

        public CommandReply(byte[] cmd)
        {
            this.RawCommand = cmd;
        }

        public byte[] RawCommand { get; set; }

        //<01><LEN><SEQ><CMD><DATA><04><STATUS><05><BCC><03>

        /// <summary>
        /// Preamble &lt;01&gt;
        /// </summary>
        public byte Preamble { get { return this.RawCommand[0]; } }

        /// <summary>
        /// Length &lt;LEN&gt;
        /// </summary>
        public byte LEN { get { return (byte)(this.RawCommand[1] - 32); } }

        /// <summary>
        /// Sequence Number &lt;SEQ&gt;
        /// </summary>
        public byte SEQ { get { return (byte)(this.RawCommand[2] - 32); } }

        /// <summary>
        /// Command &lt;CMD&gt;
        /// </summary>
        public byte CMD { get { return this.RawCommand[3]; } }

        /// <summary>
        /// Data &lt;DATA&gt;
        /// </summary>
        public byte[] DATA
        {
            get
            {
                byte[] buffer = new byte[this.LEN - 11];
                Array.Copy(this.RawCommand, 4, buffer, 0, buffer.Length);

                return buffer;
            }
        }

        /// <summary>
        /// Separator &lt;04&gt;
        /// </summary>
        public byte Separator
        {
            get
            {
                return this.RawCommand[this.RawCommand.Length - 13];
            }
        }

        /// <summary>
        /// Status &lt;STATUS&gt;
        /// </summary>
        public byte[] STATUS
        {
            get
            {
                byte[] buffer = new byte[6];
                Array.Copy(this.RawCommand, this.RawCommand.Length - 12, buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// Postamble &lt;05&gt;
        /// </summary>
        public byte Postamble
        {
            get
            {
                return this.RawCommand[this.RawCommand.Length - 6];
            }
        }

        /// <summary>
        /// Checksum &lt;BCC&gt;
        /// </summary>
        public byte[] BCC
        {
            get
            {
                byte[] buffer = new byte[4];
                Array.Copy(this.RawCommand, this.RawCommand.Length - 5, buffer, 0, 4);
                return buffer;
            }
        }

        /// <summary>
        /// Terminator &lt;03&gt;
        /// </summary>
        public byte Terminator
        {
            get
            {
                return this.RawCommand[this.RawCommand.Length - 1];
            }
        }

        /// <summary>
        /// Парснат &lt;STATUS&gt;
        /// </summary>
        public StatusBits StatusBits
        {
            get
            {
                try
                {
                    return new StatusBits(this.STATUS);
                }
                catch (Exception ex)
                {
                    try
                    {
                        File.WriteAllText("datecs-error.txt",
                            string.Format("Неправилен StatusBits! Дата: {0} Грешка: {1}",
                                DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                                ex.ToString()));
                    }
                    catch { }
                    return StatusBits.Success;
                }
            }
        }


        public override string ToString()
        {
            return BitConverter.ToString(this.RawCommand).Replace("-", " ");
        }

        public virtual OperationResult ToOperationResult()
        {
            OperationResult res = new OperationResult();
            if (StatusBits.HasErrors)
                res.Errors.AddRange(StatusBits.GetErrors());
            if (StatusBits.HasWarnings)
                res.Warnings.AddRange(StatusBits.GetWarnings());

            return res;
        }

        public virtual OperationResult<T> ToOperationResult<T>()
        {
            return this.ToOperationResult<T>(default(T));
        }

        public virtual OperationResult<T> ToOperationResult<T>(T result)
        {
            OperationResult<T> res = new OperationResult<T>(result);
            if (StatusBits.HasErrors)
                res.Errors.AddRange(StatusBits.GetErrors());
            if (StatusBits.HasWarnings)
                res.Warnings.AddRange(StatusBits.GetWarnings());

            return res;
        }
    }

    public class StatusBits
    {
        public static readonly StatusBits Empty = new StatusBits(new byte[6] { 255, 255, 255, 255, 255, 255 });
        public static readonly StatusBits Success = new StatusBits(new byte[] { 136, 128, 128, 236, 134, 154 });
        public StatusBits() { }
        public StatusBits(byte[] data)
        {
            this.Data = data;
        }

        private byte[] _Data;
        public byte[] Data
        {
            get { return _Data; }
            set
            {
                if (_Data == value)
                    return;

                this.General = new StatusMain(value[0], value[1], value[2]);
                this.ConfigKeys = new StatusConfigKeys(value[3]);
                this.FiscalMemory = new StatusFiscalMemory(value[4], value[5]);
                this._Data = value;
            }
        }

        public StatusMain General { get; set; }
        public StatusConfigKeys ConfigKeys { get; set; }
        public StatusFiscalMemory FiscalMemory { get; set; }

        public bool HasErrors
        {
            get
            {
                return (General.LidOpen || General.PrintingMechanismError || General.ClockNotSet || General.InvalidCommand ||
                    General.SyntaxError || General.TaxTerminalNotResponding || General.BatteryLow ||
                    General.RAMIsReset || General.CannotExecuteCommandInCurrentState || General.FieldOverflow || General.KLENEnded ||
                    General.PaperEnded || FiscalMemory.FiscalMemoryFull || FiscalMemory.FiscalMemoryWriteError || FiscalMemory.FiscalMemoryReadError ||
                    FiscalMemory.FiscalMemoryLastEntryError || FiscalMemory.FiscalMemryReadOnlyMode);
            }
        }
        public bool HasWarnings
        {
            get
            {
                return (General.RAMIsCorrupt || General.NoClientDisplay || General.InternalBondOpen90DegreesPrint || General.KLENVeryNearEnd ||
                    General.InternalBondOpen || General.KLENNearEnd || General.FiscalBondOpen || General.PaperNearEnd ||
                    FiscalMemory.FiscalMemoryLow);
            }
        }

        public List<string> GetErrors()
        {
            List<string> errors = new List<string>();
            if (General.LidOpen)
                errors.Add("Отворен е капакът на принтера.");
            if (General.PrintingMechanismError)
                errors.Add("Механизмът на печатащото устройство има неизправност.");
            if (General.ClockNotSet)
                errors.Add("Часовникът не е установен.");
            if (General.InvalidCommand)
                errors.Add("Кодът на получената команда е невалиден.");
            if (General.SyntaxError)
                errors.Add("Получените данни имат синктактична грешка.");
            if (General.TaxTerminalNotResponding)
                errors.Add("Вграденият данъчен терминал не отговаря.");
            if (General.BatteryLow)
                errors.Add("Слаба батерия (Часовникът за реално време е в състояние RESET).");
            if (General.RAMIsReset)
                errors.Add("Извършено е зануляване на оперативната памет.");
            if (General.CannotExecuteCommandInCurrentState)
                errors.Add("Изпълнението на командата не е позволено в текущия фискален режим.");
            if (General.FieldOverflow)
                errors.Add("При изпълнение на командата се е полуило препълване на някои полета от сумите.");
            if (General.KLENEnded)
                errors.Add("Край на КЛЕН (по-малко от 1 MB от КЛЕН свободни).");
            if (General.PaperEnded)
                errors.Add("Свършила е хартията.");
            if (FiscalMemory.FiscalMemoryFull)
                errors.Add("Фискалната памет е пълна.");
            if (FiscalMemory.FiscalMemoryWriteError)
                errors.Add("Има грешка при запис във фискалната памет.");
            if (FiscalMemory.FiscalMemoryReadError)
                errors.Add("Грешка при четене от фискалната памет.");
            if (FiscalMemory.FiscalMemoryLastEntryError)
                errors.Add("Последният запис във фискалната памет не е успешен.");
            if (FiscalMemory.FiscalMemryReadOnlyMode)
                errors.Add("Фискалната памет е установена в режим READONLY (заключена).");

            return errors;
        }
        public List<string> GetWarnings()
        {
            List<string> warnings = new List<string>();
            if (General.RAMIsCorrupt)
                warnings.Add("Установено е разрушаване на съдържанието на оперативната памет (RAM) след включване.");
            if (General.NoClientDisplay)
                warnings.Add("Не е свързан клиентски дисплей.");
            if (General.InternalBondOpen90DegreesPrint)
                warnings.Add("Отворен е служебен бон за печат на завъртян на 90 градуса текст.");
            if (General.KLENVeryNearEnd)
                warnings.Add("Много близък край на КЛЕН (допускат се само определени бонове).");
            if (General.InternalBondOpen)
                warnings.Add("Отворен е служебен бон.");
            if (General.KLENNearEnd)
                warnings.Add("Близък край на КЛЕН (по-малко от 10 MB от КЛЕН свободни).");
            if (General.FiscalBondOpen)
                warnings.Add("Отворен е фискален бон.");
            if (General.PaperNearEnd)
                warnings.Add("Останала е малко хартия.");
            if (FiscalMemory.FiscalMemoryLow)
                warnings.Add("Има място за по-малко от 50 записа във ФП.");

            return warnings;
        }

        public class StatusMain
        {
            public StatusMain(byte byte0, byte byte1, byte byte2)
            {
                this.Byte0 = new BitArray(new byte[1] { byte0 });
                this.Byte1 = new BitArray(new byte[1] { byte1 });
                this.Byte2 = new BitArray(new byte[1] { byte2 });
            }

            public BitArray Byte0 { get; set; }
            public BitArray Byte1 { get; set; }
            public BitArray Byte2 { get; set; }

            #region Byte 0
            /// <summary>
            /// 0.6 Отворен е капакът на принтера
            /// </summary>
            public bool LidOpen
            {
                get
                {
                    return Byte0[6];
                }
            }

            /// <summary>
            /// 0.5 Обща грешка - това е OR на всички грешки, маркирани с ‘#’.
            /// </summary>
            public bool GeneralError
            {
                get
                {
                    return Byte0[5];
                }
            }

            /// <summary>
            /// 0.4# Механизмът на печатащото устройство има неизправност.
            /// </summary>
            public bool PrintingMechanismError
            {
                get
                {
                    return Byte0[4];
                }
            }

            /// <summary>
            /// 0.3 Не е свързан клиентски дисплей.
            /// </summary>
            public bool NoClientDisplay
            {
                get
                {
                    return Byte0[3];
                }
            }

            /// <summary>
            /// 0.2 Часовникът не е установен.
            /// </summary>
            public bool ClockNotSet
            {
                get
                {
                    return Byte0[2];
                }
            }

            /// <summary>
            /// 0.1# Кодът на получената команда е невалиден.
            /// </summary>
            public bool InvalidCommand
            {
                get
                {
                    return Byte0[1];
                }
            }

            /// <summary>
            /// 0.0# Получените данни имат синктактична грешка.
            /// </summary>
            public bool SyntaxError
            {
                get
                {
                    return Byte0[0];
                }
            }
            #endregion

            #region Byte 1
            /// <summary>
            /// 1.6 Вграденият данъчен терминал не отговаря.
            /// </summary>
            public bool TaxTerminalNotResponding
            {
                get
                {
                    return Byte1[6];
                }
            }

            /// <summary>
            /// 1.5 Отворен е служебен бон за печат на завъртян на 90 градуса текст.
            /// </summary>
            public bool InternalBondOpen90DegreesPrint
            {
                get
                {
                    return Byte1[5];
                }
            }

            /// <summary>
            /// 1.4# Установено е разрушаване на съдържанието на оперативната памет (RAM) след включване.
            /// </summary>
            public bool RAMIsCorrupt
            {
                get
                {
                    return Byte1[4];
                }
            }

            /// <summary>
            /// 1.3# Слаба батерия (Часовникът за реално време е в състояние RESET).
            /// </summary>
            public bool BatteryLow
            {
                get
                {
                    return Byte1[3];
                }
            }

            /// <summary>
            /// 1.2# Извършено е зануляване на оперативната памет.
            /// </summary>
            public bool RAMIsReset
            {
                get
                {
                    return Byte1[2];
                }
            }

            /// <summary>
            /// 1.1# Изпълнението на командата не е позволено в текущия фискален режим.
            /// </summary>
            public bool CannotExecuteCommandInCurrentState
            {
                get
                {
                    return Byte1[1];
                }
            }

            /// <summary>
            /// 1.0 При изпълнение на командата се е полуило препълване на някои полета от сумите.
            /// </summary>
            public bool FieldOverflow
            {
                get
                {
                    return Byte1[0];
                }
            }
            #endregion

            #region Byte 2
            /// <summary>
            /// 2.6 Много близък край на КЛЕН (допускат се само определени бонове).
            /// </summary>
            public bool KLENVeryNearEnd
            {
                get
                {
                    return Byte2[6];
                }
            }

            /// <summary>
            /// 2.5 Отворен е служебен бон.
            /// </summary>
            public bool InternalBondOpen
            {
                get
                {
                    return Byte2[5];
                }
            }

            /// <summary>
            /// 2.4 Близък край на КЛЕН (по-малко от 10 MB от КЛЕН свободни).
            /// </summary>
            public bool KLENNearEnd
            {
                get
                {
                    return Byte2[4];
                }
            }

            /// <summary>
            /// 2.3 Отворен е фискален бон.
            /// </summary>
            public bool FiscalBondOpen
            {
                get
                {
                    return Byte2[3];
                }
            }

            /// <summary>
            /// 2.2 Край на КЛЕН (по-малко от 1 MB от КЛЕН свободни).
            /// </summary>
            public bool KLENEnded
            {
                get
                {
                    return Byte2[2];
                }
            }

            /// <summary>
            /// 2.1 Останала е малко хартия.
            /// </summary>
            public bool PaperNearEnd
            {
                get
                {
                    return Byte2[1];
                }
            }

            /// <summary>
            /// 2.0 # Свършила е хартията. Ако се вдигне този флаг по време на команда, свързана с печат, 
            /// то командата е отхвърлена и не е променила състоянието на принтера.
            /// </summary>
            public bool PaperEnded
            {
                get
                {
                    return Byte2[0];
                }
            }
            #endregion
        }

        public class StatusConfigKeys
        {
            public StatusConfigKeys(byte byte3)
            {
                this.Byte3 = new BitArray(new byte[1] { byte3 });
            }

            public BitArray Byte3 { get; set; }

            #region Byte 3
            /// <summary>
            /// 3.6 Състояние на Sw7.
            /// </summary>
            public bool Sw7
            {
                get
                {
                    return Byte3[6];
                }
            }

            /// <summary>
            /// 3.5 Състояние на Sw6.
            /// </summary>
            public bool Sw6
            {
                get
                {
                    return Byte3[5];
                }
            }

            /// <summary>
            /// 3.4 Състояние на Sw5.
            /// </summary>
            public bool Sw5
            {
                get
                {
                    return Byte3[4];
                }
            }

            /// <summary>
            /// 3.3 Състояние на Sw4.
            /// </summary>
            public bool Sw4
            {
                get
                {
                    return Byte3[3];
                }
            }

            /// <summary>
            /// 3.2 Състояние на Sw3.
            /// </summary>
            public bool Sw3
            {
                get
                {
                    return Byte3[2];
                }
            }

            /// <summary>
            /// 3.1 Състояние на Sw2.
            /// </summary>
            public bool Sw2
            {
                get
                {
                    return Byte3[1];
                }
            }

            /// <summary>
            /// 3.0 Състояние на Sw1.
            /// </summary>
            public bool Sw1
            {
                get
                {
                    return Byte3[0];
                }
            }
            #endregion
        }

        public class StatusFiscalMemory
        {
            public StatusFiscalMemory(byte byte4, byte byte5)
            {
                this.Byte4 = new BitArray(new byte[1] { byte4 });
                this.Byte5 = new BitArray(new byte[1] { byte5 });
            }

            public BitArray Byte4 { get; set; }
            public BitArray Byte5 { get; set; }

            #region Byte 4
            /// <summary>
            /// 4.5 OR на всички грешки, маркирани с ‘*’ от байтове 4 и 5.
            /// </summary>
            public bool GeneralError
            {
                get
                {
                    return Byte4[4];
                }
            }

            /// <summary>
            /// 4.4* Фискалната памет е пълна.
            /// </summary>
            public bool FiscalMemoryFull
            {
                get
                {
                    return Byte4[4];
                }
            }

            /// <summary>
            /// 4.3 Има място за по-малко от 50 записа във ФП.
            /// </summary>
            public bool FiscalMemoryLow
            {
                get
                {
                    return Byte4[3];
                }
            }

            /// <summary>
            /// 4.2 Зададени са индивидуален номер на принтера и номер на фискалната памет.
            /// </summary>
            public bool ConfiguredPrinterNumberAndFiscalMemoryNumber
            {
                get
                {
                    return Byte4[2];
                }
            }

            /// <summary>
            /// 4.1 Зададен е ЕИК.
            /// </summary>
            public bool ConfiguredEIK
            {
                get
                {
                    return Byte4[1];
                }
            }

            /// <summary>
            /// 4.0* Има грешка при запис във фискалната памет.
            /// </summary>
            public bool FiscalMemoryWriteError
            {
                get
                {
                    return Byte4[0];
                }
            }
            #endregion

            #region Byte 5
            /// <summary>
            /// 5.5 Грешка при четене от фискалната памет.
            /// </summary>
            public bool FiscalMemoryReadError
            {
                get
                {
                    return Byte5[5];
                }
            }

            /// <summary>
            /// 5.4 Зададени са поне веднъж данъчните ставки.
            /// </summary>
            public bool ConfiguredTaxRate
            {
                get
                {
                    return Byte5[4];
                }
            }

            /// <summary>
            /// 5.3 Принтерът е във фискален режим.
            /// </summary>
            public bool PrinterInFiscalMode
            {
                get
                {
                    return Byte5[3];
                }
            }

            /// <summary>
            /// 5.2* Последният запис във фискалната памет не е успешен.
            /// </summary>
            public bool FiscalMemoryLastEntryError
            {
                get
                {
                    return Byte5[2];
                }
            }

            /// <summary>
            /// 5.1 Фискалната памет е форматирана.
            /// </summary>
            public bool FiscalMemoryIsFormatted
            {
                get
                {
                    return Byte5[1];
                }
            }

            /// <summary>
            /// 5.0* Фискалната памет е установена в режим READONLY (заключена).
            /// </summary>
            public bool FiscalMemryReadOnlyMode
            {
                get
                {
                    return Byte5[0];
                }
            }
            #endregion
        }
    }
}
