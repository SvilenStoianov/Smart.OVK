using System;
using System.Collections.Generic;
using System.Text;

namespace Smart.Dto
{
    /// <summary>
    /// Заявка за разпечатване на фискален бон
    /// </summary>
    public class FiscalPrintBondRequest
    {
        /// <summary>
        /// ID на фискален/сторно бон (от s_cash_receipts)
        /// </summary>
        public int ReceiptID { get; set; }
    }

    public class FiscalPrintBondResponse
    {
        /// <summary>
        /// ID на фискален/сторно бон (от s_cash_receipts)
        /// </summary>
        public int ReceiptID { get; set; }

        /// <summary>
        /// Дата и час на опечатване
        /// </summary>
        public DateTime ReceiptDateTime { get; set; }

        /// <summary>
        /// Номер на отпечатания фискален/сторно бон
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Номер на фискаланта памет където е записан бона
        /// </summary>
        public string FiscalMemoryNumber { get; set; }

        /// <summary>
        /// Сериен номер на ФУ което е отпечатало бона
        /// </summary>
        public string SerialNumber { get; set; }
    }


    /// <summary>
    /// Проверка на състоянието на ФУ, ако не функционира по СУПТО не може да правим продажби
    /// </summary>
    public class FiscalStatusRequest
    {

    }

    public class FiscalStatusResponse
    {
        /// <summary>
        /// ЕИК регистрационен номер на фискализирана фирма
        /// </summary>
        public string EIK { get; set; }

        /// <summary>
        /// Индивидуален номер на ФУ (сериен номер)
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Код търговски обект
        /// </summary>
        public string SellPointCode { get; set; }

        /// <summary>
        /// Име на търговския обект
        /// </summary>
        public string SellPointName { get; set; }

        /// <summary>
        /// Код работно място
        /// </summary>
        public string SellWorkplaceCode { get; set; }
    }



    /// <summary>
    /// Печат на дневен финансов отчет, X или Z отчет (с нулиране)
    /// </summary>
    public class FiscalPrintDailyReportRequest
    {
        /// <summary>
        /// Дали е Z отчет (с нулиране на касата)
        /// </summary>
        public bool IsZ { get; set; }
    }



    /// <summary>
    /// Печат на фискалната дата (съкратено или детайлно)
    /// </summary>
    public class FiscalPrintFiscalMemoryRequest
    {
        /// <summary>
        /// От дата
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// До дата
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Дали да печата детайлно
        /// </summary>
        public bool IsDetailedReport { get; set; }
    }



    /// <summary>
    /// Печатане на фискални бонове по период от КЛЕН
    /// </summary>
    public class FiscalPrintKLENByDatesRequest
    {
        /// <summary>
        /// От дата
        /// </summary>
        public DateTime FromDateTime { get; set; }

        /// <summary>
        /// До дата
        /// </summary>
        public DateTime ToDateTime { get; set; }
    }



    /// <summary>
    /// Печатане на фискални бонове по номера от КЛЕН
    /// </summary>
    public class FiscalPrintKLENByNumbersRequest
    {
        /// <summary>
        /// От номер
        /// </summary>
        public int FromNumber { get; set; }

        /// <summary>
        /// До номер, опционален
        /// </summary>
        public int? ToNumber { get; set; }
    }



    /// <summary>
    /// Печата дубликат на последния бон
    /// </summary>
    public class FiscalPrintDuplicateRequest
    {

    }




    /// <summary>
    /// Внос/Износ на пари (наличност)
    /// </summary>
    public class FiscalTransferMoneyRequest
    {
        /// <summary>
        /// Сума за внасяне или изнасяне, при отрицателни стойности изнася пари
        /// </summary>
        public decimal Amount { get; set; }
    }

    /// <summary>
    /// Отговор на заявката за вност/износ на пари (наличност)
    /// </summary>
    public class FiscalTransferMoneyResponse
    {
        /// <summary>
        /// Текущ баланс на касата
        /// </summary>
        public decimal Balance { get; set; }
    }



    /// <summary>
    /// Синхронизира часовника на фискалния принтер с актуална дата и час от сървъра
    /// </summary>
    public class FiscalSyncDateTimeRequest
    {

    }




    /// <summary>
    /// Конфигурира оператора/потребителя с който се печата
    /// </summary>
    public class FiscalConfigureOperatorReqeust
    {
        public int UserNumber { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}