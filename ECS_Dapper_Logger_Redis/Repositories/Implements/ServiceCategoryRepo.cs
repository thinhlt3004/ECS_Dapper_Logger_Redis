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
    public class ServiceCategoryRepo : IServiceCategoryRepo
    {
        private readonly IDbConnection _dbCtx;

        public ServiceCategoryRepo(Connection dbCtx)
        {
            _dbCtx = dbCtx.ConnectionDB;
        }

        public async Task<List<ServiceCategory>> GetAll()
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT * FROM ServiceCategory";
                dbCon.Open();
                return (List<ServiceCategory>)await dbCon.QueryAsync<ServiceCategory>(query);
            }
        }

        public async Task<ServiceCategory> GetByID(int id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT * FROM ServiceCategory WHERE Id =  @id";
                dbCon.Open();
                return await dbCon.QuerySingleOrDefaultAsync<ServiceCategory>(query, new 
                {
                    id = id
                });
            }
        }
    }
}
