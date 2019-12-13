using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart.SuptoManager
{
    public partial class XF_Tables : Form
    {
        public XF_Tables()
        {
            InitializeComponent();

            this.Load += XF_Tables_Load;
        }

        private void XF_Tables_Load(object sender, EventArgs e)
        {
            var tables = SuptoQueries.GetTables();
            listTables.DataSource = tables;
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listTables.Items.Count; i++)
            {
                listTables.SetSelected(i, true);
            }
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listTables.Items.Count; i++)
            {
                listTables.SetSelected(i, false);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var tables = listTables.SelectedItems.Cast<JQuery>().ToList();
            if (tables.Count == 0)
            {
                Mess.Error("Моля, изберете поне една таблица!");
                return;
            }

            XF_ExportCSV.ShowExport(async () =>
            {
                foreach (var tbl in tables)
                {
                    string filePath = Path.Combine(GLOB.CSVExportPath, tbl.ValueMember + ".csv");
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    DataTable data = await ADO.ExecuteDataTableAsync(tbl.Sql);
                    await Task.Run(() =>
                    {
                        using (var output = new StreamWriter(filePath, false, Encoding.UTF8))
                        using (var w = new CsvWriter(output))
                        {
                            var columns = SuptoQueries.GetColumns(tbl.ValueMember, data);
                            w.Write(data, columns.ToDictionary(k => k.ColumnName, v => v.DisplayName));
                        }
                    });
                }

                if (Mess.Question("Желаете ли да отворите папката с експортираните файлове?"))
                {
                    Process.Start(GLOB.CSVExportPath);
                }
            });
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
