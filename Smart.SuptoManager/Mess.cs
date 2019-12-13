using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart.SuptoManager
{
    public class Mess
    {
        public static void Error(string message, string title = "Грешка")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Exception(Exception ex, string title = "Грешка")
        {
            MessageBox.Show("Възникна неочаквана грешка:\r\n\r\n" + ex.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Question(string message, string title = "Въпрос")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static void Info(string message, string title = "Информация")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
