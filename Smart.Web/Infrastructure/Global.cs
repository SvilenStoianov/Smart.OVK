using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Web.Infrastructure
{
    public static class Global
    {
        public static AppConfig AppConfig { get; set; }

        public static HandlerCollection Handlers { get; set; }

        public static string CurrentDirectory
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        public static void LoadSettings()
        {
            string settingsPath = Path.Combine(Global.CurrentDirectory, "settings.json");

            if (!File.Exists(settingsPath))
            {
                var set = new AppConfig();
                set.ConnectionString = "Server=myServerAddress;Port=1234;Database=myDataBase;Uid=myUsername;Pwd=myPassword;";

                string defaultJson = JsonConvert.SerializeObject(set, Formatting.Indented);
                File.WriteAllText(settingsPath + ".default", defaultJson, Encoding.UTF8);

                throw new ApplicationException("Липсва конфигурационен файл settings.json!");
            }

            string json = File.ReadAllText(settingsPath, Encoding.UTF8);
            Global.AppConfig = JsonConvert.DeserializeObject<AppConfig>(json);
        }
    }

    public class AppConfig
    {
        public string ConnectionString { get; set; }

        public bool IsDebug { get; set; }
    }
}
