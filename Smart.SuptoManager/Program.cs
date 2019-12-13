using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart.SuptoManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GLOB.CSVSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            GLOB.CSVExportPath = Environment.CurrentDirectory;

            using (var login = new XF_Main())
            {
                Application.Run(login);
                if (login.DialogResult == DialogResult.Yes)
                {
                    Application.Run(new XF_Catalog());
                }
            }
        }
    }
}
