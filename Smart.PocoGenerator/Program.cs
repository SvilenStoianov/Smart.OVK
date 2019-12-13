using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Data;
using System.IO;
using System.Text;

namespace Smart.PocoGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings.LoadSettings();

            var tables = GetSchema("Tables");
            var views = GetSchema("Views");
            var columns = GetSchema("Columns");

            var combinedTables = tables.Rows.Cast<DataRow>().ToList();
            combinedTables.AddRange(views.Rows.Cast<DataRow>());

            using (StreamWriter poco = new StreamWriter(Path.Combine(Settings.Instance.OutputDirectory, "pocos.cs"), false, Encoding.UTF8))
            {
                poco.WriteLine("using System;");
                poco.WriteLine("using LinqToDB.Mapping;");
                poco.WriteLine("");
                poco.WriteLine($"namespace {Settings.Instance.Namespace} {{");

                foreach (DataRow table in combinedTables)
                {
                    string tableName = table["TABLE_NAME"].ToString();
                    poco.WriteLine($"[Table(Name = \"{tableName}\")]");
                    poco.WriteLine($"public partial class {tableName} {{");

                    var tableColumns = columns.Rows
                        .Cast<DataRow>()
                        .Where(p => p["TABLE_NAME"].ToString() == tableName)
                        .OrderBy(p => Convert.ToInt32(p["ORDINAL_POSITION"]))
                        .ToList();
                    foreach (DataRow col in tableColumns)
                    {
                        string type = GetDotNetType(col["DATA_TYPE"].ToString());
                        bool isNullable = (type != "string" && col["IS_NULLABLE"].ToString() == "YES");
                        if (isNullable)
                        {
                            type += "?";
                        }
                        string columnName = col["COLUMN_NAME"].ToString();
                        bool isPrimaryKey = (col["COLUMN_KEY"].ToString() == "PRI");

                        if (isPrimaryKey)
                        {
                            poco.WriteLine("[PrimaryKey, Identity]");
                        }
                        else
                        {
                            poco.WriteLine($"[Column(Name = \"{columnName}\"){(isNullable ? string.Empty : ", NotNull")}]");
                        }

                        if (IsKeyword(columnName))
                        {
                            columnName = "@" + columnName;
                        }
                        poco.WriteLine(string.Format("public {0} {1} {{ get; set; }}", type, columnName));
                    }
                    poco.WriteLine("}");
                    poco.WriteLine();
                }

                poco.WriteLine("}");
            }



            using (StreamWriter poco = new StreamWriter(Path.Combine(Settings.Instance.OutputDirectory, "short.cs"), false, Encoding.UTF8))
            {
                poco.WriteLine("using System;");
                poco.WriteLine("using LinqToDB;");
                poco.WriteLine("");
                poco.WriteLine($"namespace {Settings.Instance.Namespace} {{");

                poco.WriteLine("public class PocoShort {");
                poco.WriteLine("private OVKDb Db;");
                poco.WriteLine("public PocoShort(OVKDb db){ this.Db = db; }");
                poco.WriteLine();

                foreach (DataRow table in combinedTables)
                {
                    string tableName = table["TABLE_NAME"].ToString();
                    poco.WriteLine($"public ITable<{tableName}> {tableName} => this.Db.GetTable<{tableName}>();");
                }

                poco.WriteLine("}");//end class
                poco.WriteLine("}");//end namespace
            }


            //PrintRows(tables);
            //PrintRows(views);
            //PrintRows(columns);

            //Console.ReadKey();
        }

        private static DataTable GetSchema(string collection = null)
        {
            using (var con = new MySqlConnection(Settings.Instance.ConnectionString))
            {
                con.Open();
                if (collection != null)
                {
                    return con.GetSchema(collection);
                }

                return con.GetSchema();
            }
        }

        private static string GetDotNetType(string mysqlType)
        {
            switch (mysqlType)
            {
                case "int": return "int";
                case "tinyint": return "int";
                case "timestamp": return "DateTime";
                case "varchar": return "string";
                case "bigint": return "long";
                case "text": return "string";
                case "date": return "DateTime";
                case "datetime": return "DateTime";
                case "decimal": return "decimal";
                case "float": return "float";
                case "smallint": return "int";
                default:
                    throw new ApplicationException("Непознат тип!");
            }
        }

        private static string[] _keywords = new string[]
            {
                "bool", "byte", "sbyte", "short", "ushort", "int", "uint", "long", "ulong", "double", "float", "decimal",
                "string", "char", "void", "object", "typeof", "sizeof", "null", "true", "false", "if", "else", "while", "for", "foreach", "do", "switch",
                "case", "default", "lock", "try", "throw", "catch", "finally", "goto", "break", "continue", "return", "public", "private", "internal",
                "protected", "static", "readonly", "sealed", "const", "fixed", "stackalloc", "volatile", "new", "override", "abstract", "virtual",
                "event", "extern", "ref", "out", "in", "is", "as", "params", "__arglist", "__makeref", "__reftype", "__refvalue", "this", "base",
                "namespace", "using", "class", "struct", "interface", "enum", "delegate", "checked", "unchecked", "unsafe", "operator", "implicit", "explicit"
            };

        private static bool IsKeyword(string str)
        {
            return _keywords.Contains(str);
        }

        private static void PrintRows(DataTable table)
        {
            Console.WriteLine("Schema:");
            foreach (DataColumn col in table.Columns)
            {
                Console.WriteLine($"{col.ColumnName} {col.DataType.Name} AllowNull: {col.AllowDBNull}");
            }

            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine(string.Join(" | ", row.ItemArray));
            }
        }
    }
}
