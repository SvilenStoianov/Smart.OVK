using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart.SuptoManager
{
    public partial class XF_Logs : Form
    {
        public XF_Logs()
        {
            InitializeComponent();
            gridData.AutoGenerateColumns = false;

            fromDateTime.Format = DateTimePickerFormat.Custom;
            toDateTime.Format = DateTimePickerFormat.Custom;
            fromDateTime.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            toDateTime.CustomFormat = "dd.MM.yyyy HH:mm:ss";


            this.Load += XF_Logs_Load;
            gridData.CellClick += GridData_CellClick;
        }

        private void GridData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != col_Preview.Index)
            {
                return;
            }

            DataTable data = gridData.DataSource as DataTable;
            if (data == null || data.Rows.Count == 0 || e.RowIndex >= data.Rows.Count)
            {
                return;
            }

            string logDetails = data.Rows[e.RowIndex]["log_contain"].ToString();

            Mess.Info(logDetails, "Информация за лога");
        }

        private void XF_Logs_Load(object sender, EventArgs e)
        {
            fromDateTime.Value = DateTime.Now.Date;
            toDateTime.Value = DateTime.Now.Date.AddSeconds(86399);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                EnableControls(false);

                string sql = SuptoQueries.GetLogsSql();
                var prm = new List<IDbDataParameter>();
                prm.Add(ADO.Parameter("fromDateTime", fromDateTime.Value));
                prm.Add(ADO.Parameter("toDateTime", toDateTime.Value));

                if (!string.IsNullOrWhiteSpace(key_id.Text))
                {
                    sql = sql.Replace("-- key_id", string.Empty);
                    prm.Add(ADO.Parameter("key_id", key_id.Text.Trim()));
                }
                if (!string.IsNullOrWhiteSpace(key_name.Text))
                {
                    sql = sql.Replace("-- key_name", string.Empty);
                    prm.Add(ADO.Parameter("key_name", key_name.Text.Trim()));
                }
                if (!string.IsNullOrWhiteSpace(user_code.Text))
                {
                    sql = sql.Replace("-- user_code", string.Empty);
                    prm.Add(ADO.Parameter("user_code", user_code.Text.Trim()));
                }
                if (!string.IsNullOrWhiteSpace(unp.Text))
                {
                    sql = sql.Replace("-- unp", string.Empty);
                    prm.Add(ADO.Parameter("unp", unp.Text.Trim()));
                }

                progress.Visible = true;
                DataTable data = await ADO.ExecuteDataTableAsync(sql, prm.ToArray());
                gridData.DataSource = data;
                progress.Visible = false;
            }
            catch (Exception ex)
            {
                Mess.Exception(ex);
            }
            finally
            {
                EnableControls(true);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable data = gridData.DataSource as DataTable;
            if (data == null || data.Rows.Count == 0)
            {
                Mess.Error("Няма данни за експортиране!");
                return;
            }

            XF_ExportCSV.ShowExport(async () =>
            {
                string filePath = Path.Combine(GLOB.CSVExportPath, "logs.csv");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                await Task.Run(() =>
                {
                    var columns = new Dictionary<string, string>();
                    columns["log_datetime"] = "Дата/час";
                    columns["user_code"] = "Потр. код";
                    columns["full_name"] = "Потребител";
                    columns["unp"] = "УНП";
                    columns["action_type_name"] = "Действие";
                    columns["table_name"] = "Таблица(запис)";
                    columns["log_contain"] = "Лог";

                    using (var output = new StreamWriter(filePath, false, Encoding.UTF8))
                    using (var w = new CsvWriter(output))
                    {
                        w.Write(data, columns);
                    }
                });

                if (Mess.Question("Желаете ли да отворите експортирания файл?"))
                {
                    Process.Start(filePath);
                }
            });
        }

        private void EnableControls(bool enable)
        {
            this.ControlBox = enable;
            key_id.ReadOnly = !enable;
            key_name.ReadOnly = !enable;
            user_code.ReadOnly = !enable;
            unp.ReadOnly = !enable;
            fromDateTime.Enabled = enable;
            toDateTime.Enabled = enable;
            btnSearch.Enabled = enable;
            btnExport.Enabled = enable;
        }
    }
}
