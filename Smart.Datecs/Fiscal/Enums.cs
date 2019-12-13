using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Datecs.Fiscal
{
    /// <summary>
    /// Вид на плащане
    /// </summary>
    public enum PaymentType
    {
        /// <summary>
        /// Служебен запис (В брой)
        /// </summary>
        Default = 1,

        /// <summary>
        /// В брой
        /// </summary>
        Cash = 1,

        /// <summary>
        /// Дебитна карта
        /// </summary>
        DebitCard = 4,
    }

    /// <summary>
    /// Данъчна група (ДДС)
    /// </summary>
    public enum TaxRate
    {
        /// <summary>
        /// 0% (без ДДС)
        /// </summary>
        Percent0 = 1,

        /// <summary>
        /// 20% (нормално ДДС)
        /// </summary>
        Percent20 = 2,
    }

    /// <summary>
    /// Причина за сторниране
    /// </summary>
    public enum StornoReason
    {
        None = 0,

        /// <summary>
        /// Операторска грешка
        /// </summary>
        OperatorError = 1,

        /// <summary>
        /// Връщане/замяна
        /// </summary>
        ReturnExchange = 2,

        /// <summary>
        /// Намаление на данъчната основа
        /// </summary>
        TaxReduction = 3
    }

    public enum XZReport
    {
        /// <summary>
        /// Дневен финансов отчет
        /// </summary>
        XZReport = 69,
        /// <summary>
        /// Разширен дневен финансов отчет (с разпечатка на артикулите)
        /// </summary>
        XZReportWithArticles = 108,
        /// <summary>
        /// Разширен дневен финансов отчет (с разпечатка на департаментите)
        /// </summary>
        XZReportWithDepartments = 117,
        /// <summary>
        /// Разширен дневен финансов отчет (с разпечатка на департаментите и артикулите)
        /// </summary>
        XZReportWithArticlesAndDepartments = 118,
    }

    public enum KLENDocType
    {
        /// <summary>
        /// Всички видове документи
        /// </summary>
        A,

        /// <summary>
        /// Фискални (клиентски) бонове
        /// </summary>
        F,

        /// <summary>
        /// Сторно (клиентски) бонове
        /// </summary>
        V,

        /// <summary>
        /// Анулирани (клиентски) бонове
        /// </summary>
        C,

        /// <summary>
        /// Служебни бонове
        /// </summary>
        N,

        /// <summary>
        /// Бонове от служебно въвеждане
        /// </summary>
        I,

        /// <summary>
        /// Бонове от служебно извеждане
        /// </summary>
        O,

        /// <summary>
        /// Служебни бонове със завъртян на 90 градуса печат
        /// </summary>
        R,

        /// <summary>
        /// Бонове от сервизни операции
        /// </summary>
        S,

        /// <summary>
        /// Отчети (само информация за дата/час и номер на бона)
        /// </summary>
        P,

        /// <summary>
        /// X-отчети
        /// </summary>
        X,

        /// <summary>
        /// Z-отчети
        /// </summary>
        Z
    }

    public enum ArticleType
    {
        /// <summary>
        /// Артикул
        /// </summary>
        Article = 0,

        /// <summary>
        /// Свободен текст
        /// </summary>
        FreeText = 1,

        /// <summary>
        /// Баркод
        /// </summary>
        Barcode = 2
    }
}