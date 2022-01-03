using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DAL
{
    public class Connection
    {
        private IConfiguration _config;
        public string ConnectionString;

        public Connection(IConfiguration config)
        {
            _config = config;
            ConnectionString = _config.GetConnectionString("DBConnection");
        }

        public IDbConnection ConnectionDB
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }
    }
}
