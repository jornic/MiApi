using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Clase07.Data.Repository
{
    public abstract class Connection
    {
        private readonly string _conexionString;

        public Connection()
        {
            IConfigurationRoot builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _conexionString = builder.GetConnectionString("Conexion");
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_conexionString);
        }
    }
}

