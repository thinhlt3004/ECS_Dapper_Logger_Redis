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
    public class ServiceCustomerRepo : IServiceCustomerRepo
    {

        private readonly IDbConnection _dbCtx;

        public ServiceCustomerRepo(Connection dbCtx)
        {
            _dbCtx = dbCtx.ConnectionDB;
        }

        public async Task<bool> Create(ServiceCustomer s)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"INSERT INTO ServiceCustomer(CustomerId, ServiceId, StartDate, EndDate, CurrentPrice, EmployeeHandle, Product, ProductPrice) VALUES (@CustomerId, @ServiceId, @StartDate, @EndDate, @CurrentPrice, @EmployeeHandle, @Product, @ProductPrice)";
                dbCon.Open();
                int result = await dbCon.ExecuteAsync(query, new
                {
                    CustomerId = s.CustomerId,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    CurrentPrice = s.CurrentPrice,
                    EmployeeHandle = false,
                    Product = s.Product,
                    ProductPrice = s.ProductPrice,
                    ServiceId = s.ServiceId
                });

                return result > 0;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"DELETE FROM ServiceCustomer WHERE Id = @id";
                dbCon.Open();
                int result = await dbCon.ExecuteAsync(query, new
                {
                    id = id
                });
                return result > 0;
            }
        }

        public async Task<List<ServiceCustomer>> GetAll()
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT * FROM ServiceCustomer";
                dbCon.Open();
                return (List<ServiceCustomer>)await dbCon.QueryAsync<ServiceCustomer>(query);
            }
        }

        public async Task<ServiceCustomer> GetByID(int id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT * FROM ServiceCustomer WHERE Id = @id";
                dbCon.Open();
                return (ServiceCustomer) await dbCon.QuerySingleOrDefaultAsync<ServiceCustomer>(query, new
                {
                    id = id
                });
            }
        }

        public async Task<bool> Update(int id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"UPDATE ServiceCustomer SET EmployeeHandle = @EmployeeHandle WHERE Id = @id ";
                dbCon.Open();
                int result = await dbCon.ExecuteAsync(query, new
                {
                    EmployeeHandle = true,
                    id = id
                });
                return result > 0;
            }
        }
    }
}
