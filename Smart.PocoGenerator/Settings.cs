using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Smart.PocoGenerator
{
    public class Settings
    {
        public static Settings Instance { get; set; }

        public string ConnectionString { get; set; }

        public string OutputDirectory { get; set; }

        public string Namespace { get; set; }

        public static void LoadSettings()
        {
            if (!File.Exists("settings.json"))
            {
                var set = new Settings();
                set.ConnectionString = "Server=myServerAddress;Port=1234;Database=myDataBase;Uid=myUsername;Pwd=myPassword;";
                set.OutputDirectory = Environment.CurrentDirectory;
                set.Namespace = "Smart.Web";

                string defaultJson = JsonConvert.SerializeObject(set, Formatting.Indented);
                File.WriteAllText("settings.json.default", defaultJson, Encoding.UTF8);

                throw new ApplicationException("Липсва конфигурационен файл settings.json!");
            }

            string json = File.ReadAllText("settings.json", Encoding.UTF8);
            Settings.Instance = JsonConvert.DeserializeObject<Settings>(json);
        }
    }
}
