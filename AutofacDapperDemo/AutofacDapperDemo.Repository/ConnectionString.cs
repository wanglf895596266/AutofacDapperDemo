using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDapperDemo.Repository
{
    public class ConnectionFactory
    {
        public static string Conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        public static IDbConnection GetDbConnection()
        {
            return new SqlConnection(Conn);
        }
    }
}
