using Dapper;
using ECS_Dapper_Logger_Redis.DAL;
using ECS_Dapper_Logger_Redis.Models;
using ECS_Dapper_Logger_Redis.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Repositories.Implements
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly IDbConnection _dbCtx;

        public CustomerRepo(Connection dbCtx)
        {
            _dbCtx = dbCtx.ConnectionDB;
        }

        public async Task<List<Customer>> GetAll()
        {
            using(IDbConnection dbCon = _dbCtx)
            {
                string query = @"SELECT * FROM Customers";
                dbCon.Open();
                return (List<Customer>) await dbCon.QueryAsync<Customer>(query);
            }
        }

        public async Task<Customer> GetByID(int id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                string query = @"SELECT * FROM Customers WHERE Id = @id";
                dbCon.Open();
                return (Customer) await dbCon.QueryAsync<Customer>(query, new { id = id });
            }
        }
    }
}
