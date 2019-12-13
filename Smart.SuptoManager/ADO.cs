using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Smart.SuptoManager
{
    public class ADO
    {
        private static string ConnectionString;

        public static void SetConnectionString(string host, uint port, string database, string username, string password)
        {
            var bld = new MySqlConnectionStringBuilder();
            bld.Server = host;
            bld.Port = port;
            bld.UserID = username;
            bld.Password = password;
            bld.Database = database;
            bld.ConvertZeroDateTime = true;

            ConnectionString = bld.ToString();
        }

        public static IDbDataParameter Parameter(string name, object value)
        {
            return new MySqlParameter(name, value);
        }

        public static async Task<DataTable> ExecuteDataTableAsync(string sql, params IDbDataParameter[] parameters)
        {
            using (var con = new MySqlConnection(ADO.ConnectionString))
            using (var cmd = new MySqlCommand(sql, con))
            using (var adp = new MySqlDataAdapter(cmd))
            {
                await con.OpenAsync();
                if (parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                var data = new DataTable("data");
                adp.Fill(data);
                return data;
            }
        }

        public static async Task<DataRow> ExecuteRowAsync(string sql, params IDbDataParameter[] parameters)
        {
            using (var con = new MySqlConnection(ADO.ConnectionString))
            using (var cmd = new MySqlCommand(sql, con))
            {
                await con.OpenAsync();
                if (parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                var data = new DataTable("data");
                var reader = await cmd.ExecuteReaderAsync();
                data.Load(reader);

                if (data.Rows.Count == 0)
                {
                    return null;
                }

                return data.Rows[0];
            }
        }
    }
}
