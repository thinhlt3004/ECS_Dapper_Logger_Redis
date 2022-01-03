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
    public class ReportRepo : IReportRepo
    {

        private readonly IDbConnection _dbCtx;

        public ReportRepo(Connection dbCtx)
        {
            _dbCtx = dbCtx.ConnectionDB;
        }

        public async Task<bool> Create(Report r)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"INSERT INTO Report (ServiceOfCus, date, count, totalPrice) VALUES(@ServiceOfCus, @date, @count, @totalPrice)";
                dbCon.Open();
                int result = await _dbCtx.ExecuteAsync(query, new
                {
                    ServiceOfCus = r.ServiceOfCus,
                    date = r.Date,
                    count = r.Count,
                    totalPrice = r.TotalPrice
                });
                return result > 0;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"DELETE FROM Report WHERE Id = @id";
                dbCon.Open();
                int result = await dbCon.ExecuteAsync(query, new
                {
                    id = id
                });
                return result > 0;
            }
        }

        public async Task<List<Report>> GetAll()
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT * FROM Report";
                dbCon.Open();
                return (List<Report>)await dbCon.QueryAsync<Report>(query);
            }
        }

        public async Task<Report> GetById(int id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT * FROM Report WHERE Id = @id";
                dbCon.Open();
                return await dbCon.QuerySingleOrDefaultAsync<Report>(query, new
                {
                    id = id
                });
            }
        }

        public async Task<bool> Update(Report r)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"UPDATE Report SET ServiceOfCus=@ServiceOfCus, date=@date, count=@count, totalPrice=@totalPrice WHERE Id=@id";
                dbCon.Open();
                int result = await dbCon.ExecuteAsync(query, new
                {
                    id = r.Id,
                    ServiceOfCus = r.ServiceOfCus,
                    date= r.Date,
                    count = r.Count,
                    totalPrice = r.TotalPrice
                });
                return result > 0;
            }
        }
    }
}
