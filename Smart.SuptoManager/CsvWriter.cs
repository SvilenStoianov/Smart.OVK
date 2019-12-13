using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.SuptoManager
{
    /// <summary>
    /// Помощен клас за експортиране на модели към CSV
    /// </summary>
    public class CsvWriter : IDisposable
    {
        private TextWriter Output;

        private bool HasHeader;

        public CsvConfiguration Configuration { get; set; }

        public CsvWriter(TextWriter output)
        {
            this.Output = output;
            this.Configuration = new CsvConfiguration();
        }

        public void Write<T>(IEnumerable<T> rows, Dictionary<string, string> headerMap = null)
        {
            var props = typeof(T).GetProperties();
            //проверяваме дали трябва да напишем хедъра
            if (this.HasHeader == false)
            {
                this.HasHeader = true;
                foreach (var pr in props)
                {
                    if (headerMap != null && headerMap.ContainsKey(pr.Name))
                    {
                        this.Write(headerMap[pr.Name]);
                    }
                    else
                    {
                        this.Write(pr.Name);
                    }

                    //ако е последната колона не трябва да има разделител
                    if (pr != props[props.Length - 1])
                    {
                        this.Output.Write(this.Configuration.Delimiter);
                    }
                }

                this.Output.Write(this.Configuration.NewLine);
            }

            foreach (var row in rows)
            {
                foreach (var pr in props)
                {
                    object value = pr.GetValue(row, null);

                    //ако е дата прилагаме форматиране
                    if (value is DateTime d)
                    {
                        string str = d.ToString(this.Configuration.DateTimeFormat);
                        this.Output.Write(str);
                    }
                    //ако е null празен стринг
                    else if (value is null)
                    {
                        this.Output.Write(string.Empty);
                    }
                    //по-подразбиране стойността
                    else
                    {
                        this.Write(value.ToString());
                    }

                    //ако е последната колона не трябва да има разделител
                    if (pr != props[props.Length - 1])
                    {
                        this.Output.Write(this.Configuration.Delimiter);
                    }
                }

                this.Output.Write(this.Configuration.NewLine);
            }

            this.Output.Flush();
        }

        public void Write(DataTable table, Dictionary<string, string> headerMap = null)
        {
            var columns = table.Columns;
            //проверяваме дали трябва да напишем хедъра
            if (this.HasHeader == false)
            {
                this.HasHeader = true;
                foreach (DataColumn col in columns)
                {
                    if (headerMap != null && headerMap.ContainsKey(col.ColumnName))
                    {
                        this.Write(headerMap[col.ColumnName]);
                    }
                    else
                    {
                        this.Write(col.ColumnName);
                    }

                    //ако е последната колона не трябва да има разделител
                    if (col != columns[columns.Count - 1])
                    {
                        this.Output.Write(this.Configuration.Delimiter);
                    }
                }

                this.Output.Write(this.Configuration.NewLine);
            }

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in columns)
                {
                    object value = row[col];

                    //ако е дата прилагаме форматиране
                    if (value is DateTime d)
                    {
                        string str = d.ToString(this.Configuration.DateTimeFormat);
                        this.Output.Write(str);
                    }
                    //ако е null празен стринг
                    else if (value is DBNull || value is null)
                    {
                        this.Output.Write(string.Empty);
                    }
                    //по-подразбиране стойността
                    else
                    {
                        this.Write(value.ToString());
                    }

                    //ако е последната колона не трябва да има разделител
                    if (col != columns[columns.Count - 1])
                    {
                        this.Output.Write(this.Configuration.Delimiter);
                    }
                }

                this.Output.Write(this.Configuration.NewLine);
            }

            this.Output.Flush();
        }

        private void Write(string str)
        {
            if (str.Contains(this.Configuration.Delimiter) || str.Contains(this.Configuration.Quote) || str.Contains(this.Configuration.NewLine))
            {
                str = this.Configuration.Quote + str.Replace(this.Configuration.Quote, this.Configuration.Quote + this.Configuration.Quote) + this.Configuration.Quote;
            }

            this.Output.Write(str);
        }

        public void Dispose()
        {
            this.Output?.Flush();
            this.Output?.Dispose();
        }

        public class CsvConfiguration
        {
            public string NewLine { get; set; }

            public string Delimiter { get; set; }

            public string Quote { get; set; }

            public string DateTimeFormat { get; set; }

            public CsvConfiguration(string newLine = "\r\n", string delimiter = ";", string quote = "\"", string dateTimeFormat = "yyyy-MM-dd HH:mm:ss")
            {
                this.NewLine = newLine;
                this.Delimiter = delimiter;
                this.Quote = quote;
                this.DateTimeFormat = dateTimeFormat;
            }
        }
    }
}
