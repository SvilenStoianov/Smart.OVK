using LinqToDB.Configuration;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Web.Infrastructure
{
    public class MySqlConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }

        public string Name => "MySql";

        public string ProviderName => "MySql";

        public bool IsGlobal => false;
    }

    public class MySqlSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "MySql";

        public string DefaultDataProvider => "MySql";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return new MySqlConnectionStringSettings()
                {
                    ConnectionString = Global.AppConfig.ConnectionString
                };
            }
        }
    }

    public class OVKDb : DataConnection
    {
        public PocoShort Poco { get; set; }

        public OVKDb()
        {
            this.Poco = new PocoShort(this);
        }
    }
}
