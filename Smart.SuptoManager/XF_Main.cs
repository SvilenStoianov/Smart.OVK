using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Smart.SuptoManager
{

    public partial class XF_Main : Form
    {
        public XF_Main()
        {
            this.InitializeComponent();

            this.Load += XF_Main_Load;
            lblVersion.Text = "Версия: " + typeof(SuptoQueries).Assembly.GetName().Version + " Copyright ©  2019 Всички права запазени";
        }

        private void XF_Main_Load(object sender, EventArgs e)
        {
            var con = GetConnection();
            txtHost.Text = (con.ContainsKey("Host") ? con["Host"] : string.Empty);
            txtPort.Text = (con.ContainsKey("Port") ? con["Port"] : string.Empty);
            txtDatabase.Text = (con.ContainsKey("Database") ? con["Database"] : string.Empty);
            txtUsername.Text = (con.ContainsKey("Username") ? con["Username"] : string.Empty);
            txtPassword.Text = (con.ContainsKey("Password") ? con["Password"] : string.Empty);
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHost.Text))
            {
                Mess.Error("Моля, въведете \"СЪРВЪР IP\" !");
                txtHost.Select();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPort.Text) || !uint.TryParse(txtPort.Text.Trim(), out uint port))
            {
                Mess.Error("Моля, въведете \"ПОРТ\" !");
                txtPort.Select();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDatabase.Text))
            {
                Mess.Error("Моля, въведете \"БАЗА\" !");
                txtDatabase.Select();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                Mess.Error("Моля, въведете \"ПОТРЕБИТЕЛ\" !");
                txtUsername.Select();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                Mess.Error("Моля, въведете \"ПАРОЛА\" !");
                txtPassword.Select();
                return;
            }

            ADO.SetConnectionString(txtHost.Text.Trim(), port, txtDatabase.Text, txtUsername.Text.Trim(), txtPassword.Text.Trim());
            try
            {
                EnableControls(false);

                var check = await ADO.ExecuteRowAsync(SuptoQueries.CheckConnection());

                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            catch (MySqlException ex)
            {
                EnableControls(true);

                if (ex.Number == 1042)
                {
                    Mess.Error("Няма връзка към даденото СЪРВЪРНО IP!\r\n\r\n" + ex.Message);
                    return;
                }

                if (ex.Number == 1045 || (ex.InnerException is MySqlException mex && mex.Number == 1045))
                {
                    Mess.Error("Въведения ПОТРЕБИТЕЛ или ПАРОЛА са невалидни!");
                    return;
                }

                Mess.Exception(ex);
            }
            catch (Exception ex)
            {
                EnableControls(true);
                Mess.Exception(ex);
            }
        }

        private void EnableControls(bool enable)
        {
            txtHost.ReadOnly = !enable;
            txtPort.ReadOnly = !enable;
            txtDatabase.ReadOnly = !enable;
            txtUsername.ReadOnly = !enable;
            txtPassword.ReadOnly = !enable;
            btnExecute.Enabled = enable;
            this.ControlBox = enable;
        }

        private Dictionary<string, string> GetConnection()
        {
            var dic = new Dictionary<string, string>();
            if (!File.Exists("connection.ini"))
            {
                return dic;
            }

            string[] lines = File.ReadAllLines("connection.ini");
            foreach (string l in lines)
            {
                string[] split = l.Split('=');
                if (split.Length == 2)
                {
                    dic.Add(split[0], split[1]);
                }
            }

            return dic;
        }
    }
}