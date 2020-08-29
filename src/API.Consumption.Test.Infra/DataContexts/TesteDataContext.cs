using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace API.Consumption.test.Infra.DataContexts
{
    public class TesteDataContext : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public TesteDataContext(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetConnectionString("TesteDb"));
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
