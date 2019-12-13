using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Datecs.Fiscal
{
    /// <summary>
    /// Клас описващ всички данни необходими за създаването на фискален или служебен бон
    /// </summary>
    public class Bond
    {
        public Bond()
        {
            this.Cashier = new Cashier();
            this.Articles = new List<Article>();
        }

        public Bond(PaymentType paymentType, Cashier cashier, params Article[] details) : this()
        {
            this.PaymentType = paymentType;
            this.Cashier = cashier;

            if (details.Length > 0)
            {
                this.Articles.AddRange(details);
            }
        }

        /// <summary>
        /// Вид на плащането
        /// </summary>
        public PaymentType PaymentType { get; set; }

        /// <summary>
        /// Касиер/Оператор издал бона
        /// </summary>
        public Cashier Cashier { get; set; }

        /// <summary>
        /// Уникален номер на продажбата
        /// </summary>
        public string UNP { get; set; }

        /// <summary>
        /// Причина за сторнирането
        /// </summary>
        public StornoReason StornoReason { get; set; }

        /// <summary>
        /// Пореден номер на документа (от ляво на датата и часа)
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата и час на издаване
        /// </summary>
        public DateTime DocumentDateTime { get; set; }

        /// <summary>
        /// Номер на фискалната памет от която е издаден бона (дясния осем фицрен стринг ор предпоследния ред)
        /// </summary>
        public string FiscalMemoryNumber { get; set; }

        /// <summary>
        /// Редове за печат към фискалния бон
        /// </summary>
        public List<Article> Articles { get; set; }

        public void AddFreeText(string text = "")
        {
            this.Articles.Add(new Article() { ArticleType = ArticleType.FreeText, Name = text });
        }

        public void AddBarcode(string barcode)
        {
            this.Articles.Add(new Article() { ArticleType = ArticleType.Barcode, Name = barcode });
        }
    }

    /// <summary>
    /// Информация за разпечатан фискален бон
    /// </summary>
    public class BondInfo
    {
        /// <summary>
        /// Номер на документа
        /// </summary>
        public string DocumentNumber { get; set; }
    }

    /// <summary>
    /// Артикул/продажба към фискален бон
    /// </summary>
    public class Article
    {
        public ArticleType ArticleType { get; set; }

        /// <summary>
        /// Наименование на артикула
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество на артикула
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Мерна единица
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Цена на артикула (единична)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Данъчна група / процент ДДС
        /// </summary>
        public TaxRate TaxRate { get; set; }

        /// <summary>
        /// Отстъпка (стойност)
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Стойност на артикула (Количество * Цена)
        /// </summary>
        public decimal Total
        {
            get
            {
                return this.Quantity * this.Price;
            }
        }
    }

    /// <summary>
    /// Клас описващ модел за Касиер/Оператор
    /// </summary>
    public class Cashier
    {
        /// <summary>
        /// Име на касиер/оператор
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Номер на касиер/оператор
        /// </summary>
        public int UserNumber { get; set; }

        /// <summary>
        /// Потребител на касиер/оператор
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Парола на касиер/оператор
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Номер на касата
        /// </summary>
        public int CashDeskNumber { get; set; }
    }

    /// <summary>
    /// Информация за фискалния принтер (ЕИК, модел, сериен номер, номер на фискална памет и други)
    /// </summary>
    public class DeviceInfo
    {
        /// <summary>
        /// ЕИК на фискализираната фирма
        /// </summary>
        public string EIK { get; set; }

        /// <summary>
        /// FP-2000
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// BG
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// 1.00
        /// </summary>
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// 23NOV18 1000
        /// </summary>
        public DateTime FirmwareDateTime { get; set; }

        /// <summary>
        /// 6C0B
        /// </summary>
        public string FirmwareChecksum { get; set; }

        /// <summary>
        /// 02557242
        /// </summary>
        public string FiscalMemoryNumber { get; set; }

        /// <summary>
        /// DT557242
        /// </summary>
        public string SerialNumber { get; set; }

        public DeviceInfo()
        {

        }

        public DeviceInfo(byte[] deviceInfo)
        {
            //FP-2000,1.00BG 23NOV18 1000,6C0B,0101011100000000,DT557242,02557242
            string data = Encoding.GetEncoding(1251).GetString(deviceInfo);
            string[] tmp = data.Split(',');

            this.ModelName = tmp[0];
            string fw = tmp[1];
            this.FirmwareVersion = fw.Remove(4);
            this.CountryCode = fw.Substring(4, 2);
            fw = fw.Remove(0, 7);
            if (DateTime.TryParseExact(fw, "ddMMMyy HHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d))
            {
                this.FirmwareDateTime = d;
            }
            this.FirmwareChecksum = tmp[2];
            this.SerialNumber = tmp[4];
            this.FiscalMemoryNumber = tmp[5];
        }
    }
}
