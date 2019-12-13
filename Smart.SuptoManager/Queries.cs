using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Smart.SuptoManager
{
    public enum ColumnType
    {
        Text,
        Date,
        DateTime,
        Time
    }

    public class ColumnMap
    {
        public string ColumnName { get; set; }

        public string DisplayName { get; set; }

        public ColumnType Type { get; set; }

        public ColumnMap(string name, string displayName, ColumnType type = ColumnType.Text)
        {
            this.ColumnName = name;
            this.DisplayName = displayName;
            this.Type = type;
        }
    }

    public class SuptoQueries
    {
        private static List<JQuery> Queries = new List<JQuery>();
        private static List<JQuery> Tables = new List<JQuery>();
        private static Dictionary<string, List<ColumnMap>> ColumnNames = new Dictionary<string, List<ColumnMap>>();

        static SuptoQueries()
        {
            InitSQL();
            InitTables();
            InitColumnNames();
        }

        public static JQuery GetQuery(string queryName)
        {
            return Queries.FirstOrDefault(p => p.ValueMember == queryName);
        }

        public static List<JQuery> GetQueries()
        {
            return Queries.ToList();
        }

        public static List<JQuery> GetTables()
        {
            return Tables.ToList();
        }

        public static List<ColumnMap> GetColumns(string queryName, DataTable data = null)
        {
            if (ColumnNames.ContainsKey(queryName))
            {
                return ColumnNames[queryName];
            }

            if (data != null)
            {
                return data.Columns.Cast<DataColumn>()
                     .Select(p => new ColumnMap(p.ColumnName, p.ColumnName, (p.DataType == typeof(DateTime) ? ColumnType.DateTime : ColumnType.Text)))
                     .ToList();
            }

            throw new ApplicationException("Няма така заявка! QueryName: " + queryName);
        }

        #region Columns
        private static void InitColumnNames()
        {
            #region Справки
            //1. Обобщени данни за продажбите
            var sales_summary = new List<ColumnMap>();
            ColumnNames["sales-summary"] = sales_summary;
            sales_summary.Add(new ColumnMap("unpid", "Първичен ключ"));
            sales_summary.Add(new ColumnMap("unp", "УНП – съгласно т. 9"));
            sales_summary.Add(new ColumnMap("sysnum", "системен номер на продажбата, присвоен от софтуера"));
            sales_summary.Add(new ColumnMap("sell_point_code", "код на търговски обект"));
            sales_summary.Add(new ColumnMap("sell_point_name", "наименование на търговски обект"));
            sales_summary.Add(new ColumnMap("open_date", "дата на откриване на продажбата", ColumnType.Date));
            sales_summary.Add(new ColumnMap("open_datetime", "време на откриване на продажбата (час, минута, секунда)", ColumnType.DateTime));
            sales_summary.Add(new ColumnMap("sell_workplace_code", "код на работно място"));
            sales_summary.Add(new ColumnMap("user_code", "код на оператор"));
            sales_summary.Add(new ColumnMap("sum_out_discount_out_vat_amount", "обща сума на продажбата – без ДДС и без отстъпка, в лв."));
            sales_summary.Add(new ColumnMap("sum_discountjamount", "отстъпка в лв."));
            sales_summary.Add(new ColumnMap("sum_vat_amount", "ДДС – сума – в лв."));
            sales_summary.Add(new ColumnMap("sum_jamount", "дължима сума по продажбата – в лв."));        
            sales_summary.Add(new ColumnMap("invoicenum", "фактура за продажбата – номер (ако е издадена фактура и в софтуера е налична информация)"));
            sales_summary.Add(new ColumnMap("invoicefdocdate", "фактура за продажбата – дата (ако е издадена фактура и в софтуера е налична информация)", ColumnType.Date));
            sales_summary.Add(new ColumnMap("close_date", "дата на приключване на продажбата", ColumnType.Date));
            sales_summary.Add(new ColumnMap("close_datetime", "време на приключване на продажбата (час, минута, секунда)", ColumnType.DateTime));
            sales_summary.Add(new ColumnMap("jpartner_code", "клиент код (при наличие на въведена информация)"));
            sales_summary.Add(new ColumnMap("jpartnername", "клиент име (при наличие на въведена информация)"));
            sales_summary.Add(new ColumnMap("usn_status", "статус на УНП-та"));

            //2. Данни за плащания по продажби
            var pay = new List<ColumnMap>();
            ColumnNames["payments"] = pay;
            pay.Add(new ColumnMap("unp", "УНП – съгласно т. 9"));
            pay.Add(new ColumnMap("sysnum", "системен номер на продажбата, присвоен от софтуера"));
            pay.Add(new ColumnMap("open_date", "дата на откриване на продажбата", ColumnType.Date));
            pay.Add(new ColumnMap("close_date", "дата на приключване на продажбата", ColumnType.Date));
            pay.Add(new ColumnMap("createdby_userid", "код на оператор, регистрирал плащането"));
            pay.Add(new ColumnMap("payamount", "обща сума по продажбата – в лв."));
            pay.Add(new ColumnMap("payment_date", "дата на плащане", ColumnType.Date));
            pay.Add(new ColumnMap("out_vat_amount", "платена сума без ДДС – в лв"));
            pay.Add(new ColumnMap("vat_amount", "ДДС – сума – в лв"));
            pay.Add(new ColumnMap("payment_type", "вид на плащането – съгласно номенклатурата в софтуера"));
            pay.Add(new ColumnMap("serial_number", "индивидуален номер на ФУ, на което е издаден ФБ за плащането"));

            //3. Детайлни данни за продажбите
            var sales_detailed = new List<ColumnMap>();
            ColumnNames["sales-detailed"] = sales_detailed;
            sales_detailed.Add(new ColumnMap("unp", "УНП – съгласно т. 9"));
            sales_detailed.Add(new ColumnMap("sysnum", "системен номер на продажбата, присвоен от софтуера"));
            sales_detailed.Add(new ColumnMap("product_ucode", "код на стоката/услугата"));
            sales_detailed.Add(new ColumnMap("product_name", "наименование на стоката/услугата"));
            sales_detailed.Add(new ColumnMap("jquantity", "количество"));
            sales_detailed.Add(new ColumnMap("out_discount_out_vat_unit_price", "единична цена (без отстъпка) – без ДДС, в лв."));
            sales_detailed.Add(new ColumnMap("discountjamount", "отстъпка (сума) – в лв."));
            sales_detailed.Add(new ColumnMap("vat_percent", "ДДС ставка"));
            sales_detailed.Add(new ColumnMap("vat_amount", "ДДС – сума, в лв."));
            sales_detailed.Add(new ColumnMap("jamount", "обща сума – в лв."));

            var advances = new List<ColumnMap>();
            ColumnNames["advances"] = advances;
            advances.Add(new ColumnMap("paymentid", "Уникален номер на приспадането"));
            advances.Add(new ColumnMap("distribution_datetime", "Дата и час на приспадането на аванса", ColumnType.DateTime));
            advances.Add(new ColumnMap("payment_datetime", "Дата и час на внасяне на аванса", ColumnType.DateTime));
            advances.Add(new ColumnMap("unp", "УНП на аванса"));
            advances.Add(new ColumnMap("payamount", "Сума на аванса"));
            advances.Add(new ColumnMap("junp", "УНП на задължението, което ще приспадаме с аванса"));
            advances.Add(new ColumnMap("ucode", "код на услугата / стоката"));
            advances.Add(new ColumnMap("product_name", "име на услугата / стоката"));
            advances.Add(new ColumnMap("jquantity", "количество"));
            advances.Add(new ColumnMap("measure_name", "мерна единица(бр., кг, опаковка и т.н.)"));
            advances.Add(new ColumnMap("jamount", "сума на задължението"));
            advances.Add(new ColumnMap("dist_amount", "приспадната сума"));
            advances.Add(new ColumnMap("createdby_userid", "код на потребителя създал записа"));

            //4. Сторнирани продажби
            var stornos = new List<ColumnMap>();
            ColumnNames["stornos"] = stornos;
            stornos.Add(new ColumnMap("unp", "УНП – съгласно т. 9"));
            stornos.Add(new ColumnMap("sysnum", "системен номер на продажбата, присвоен от софтуера"));
            stornos.Add(new ColumnMap("close_date", "дата на приключване на продажбата", ColumnType.Date));
            stornos.Add(new ColumnMap("close_datetime", "време на приключване на продажбата (час, минута, секунда)", ColumnType.DateTime));
            stornos.Add(new ColumnMap("ucode", "код на стоката/услугата"));
            stornos.Add(new ColumnMap("product_name", "наименование на стоката/услугата"));
            stornos.Add(new ColumnMap("jquantity", "количество"));
            stornos.Add(new ColumnMap("out_vat_unit_price", "единична цена (без отстъпка) – без ДДС, в лв."));
            stornos.Add(new ColumnMap("discountjamount", "отстъпка (сума) – в лв."));
            stornos.Add(new ColumnMap("vat_percent", "ДДС ставка"));
            stornos.Add(new ColumnMap("vat_amount", "ДДС – сума, в лв."));
            stornos.Add(new ColumnMap("jamount", "обща сума – в лв."));
            stornos.Add(new ColumnMap("receiptdatetime", "дата на сторниране на продажбата", ColumnType.Date));
            stornos.Add(new ColumnMap("receipttime", "време на сторниране на продажбата (час, мин., сек.)", ColumnType.Time));
            stornos.Add(new ColumnMap("receipt_serial_number", "индивидуален номер на ФУ, на което е издаден Сторно-ФБ"));
            stornos.Add(new ColumnMap("createdby_userid", "код на оператор, извършил сторнирането"));

            //5.Анулирани продажби
            var annulled = new List<ColumnMap>();
            ColumnNames["annulled"] = annulled;
            annulled.Add(new ColumnMap("unp", "УНП – съгласно т. 9"));
            annulled.Add(new ColumnMap("sysnum", "системен номер на продажбата, присвоен от софтуера"));
            annulled.Add(new ColumnMap("open_date", "дата на откриване на продажбата", ColumnType.Date));
            annulled.Add(new ColumnMap("open_datetime", "време на откриване на продажбата (час, минута, секунда)", ColumnType.DateTime));
            annulled.Add(new ColumnMap("product_ucode", "код на стоката/услугата"));
            annulled.Add(new ColumnMap("product_name", "наименование на стоката/услугата"));
            annulled.Add(new ColumnMap("jquantity", "количество"));
            annulled.Add(new ColumnMap("out_discount_out_vat_unit_price", "единична цена (без отстъпка) – без ДДС, в лв."));
            annulled.Add(new ColumnMap("discountjamount", "отстъпка (сума) – в лв."));
            annulled.Add(new ColumnMap("vat_percent", "ДДС ставка"));
            annulled.Add(new ColumnMap("vat_amount", "ДДС – сума, в лв."));
            annulled.Add(new ColumnMap("jamount", "обща сума – в лв."));
            annulled.Add(new ColumnMap("annul_date", "дата на анулиране на продажбата или на стоката/услугата", ColumnType.Date));
            annulled.Add(new ColumnMap("annul_datetime", "време на анулиране на продажбата или на стоката/услугата (час, мин., сек.)", ColumnType.DateTime));
            annulled.Add(new ColumnMap("annul_userid", "код на оператор, извършил анулирането"));

            //Обобщени данни за доставки
            var shop_in = new List<ColumnMap>();
            ColumnNames["shop_in"] = shop_in;
            shop_in.Add(new ColumnMap("in_document_id", "ID на запис"));
            shop_in.Add(new ColumnMap("delivery_date", "дата на доставка", ColumnType.Date));
            shop_in.Add(new ColumnMap("delivery_time", "време (час, минута, секунда)", ColumnType.Time));
            shop_in.Add(new ColumnMap("user_code", "код на оператор"));
            shop_in.Add(new ColumnMap("provider_code", "доставчик - код"));
            shop_in.Add(new ColumnMap("provider_name", "доставчик - име"));
            shop_in.Add(new ColumnMap("invoice_number", "фактура за доставка - №"));
            shop_in.Add(new ColumnMap("invoce_date", "фактура за доставка - дата"));
            shop_in.Add(new ColumnMap("totalAmountOutVAT", "обща сума на доставката (без отстъпка), без ДДС - в лв."));
            shop_in.Add(new ColumnMap("discountAmount", "отстъпка - в лв."));
            shop_in.Add(new ColumnMap("vatAmount", "ДДС - сума - в лв."));
            shop_in.Add(new ColumnMap("totalAmount", "обща сума - в лв."));
            shop_in.Add(new ColumnMap("payment_type", "вид на плащането - съгласно номенклатурата в софтуера"));

            var shop_in_detailed = new List<ColumnMap>();
            ColumnNames["shop_in_detailed"] = shop_in_detailed;
            shop_in_detailed.Add(new ColumnMap("in_document_id", "ID на запис"));
            shop_in_detailed.Add(new ColumnMap("product_code", "код на стоката/услугата"));
            shop_in_detailed.Add(new ColumnMap("product_name", "наименование на стоката/услугата"));
            shop_in_detailed.Add(new ColumnMap("quantity", "количество"));
            shop_in_detailed.Add(new ColumnMap("unit_price_out_discount", "единична цена (без отстъпка) - в лв."));
            shop_in_detailed.Add(new ColumnMap("discountAmount", "отстъпка (сума) - в лв."));
            shop_in_detailed.Add(new ColumnMap("vatAmount", "ДДС сума - в лв."));
            shop_in_detailed.Add(new ColumnMap("totalAmount", "обща сума - в лв."));

            var shop_movements = new List<ColumnMap>();
            ColumnNames["shop_movements"] = shop_movements;
            shop_movements.Add(new ColumnMap("product_code", "код на стоката"));
            shop_movements.Add(new ColumnMap("product_name", "наименование на стоката"));

            shop_movements.Add(new ColumnMap("sum_before_quantity", "количество в началото на периода"));
            shop_movements.Add(new ColumnMap("sum_before_amount", "обща стойност в началото на периода - в лв."));

            shop_movements.Add(new ColumnMap("sum_in_quantity", "дебитен оборот за периода - количество"));

            shop_movements.Add(new ColumnMap("sum_in_amount", "дебитен оборот за периода - стойност, в лв."));
            shop_movements.Add(new ColumnMap("sum_out_quantity", "кредитен оборот за периода - количество"));
            shop_movements.Add(new ColumnMap("sum_out_amount", "кредитен оборот за периода - стойност, в лв."));

            shop_movements.Add(new ColumnMap("sum_end_quantity", "количество в края на периода"));
            shop_movements.Add(new ColumnMap("sum_end_amount", "обща стойност в края на периода - в лв."));
            #endregion Справки


            #region Номенклатури
            //стоки/услуги – код, наименование, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране;
            var nomen_services = new List<ColumnMap>();
            ColumnNames["nomen-services"] = nomen_services;
            nomen_services.Add(new ColumnMap("ucode", "код"));
            nomen_services.Add(new ColumnMap("product_name", "наименование"));
            nomen_services.Add(new ColumnMap("from_date", "дата на първоначално конфигуриране в системата", ColumnType.Date));
            nomen_services.Add(new ColumnMap("lastupdatetime", "дата на последна промяна", ColumnType.DateTime));
            nomen_services.Add(new ColumnMap("to_date", "дата на деактивиране", ColumnType.Date));


            //клиенти – идентификатор (ЕИК, друг), имена, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране (ако софтуерът разполага с функционалност за въвеждане на клиенти);
            var nomen_clients = new List<ColumnMap>();
            ColumnNames["nomen-clients"] = nomen_clients;
            nomen_clients.Add(new ColumnMap("ID", "код"));
            nomen_clients.Add(new ColumnMap("egn", "идентификатор (ЕИК, друг)"));
            nomen_clients.Add(new ColumnMap("owner_name", "имена"));
            nomen_clients.Add(new ColumnMap("from_date", "дата на първоначално конфигуриране в системата", ColumnType.Date));
            nomen_clients.Add(new ColumnMap("lastupdatetime", "дата на последна промяна", ColumnType.DateTime));
            nomen_clients.Add(new ColumnMap("to_date", "дата на деактивиране", ColumnType.Date));

            //видове операции (действия) – код, наименование, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране;
            var nomen_oper_types = new List<ColumnMap>();
            ColumnNames["nomen-oper-types"] = nomen_oper_types;
            nomen_oper_types.Add(new ColumnMap("ucode", "код"));
            nomen_oper_types.Add(new ColumnMap("code_name", "наименование"));
            nomen_oper_types.Add(new ColumnMap("from_date", "дата на първоначално конфигуриране в системата", ColumnType.Date));
            nomen_oper_types.Add(new ColumnMap("lastupdatetime", "дата на последна промяна", ColumnType.DateTime));
            nomen_oper_types.Add(new ColumnMap("to_date", "дата на деактивиране", ColumnType.Date));

            //видове плащания – код, наименование, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране;
            var nomen_payment_types = new List<ColumnMap>();
            ColumnNames["nomen-payment-types"] = nomen_payment_types;
            nomen_payment_types.Add(new ColumnMap("ucode", "код"));
            nomen_payment_types.Add(new ColumnMap("code_name", "наименование"));
            nomen_payment_types.Add(new ColumnMap("from_date", "дата на първоначално конфигуриране в системата", ColumnType.Date));
            nomen_payment_types.Add(new ColumnMap("lastupdatetime", "дата на последна промяна", ColumnType.DateTime));
            nomen_payment_types.Add(new ColumnMap("to_date", "дата на деактивиране", ColumnType.Date));

            //търговски обекти – код, наименование, местонахождение, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране;
            var nomen_sell_points = new List<ColumnMap>();
            ColumnNames["nomen-sell-points"] = nomen_sell_points;
            nomen_sell_points.Add(new ColumnMap("ucode", "код"));
            nomen_sell_points.Add(new ColumnMap("code_name", "наименование"));
            nomen_sell_points.Add(new ColumnMap("acodenote", "местонахождение"));
            nomen_sell_points.Add(new ColumnMap("from_date", "дата на първоначално конфигуриране в системата", ColumnType.Date));
            nomen_sell_points.Add(new ColumnMap("lastupdatetime", "дата на последна промяна", ColumnType.Date));
            nomen_sell_points.Add(new ColumnMap("to_date", "дата на деактивиране", ColumnType.Date));

            //работни места – код, търговски обект, в който се намира, индивидуален номер на свързаното към него ФУ, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране;
            var nomen_workstations = new List<ColumnMap>();
            ColumnNames["nomen-workstations"] = nomen_workstations;
            nomen_workstations.Add(new ColumnMap("ucode", "код"));
            nomen_workstations.Add(new ColumnMap("sell_point_code", "търговски обект, в който се намира"));
            nomen_workstations.Add(new ColumnMap("serial_number", "индивидуален номер на свързаното към него ФУ"));
            nomen_workstations.Add(new ColumnMap("from_date", "дата на първоначално конфигуриране в системата", ColumnType.Date));
            nomen_workstations.Add(new ColumnMap("lastupdatetime", "дата на последна промяна", ColumnType.Date));
            nomen_workstations.Add(new ColumnMap("to_date", "дата на деактивиране", ColumnType.Date));

            //потребители (оператори) – уникален код в системата, имена по документ за самоличност, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране; присвоени роли и период на активност за всяка от тях, други въведени данни;
            var nomen_users = new List<ColumnMap>();
            ColumnNames["nomen-users"] = nomen_users;
            nomen_users.Add(new ColumnMap("ID", "уникален код в системата"));
            nomen_users.Add(new ColumnMap("full_name", "имена по документ за самоличност"));
            nomen_users.Add(new ColumnMap("from_date", "дата на първоначално конфигуриране в системата", ColumnType.Date));
            nomen_users.Add(new ColumnMap("lastupdatetime", "дата на последна промяна", ColumnType.DateTime));
            nomen_users.Add(new ColumnMap("to_date", "дата на деактивиране", ColumnType.Date));
            nomen_users.Add(new ColumnMap("has_access", "достъп"));
            nomen_users.Add(new ColumnMap("user_privileges", "присвоени права и период на активност за всяка от тях"));


            //права, присвоявани на ролите – код, наименование, описание, дата на конфигуриране/деактивиране.
            var nomen_user_privileges = new List<ColumnMap>();
            ColumnNames["nomen-user-privileges"] = nomen_user_privileges;
            nomen_user_privileges.Add(new ColumnMap("ucode", "код"));
            nomen_user_privileges.Add(new ColumnMap("code_name", "наименование"));
            nomen_user_privileges.Add(new ColumnMap("from_date", "дата на първоначално конфигуриране в системата", ColumnType.Date));
            nomen_user_privileges.Add(new ColumnMap("lastupdatetime", "дата на последна промяна", ColumnType.Date));
            nomen_user_privileges.Add(new ColumnMap("to_date", "дата на деактивиране", ColumnType.Date));


            //права, присвоявани на ролите – код, наименование, описание, дата на конфигуриране/деактивиране.
            var nomen_providers = new List<ColumnMap>();
            ColumnNames["nomen-suppliers"] = nomen_providers;
            nomen_providers.Add(new ColumnMap("ID", "код"));
            nomen_providers.Add(new ColumnMap("name", "наименование"));
            nomen_providers.Add(new ColumnMap("EIK", "ЕИК"));
            nomen_providers.Add(new ColumnMap("from_date", "дата на първоначално конфигуриране в системата", ColumnType.Date));
            nomen_providers.Add(new ColumnMap("lastupdatetime", "дата на последна промяна", ColumnType.Date));
            nomen_providers.Add(new ColumnMap("to_date", "дата на деактивиране", ColumnType.Date));

            #endregion Номенклатури
        }
        #endregion Columns

        #region SQL
        public static string CheckConnection()
        {
            return @"SELECT s.codeid FROM s_codes s WHERE s.codeid=1;";
        }

        private static void InitSQL()
        {
            #region Справки
            Queries.Add(new JQuery("sales-summary", "Обобщени данни за продажбите", @"
                SELECT s.unpid,
                       s.unp,
                       s.sysnum,
                       s.sell_point_code,
                       s.sell_point_name,
                       s.open_date,
                       s.open_datetime,
                       s.sell_workplace_code,
                       s.user_code,
                       SUM(s.out_discount_out_vat_amount) AS sum_out_discount_out_vat_amount,
                       SUM(s.discountjamount)             as sum_discountjamount,
                       SUM(s.vat_amount)                  AS sum_vat_amount,
                       SUM(s.jamount)                     as sum_jamount,                     
                       NULL as invoicenum,
                       NULL AS invoicefdocdate,
                       s.close_date,
                       s.close_datetime,
                       s.jpartner_code,
                       s.jpartnername,
                       s.usn_status
                FROM vs_orders s
                where s.unpid > 1
                  AND s.open_date >= @fromdate
                  AND s.open_date <= @todate
                  -- unp AND s.unp = @unp
                  -- sell_point_code AND s.sell_point_code = @sell_point_code
                  -- serial_number AND s.serial_number = @serial_number
                  -- sell_workplace_code AND s.sell_workplace_code = @sell_workplace_code
                  -- user_code AND s.user_code = @user_code
                group by s.unp, s.open_datetime
                order by s.open_datetime;"));


            Queries.Add(new JQuery("payments", "Данни за плащания по продажби", @"
                select sp.unp,
                       sp.sysnum,
                       sp.open_date,
                       sp.close_date,
                       sp.createdby_userid, 
                       sp.payamount,-- sum_jamount
                       sp.payment_date,
                       sp.out_vat_amount,
                       sp.vat_amount,
                       sp.payment_type,-- cashdeskname
                       sp.serial_number
                FROM vs_payments sp
                WHERE sp.pattern IN (1, 2)
                  -- unp AND sp.unp = @unp
                  -- sell_point_code AND sp.sell_point_code = @sell_point_code
                  -- serial_number AND sp.serial_number = @serial_number
                  -- sell_workplace_code AND sp.sell_workplace_code = @sell_workplace_code
                  -- user_code AND sp.createdby_userid = @user_code
                  AND sp.payment_date >= @fromdate
                  AND sp.payment_date <= @todate
                order by sp.payment_datetime;"));


            Queries.Add(new JQuery("sales-detailed", "Детайлни данни за продажбите", @"
                SELECT s.unpid,
                       s.unp,
                       s.sysnum,
                       s.product_ucode,
                       s.product_name,
                       s.jquantity,
                       s.out_discount_out_vat_unit_price,
                       s.discountjamount,
                       s.vat_percent,
                       s.vat_amount,
                       s.jamount
                FROM vs_orders s
                where s.unpid > 1
                  AND s.open_date >= @fromdate
                  AND s.open_date <= @todate
                  -- unp AND s.unp = @unp
                  -- sell_point_code AND s.sell_point_code = @sell_point_code
                  -- serial_number AND s.serial_number = @serial_number
                  -- sell_workplace_code AND s.sell_workplace_code = @sell_workplace_code
                  -- user_code AND s.user_code = @user_code
                group by s.jorderid, s.open_datetime
                order by s.open_datetime;"));


            Queries.Add(new JQuery("advances", "Данни за приспадане на аванси", @"
                SELECT paymentid,
                       distribution_datetime,
                       payment_datetime,
                       unp,
                       payamount,
                       junp,
                       ucode,
                       product_name,
                       jquantity,
                       measure_name,
                       jamount,
                       dist_amount,
                       createdby_userid
                FROM vs_advance_distributions s
                where s.unpid > 1
                  AND s.payment_datetime >= @fromdate
                  AND s.payment_datetime <= @todate
                  -- unp AND s.unp = @unp
                  -- user_code AND s.createdby_userid = @user_code
                order by s.payment_datetime;"));


            Queries.Add(new JQuery("stornos", "Сторнирани продажби", @"
                SELECT s.unp,
                       s.sysnum,
                       s.close_date,
                       s.close_datetime,
                       s.ucode,
                       s.product_name,
                       s.jquantity,
                       s.out_vat_unit_price,
                       s.discountjamount,
                       s.vat_percent,
                       s.vat_amount,
                       s.jamount,
                       s.payment_date,
                       s.receiptdatetime,
                       s.receiptdatetime as receipttime,
                       s.receipt_serial_number,
                       s.createdby_userid
                FROM vs_paydistributions s
                where s.pattern = 3
                  -- unp AND s.unp = @unp
                  -- sell_point_code AND s.sell_point_code = @sell_point_code
                  -- serial_number AND s.serial_number = @serial_number
                  -- sell_workplace_code AND s.sell_workplace_code = @sell_workplace_code
                  -- user_code AND s.createdby_userid = @user_code
                  AND s.payment_date >= @fromdate
                  AND s.payment_date <= @todate
                  AND s.receiptdatetime IS NOT NULL
                order by s.payment_datetime;"));

            Queries.Add(new JQuery("annulled", "Анулирани продажби", @"
                SELECT s.annul_jorder_id,
                       s.unp,
                       s.sysnum,
                       s.open_date,
                       s.open_datetime,
                       s.product_ucode,
                       s.product_name,
                       s.jquantity,
                       s.out_discount_out_vat_unit_price,
                       s.discountjamount,
                       s.vat_percent,
                       s.vat_amount,
                       s.jamount,
                       s.annul_date,
                       s.annul_datetime,
                       s.annul_userid
                FROM vs_orders_annulled s
                where s.unpid > 1
                  AND s.open_date >= @fromdate
                  AND s.open_date <= @todate
                  -- unp AND s.unp = @unp
                  -- sell_point_code AND s.sell_point_code = @sell_point_code
                  -- serial_number AND s.serial_number = @serial_number
                  -- sell_workplace_code AND s.sell_workplace_code = @sell_workplace_code
                  -- user_code AND s.annul_userid = @user_code
                order by s.annul_datetime;"));

            Queries.Add(new JQuery("shop_in", "Обобщени данни за доставки", @"
                select i.in_document_id,-- ID на запис;
                       i.delivery_date,-- дата на доставка;
                       i.delivery_time,-- време (час, минута, секунда);
                       i.user_code,-- код на оператор;
                       i.provider_code,-- доставчик - код;
                       i.provider_name,-- доставчик - име;
                       i.invoice_number,-- фактура за доставка - №;
                       i.invoce_date,-- фактура за доставка - дата;
                       SUM(i.unit_price_out_discount_out_vat * i.quantity)                   as totalAmountOutVAT,-- обща сума на доставката (без отстъпка), без ДДС - в лв.;
                       SUM(i.unit_price_discount * i.quantity) as discountAmount,-- отстъпка - в лв.;
                       SUM((i.unit_price_with_vat - i.unit_price_out_vat) * i.quantity)      as vatAmount,-- ДДС - сума - в лв.
                       SUM(i.unit_price_with_vat * i.quantity)                               as totalAmount,-- обща сума - в лв.;
                       i.payment_type -- вид на плащането - съгласно номенклатурата в софтуера.
                FROM vs_shop_in i
                WHERE i.delivery_date >= @fromdate
                  AND i.delivery_date <= @todate
                -- user_code AND i.user_code = @user_code
                group by i.in_document_id
                order by i.delivery_date;"));

            Queries.Add(new JQuery("shop_in_detailed", "Детайлни данни за доставки", @"
                SELECT i.in_document_id,-- ID на запис - съвпада с ID на запис от таблицата с обобщени данни за доставки;
                       i.product_code,-- код на стоката/услугата;
                       i.product_name,-- наименование на стоката/услугата;
                       i.quantity,-- количество;
                       i.unit_price_out_discount,-- единична цена (без отстъпка) - в лв.;
                       (i.unit_price_discount * i.quantity) as discountAmount, -- отстъпка (сума) - в лв.;
                       ((i.unit_price_with_vat - i.unit_price_out_vat) * i.quantity)      as vatAmount,-- ДДС сума - в лв.;
                       (i.unit_price_with_vat * i.quantity)                               as totalAmount -- обща сума - в лв.
                FROM vs_shop_in i
                WHERE i.delivery_date >= @fromdate
                  AND i.delivery_date <= @todate
                -- user_code AND i.user_code = @user_code
                order by i.delivery_date, i.delivery_time;"));

            Queries.Add(new JQuery("shop_movements", "Движение на стоки за период", @"CALL p_oborotka(@fromdate, @todate);"));
            #endregion Справки

            #region Номенклатури
            //стоки/услуги – код, наименование, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране;
            Queries.Add(new JQuery("nomen-services", "Ном. стоки/услуги", @"
                select s.productid,
                       s.ucode,
                       s.product_name,
                       s.from_date,
                       s.to_date,
                       s.lastupdatetime
                FROM vs_products s
                where s.productid>1
                  AND @fromdate <= s.to_date
                  AND @todate >= s.from_date
                order by s.pos;"));

            //доставчици – идентификатор (ЕИК, друг), имена, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране (ако софтуерът разполага с функционалност за въвеждане на доставчици);
            Queries.Add(new JQuery("nomen-suppliers", "Ном. доставчици", @"SELECT sp.ID,
                                                   sp.name,
                                                   sp.EIK,
                                                   sd.from_date,
                                                   sd.to_date,
                                                   sd.lastupdatetime
                                              FROM store_providers sp
                                            INNER JOIN s_durations sd ON sd.durationid=sp.durationid ;"));


            //клиенти – идентификатор (ЕИК, друг), имена, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране (ако софтуерът разполага с функционалност за въвеждане на клиенти);
            Queries.Add(new JQuery("nomen-clients", "Ном. клиенти(контрагенти)", @"
                select o.ID,
                       '' as egn,
                       o.owner_name,
                       sd.from_date,
                       sd.to_date,
                       sd.lastupdatetime
                FROM owners o
                         inner join s_durations sd on o.durationid = sd.durationid
                order by o.ID;"));


            //видове операции (действия) – код, наименование, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране;
            Queries.Add(new JQuery("nomen-oper-types", "Ном. видове операции (действия)", @"
                select 'pko' as ucode,
                       'ПКО' as code_name,
                       DATE('2000-01-01')  as from_date,
                       DATE('5000-01-01')  as to_date,
                       DATE('2000-01-01')  as lastupdatetime
                FROM dual;"));

            //видове плащания – код, наименование, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране;
            Queries.Add(new JQuery("nomen-payment-types", "Ном. видове плащания", @"
                select s.codeid,
                       s.ucode,
                       s.code_name,
                       sd.from_date,
                       sd.to_date,
                       sd.lastupdatetime
                FROM s_codes s
                         inner join s_durations sd on s.durationid = sd.durationid
                where s.code_typeid = 100
                  AND @fromdate <= sd.to_date
                  AND @todate >= sd.from_date
                order by s.pos;"));

            //търговски обекти – код, наименование, местонахождение, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране;
            Queries.Add(new JQuery("nomen-sell-points", "Ном. търговски обекти", @"
                select s.codeid,
                       s.ucode,
                       s.code_name,
                       '' as acodenote,
                       sd.from_date,
                       sd.to_date,
                       sd.lastupdatetime
                FROM s_codes s
                         inner join s_durations sd on s.durationid = sd.durationid
                where s.code_typeid = 102
                  AND @fromdate <= sd.to_date
                  AND @todate >= sd.from_date
                order by s.pos;"));

            //работни места – код, търговски обект, в който се намира, индивидуален номер на свързаното към него ФУ, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране;
            Queries.Add(new JQuery("nomen-workstations", "Ном. работни места", @"
                select s.codeid,
                       s.ucode,
                       (SELECT CONCAT('(', po.ucode, ') ', po.code_name)
                        FROM s_codes po
                        where po.codeid = s.parent_codeid) as sell_point_code,
                       pr.serial_number,
                       sd.from_date,
                       sd.to_date,
                       sd.lastupdatetime
                FROM s_codes s
                         inner join s_durations sd on s.durationid = sd.durationid
                         inner join s_printers pr on pr.printer_id = 1
                where s.code_typeid = 103
                  AND @fromdate <= sd.to_date
                  AND @todate >= sd.from_date
                order by s.pos;"));

            //потребители (оператори) – уникален код в системата, имена по документ за самоличност, дата на първоначално конфигуриране в системата, дата на последна промяна, дата на деактивиране; присвоени роли и период на активност за всяка от тях, други въведени данни;
            Queries.Add(new JQuery("nomen-users", "Ном. потребители (оператори) и права", @"
                select u.ID,
                       u.full_name,
                       u.from_date,
                       u.to_date,
                       u.lastupdatetime,
                       u.has_access,
                       u.user_privileges
                FROM vs_users u
                where u.ID > 1
                order by u.ID;"));


            //права, присвоявани на ролите – код, наименование, описание, дата на конфигуриране/деактивиране.
            Queries.Add(new JQuery("nomen-user-privileges", "Ном. видове права", @"
                select '01' as ucode, 'АДМИНИСТРАТОР' as code_name, DATE('2000-01-01') as from_date, DATE('5000-01-01') as to_date, DATE('2000-01-01') as lastupdatetime
                union all
                select '02' as ucode, 'ПРОДАВАЧ' as code_name, DATE('2000-01-01') as from_date, DATE('5000-01-01') as to_date, DATE('2000-01-01') as lastupdatetime  
                union all
                select '03' as ucode, 'ОДИТОРСКИ ПРОФИЛ' as code_name, DATE('2000-01-01') as from_date, DATE('5000-01-01') as to_date, DATE('2000-01-01') as lastupdatetime                
                 " ));
            #endregion Номенклатури

        }

        public static string GetLogsSql()
        {
            return @"
                select s.log_datetime,
                      lpad(u.ID,4,'0')    as user_code,
                       u.full_name,
                       su.unp,
                       (case
                            when s.action_type = 1 THEN 'Нов запис'
                            when s.action_type = 2 THEN 'Промяна'
                            when s.action_type = 3 THEN 'Изтриване'
                            else 'N/A' END)              as action_type_name,
                       CONCAT(s.key_name, ':', s.key_id) as table_name,
                       s.log_contain
                FROM s_log s
                         inner join s_sessions ss on s.sessionid = ss.sessionid
                         inner join users u on ss.userid = u.ID
                         inner join s_unps su on s.unpid = su.unpid
                WHERE 1=1
                AND s.log_datetime >= @fromDateTime
                AND s.log_datetime <= @toDateTime
                -- key_id AND s.key_id = @key_id
                -- key_name AND s.key_name = @key_name
                -- user_code AND u.ID = @user_code
                -- unp AND su.unp = @unp
                ORDER BY s.log_datetime;";
        }
        #endregion SQL

        #region Tables
        private static void InitTables()
        {
            Tables.Add(new JQuery("s_cash_receipts", "Таблица: s_cash_receipts", "SELECT * FROM s_cash_receipts"));
            Tables.Add(new JQuery("s_code_types", "Таблица: s_code_types", "SELECT * FROM s_code_types"));
            Tables.Add(new JQuery("s_codes", "Таблица: s_codes", "SELECT * FROM s_codes"));
            Tables.Add(new JQuery("s_counters", "Таблица: s_counters", "SELECT * FROM s_counters"));
            Tables.Add(new JQuery("s_durations", "Таблица: s_durations", "SELECT * FROM s_durations"));
            Tables.Add(new JQuery("s_info", "Таблица: s_info", "SELECT * FROM s_info"));
            Tables.Add(new JQuery("s_log", "Таблица: s_log", "SELECT * FROM s_log"));
            Tables.Add(new JQuery("s_orders", "Таблица: s_orders", "SELECT * FROM s_orders"));
            Tables.Add(new JQuery("s_orders_annulled", "Таблица: s_orders_annulled", "SELECT * FROM s_orders_annulled"));
            Tables.Add(new JQuery("s_paydistributions", "Таблица: s_paydistributions", "SELECT * FROM s_paydistributions"));
            Tables.Add(new JQuery("s_payments", "Таблица: s_payments", "SELECT * FROM s_payments"));
            Tables.Add(new JQuery("s_printers", "Таблица: s_printers", "SELECT * FROM s_printers"));
            Tables.Add(new JQuery("s_product_prices", "Таблица: s_product_prices", "SELECT * FROM s_product_prices"));
            Tables.Add(new JQuery("s_products", "Таблица: s_products", "SELECT * FROM s_products"));
            Tables.Add(new JQuery("s_sessions", "Таблица: s_sessions", "SELECT * FROM s_sessions"));
            Tables.Add(new JQuery("s_settings", "Таблица: s_settings", "SELECT * FROM s_settings"));
            Tables.Add(new JQuery("s_supto_tables", "Таблица: s_supto_tables", "SELECT * FROM s_supto_tables"));
            Tables.Add(new JQuery("s_unps", "Таблица: s_unps", "SELECT * FROM s_unps"));
            Tables.Add(new JQuery("vs_advance_distributions", "Таблица: vs_advance_distributions", "SELECT * FROM vs_advance_distributions"));
            Tables.Add(new JQuery("vs_orders", "Таблица: vs_orders", "SELECT * FROM vs_orders"));
            Tables.Add(new JQuery("vs_orders_annulled", "Таблица: vs_orders_annulled", "SELECT * FROM vs_orders_annulled"));
            Tables.Add(new JQuery("vs_paydistributions", "Таблица: vs_paydistributions", "SELECT * FROM vs_paydistributions"));
            Tables.Add(new JQuery("vs_payments", "Таблица: vs_payments", "SELECT * FROM vs_payments"));
            Tables.Add(new JQuery("vs_products", "Таблица: vs_products", "SELECT * FROM vs_products"));
            Tables.Add(new JQuery("vs_users", "Таблица: vs_users", "SELECT * FROM vs_users"));
            Tables.Add(new JQuery("vs_codes", "Таблица: vs_codes", "SELECT * FROM vs_codes"));
            Tables.Add(new JQuery("s_cash_receipts_details", "Таблица: s_cash_receipts_details", "SELECT * FROM s_cash_receipts_details"));
            Tables.Add(new JQuery("act", "Таблица: act", "SELECT * FROM act"));
            Tables.Add(new JQuery("act_exam", "Таблица: act_exam", "SELECT * FROM act_exam"));
            Tables.Add(new JQuery("act_calc_man", "Таблица: act_calc_man", "SELECT * FROM act_calc_man"));
            Tables.Add(new JQuery("act_calc_store", "Таблица: act_calc_store", "SELECT * FROM act_calc_store"));
            Tables.Add(new JQuery("act_imaging", "Таблица: act_imaging", "SELECT * FROM act_imaging"));
            Tables.Add(new JQuery("act_lab", "Таблица: act_lab", "SELECT * FROM act_lab"));
            Tables.Add(new JQuery("act_vaccine", "Таблица: act_vaccine", "SELECT * FROM act_vaccine"));
            Tables.Add(new JQuery("cash_avance", "Таблица: cash_avance", "SELECT * FROM cash_avance"));
            Tables.Add(new JQuery("cash_avance_uses", "Таблица: cash_avance_uses", "SELECT * FROM cash_avance_uses"));
            Tables.Add(new JQuery("hospital", "Таблица: hospital", "SELECT * FROM hospital"));
            Tables.Add(new JQuery("hospital_daily", "Таблица: hospital_daily", "SELECT * FROM hospital_daily"));
            Tables.Add(new JQuery("man_act", "Таблица: man_act", "SELECT * FROM man_act"));
            Tables.Add(new JQuery("man_groups", "Таблица: man_groups", "SELECT * FROM man_groups"));
            Tables.Add(new JQuery("man_list", "Таблица: man_list", "SELECT * FROM man_list"));
            Tables.Add(new JQuery("owners", "Таблица: owners", "SELECT * FROM owners"));
            Tables.Add(new JQuery("patients", "Таблица: patients", "SELECT * FROM patients"));
            Tables.Add(new JQuery("shop_articles", "Таблица: shop_articles", "SELECT * FROM shop_articles"));
            Tables.Add(new JQuery("shop_docin", "Таблица: shop_docin", "SELECT * FROM shop_docin"));
            Tables.Add(new JQuery("shop_docin_pay", "Таблица: shop_docin_pay", "SELECT * FROM shop_docin_pay"));
            Tables.Add(new JQuery("shop_docout", "Таблица: shop_docout", "SELECT * FROM shop_docout"));
            Tables.Add(new JQuery("shop_group", "Таблица: shop_group", "SELECT * FROM shop_group"));
            Tables.Add(new JQuery("shop_in", "Таблица: shop_in", "SELECT * FROM shop_in"));
            Tables.Add(new JQuery("shop_manufacturers", "Таблица: shop_manufacturers", "SELECT * FROM shop_manufacturers"));
            Tables.Add(new JQuery("shop_out", "Таблица: shop_out", "SELECT * FROM shop_out"));
            Tables.Add(new JQuery("shop_price", "Таблица: shop_price", "SELECT * FROM shop_price"));
            Tables.Add(new JQuery("shop_subgroup", "Таблица: shop_subgroup", "SELECT * FROM shop_subgroup"));
            Tables.Add(new JQuery("store_providers", "Таблица: store_providers", "SELECT * FROM store_providers"));
            Tables.Add(new JQuery("users", "Таблица: users", "SELECT * FROM users"));
        }
        #endregion Tables
    }

    public class JQuery
    {
        public JQuery()
        {
        }

        public JQuery(string value, string display, string sql = "")
        {
            this.ValueMember = value;
            this.DisplayMember = display;
            this.Sql = sql;
        }

        /// <summary>
        /// Вътрешно име на заявката
        /// </summary>
        public string ValueMember { get; set; }

        /// <summary>
        /// Име на заявката за визуализация
        /// </summary>
        public string DisplayMember { get; set; }

        /// <summary>
        /// SQL
        /// </summary>
        public string Sql { get; set; }

        public override string ToString()
        {
            return this.DisplayMember;
        }
    }
}
