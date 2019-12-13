using System;
using System.IO;
using System.Windows.Forms;

namespace Smart.SuptoManager
{
    public partial class XF_ExportCSV : Form
    {
        private Action ExportAction;

        public XF_ExportCSV()
        {
            InitializeComponent();
        }

        public static void ShowExport(Action exportAction)
        {
            using (var dlg = new XF_ExportCSV())
            {
                dlg.ExportAction = exportAction;

                dlg.CSVExportPath.Text = GLOB.CSVExportPath;
                dlg.CSVSeparator.Text = GLOB.CSVSeparator;

                dlg.ShowDialog();
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CSVExportPath.Text))
                {
                    Mess.Error("Пътя за експорт не може да бъде празен!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(CSVSeparator.Text))
                {
                    Mess.Error("Моля, въведете разделител!");
                    return;
                }

                GLOB.CSVExportPath = CSVExportPath.Text.Trim();
                GLOB.CSVSeparator = CSVSeparator.Text.Trim();

                if (!Directory.Exists(GLOB.CSVExportPath))
                {
                    Directory.CreateDirectory(GLOB.CSVExportPath);
                }

                EnableControls(false);
                this.ExportAction.Invoke();



                this.Close();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Моля, изберете директория къде да се експортират данните";
                dlg.ShowNewFolderButton = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    GLOB.CSVExportPath = dlg.SelectedPath;
                    CSVExportPath.Text = dlg.SelectedPath;
                }
            }
        }

        private void EnableControls(bool enable)
        {
            this.ControlBox = enable;
            CSVExportPath.ReadOnly = !enable;
            CSVSeparator.ReadOnly = !enable;
            btnSelect.Enabled = enable;
            btnExport.Enabled = enable;
            btnCancel.Enabled = enable;
            progress.Visible = !enable;
        }
    }
}
