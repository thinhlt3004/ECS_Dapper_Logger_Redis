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
    public class EmpOfCustomerRepo : IEmpOfCustomerRepo
    {

        private readonly IDbConnection _dbCtx;

        public EmpOfCustomerRepo(Connection dbCtx)
        {
            _dbCtx = dbCtx.ConnectionDB;
        }

        public async Task<bool> Create(EmpOfCustomer sc)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"INSERT INTO EmpOfCustomer(EmpId, ServiceOfCus, StartDate, EndDate, Status) VALUES(@EmpId, @ServiceOfCus, @StartDate, @EndDate, @Status)";
                dbCon.Open();
                int result = await dbCon.ExecuteAsync(query, new
                {
                    EmpId = sc.EmpId,
                    ServiceOfCus = sc.ServiceOfCus,
                    StartDate = sc.StartDate,
                    EndDate = sc.EndDate,
                    Status = true
                });
                return result > 0;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"DELETE FROM EmpOfCustomer WHERE Id = @id";
                dbCon.Open();
                int result = await dbCon.ExecuteAsync(query, new
                {
                    id = id
                });

                return result > 0;
            }
        }

        public async Task<List<EmpOfCustomer>> GetAll()
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT * FROM EmpOfCustomer";
                dbCon.Open();
                return (List<EmpOfCustomer>) await dbCon.QueryAsync<EmpOfCustomer>(query);
            }
        }

        public async Task<EmpOfCustomer> GetByID(int id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT * FROM EmpOfCustomer WHERE Id = @id";
                dbCon.Open();
                return (EmpOfCustomer) await dbCon.QuerySingleOrDefaultAsync<EmpOfCustomer>(query, new
                {
                    id = id
                });
            }
        }

        public async Task<bool> Update(int id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"UPDATE EmpOfCustomer SET Status=@status WHERE Id = @id";
                dbCon.Open();

                int result = await dbCon.ExecuteAsync(query, new
                {
                    id = id,
                    status = false
                });
                return result > 0;
            }
        }
    }
}
