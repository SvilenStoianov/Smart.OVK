using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart.SuptoManager
{
    public partial class XF_Catalog : Form
    {
        public XF_Catalog()
        {
            InitializeComponent();
            gridData.AutoGenerateColumns = false;

            this.Load += XF_Catalog_Load;
        }

        private void XF_Catalog_Load(object sender, EventArgs e)
        {
            var queries = SuptoQueries.GetQueries().ToArray();
            listQuery.Items.AddRange(queries);

            var now = DateTime.Now.Date;
            fromDate.Value = new DateTime(now.Year, now.Month, 1);
            toDate.Value = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var query = listQuery.SelectedItem as JQuery;
            if (query == null)
            {
                Mess.Error("Моля, изберете справка!");
                return;
            }

            try
            {
                EnableControls(false);

                string sql = query.Sql;
                var prm = new List<IDbDataParameter>();
                prm.Add(ADO.Parameter("fromdate", fromDate.Value.Date));
                prm.Add(ADO.Parameter("todate", toDate.Value.Date));
                if (!string.IsNullOrWhiteSpace(unp.Text))
                {
                    sql = sql.Replace("-- unp", string.Empty);
                    prm.Add(ADO.Parameter("unp", unp.Text.Trim()));
                }
                if (!string.IsNullOrWhiteSpace(sell_point_code.Text))
                {
                    sql = sql.Replace("-- sell_point_code", string.Empty);
                    prm.Add(ADO.Parameter("sell_point_code", sell_point_code.Text.Trim()));
                }
                if (!string.IsNullOrWhiteSpace(serial_number.Text))
                {
                    sql = sql.Replace("-- serial_number", string.Empty);
                    prm.Add(ADO.Parameter("serial_number", serial_number.Text.Trim()));
                }
                if (!string.IsNullOrWhiteSpace(sell_workplace_code.Text))
                {
                    sql = sql.Replace("-- sell_workplace_code", string.Empty);
                    prm.Add(ADO.Parameter("sell_workplace_code", sell_workplace_code.Text.Trim()));
                }
                if (!string.IsNullOrWhiteSpace(user_code.Text))
                {
                    sql = sql.Replace("-- user_code", string.Empty);
                    prm.Add(ADO.Parameter("user_code", user_code.Text.Trim()));
                }

                progress.Visible = true;
                DataTable data = await ADO.ExecuteDataTableAsync(sql, prm.ToArray());
                PopulateColumns(query.ValueMember, data);
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

        private void EnableControls(bool enable)
        {
            this.ControlBox = enable;
            listQuery.Enabled = enable;
            fromDate.Enabled = enable;
            toDate.Enabled = enable;
            unp.ReadOnly = !enable;
            sell_point_code.ReadOnly = !enable;
            serial_number.ReadOnly = !enable;
            sell_workplace_code.ReadOnly = !enable;
            user_code.ReadOnly = !enable;
            btnSearch.Enabled = enable;
        }

        private void PopulateColumns(string queryName, DataTable data)
        {
            var columns = SuptoQueries.GetColumns(queryName, data);
            gridData.Columns.Clear();

            foreach (var col in columns)
            {
                gridData.Columns.Add(col.ColumnName, col.DisplayName);
                var cc = gridData.Columns[gridData.Columns.Count - 1] as DataGridViewTextBoxColumn;
                cc.DataPropertyName = col.ColumnName;
                if (col.Type == ColumnType.Date)
                {
                    cc.DefaultCellStyle.Format = "dd.MM.yyyy";
                }
                else if (col.Type == ColumnType.DateTime)
                {
                    cc.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";
                }
                else if (col.Type == ColumnType.Time)
                {
                    cc.DefaultCellStyle.Format = @"hh\:mm\:ss";
                }
            }
        }

        private void btnExportToCSV_Click(object sender, EventArgs e)
        {
            var query = listQuery.SelectedItem as JQuery;
            if (query == null)
            {
                Mess.Error("Моля, изберете справка!");
                return;
            }

            DataTable data = gridData.DataSource as DataTable;
            if (data == null || data.Rows.Count == 0)
            {
                Mess.Error("Няма данни за експортиране!");
                return;
            }

            XF_ExportCSV.ShowExport(async () =>
            {
                string filePath = Path.Combine(GLOB.CSVExportPath, query.ValueMember + ".csv");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                await Task.Run(() =>
                {
                    using (var output = new StreamWriter(filePath, false, Encoding.UTF8))
                    using (var w = new CsvWriter(output))
                    {
                        var columns = SuptoQueries.GetColumns(query.ValueMember, data);
                        w.Write(data, columns.ToDictionary(k => k.ColumnName, v => v.DisplayName));
                    }
                });

                if (Mess.Question("Желаете ли да отворите експортирания файл?"))
                {
                    Process.Start(filePath);
                }
            });
        }

        private void btnExportTables_Click(object sender, EventArgs e)
        {
            using (var form = new XF_Tables())
            {
                form.ShowDialog();
            }
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            using (var form = new XF_Logs())
            {
                form.ShowDialog();
            }
        }
    }
}
