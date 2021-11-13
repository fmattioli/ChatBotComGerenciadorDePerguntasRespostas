using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Infra.Data.Config
{
    public class DbSession : IDisposable
    {
        protected static string ConnectionString { get; set; }

        public IDbConnection Connection { get; set; }
        public DbSession(IConfiguration configuration)
        {
            Connection = new SqlConnection("Data Source=agarni.ddns.net,7789;Initial Catalog=Bot;Integrated Security=false;User Id=Bot;Password=bot@123;Connection Timeout=30;");
            Connection.Open();
            Inicializar();

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static void Inicializar()
        {
            ConnectionString = "Data Source=agarni.ddns.net,7789;Initial Catalog=Bot;Integrated Security=false;User Id=Bot;Password=bot@123;Connection Timeout=30;";
        }

        protected virtual void Dispose(bool disposing)
        {
            Connection.Dispose();
            Connection.Close();
        }

        ~DbSession()
        {
            Dispose(false);
        }
    }
}
