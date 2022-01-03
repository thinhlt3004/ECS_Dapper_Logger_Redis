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
    public class ServiceRepo : IServiceRepo
    {
        private readonly IDbConnection _dbCtx;

        public ServiceRepo(Connection dbCtx)
        {
            _dbCtx = dbCtx.ConnectionDB;
        }

        public async Task<bool> Create(Service s)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"INSERT INTO Services (Id, Name, Description, Price, ServiceCategoryId, Image) VALUES (@Id, @Name, @Description, @Price, @ServiceCategoryId, @Image)";
                dbCon.Open();
                int result = await dbCon.ExecuteAsync(query, new
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    ServiceCategoryId = s.ServiceCategoryId,
                    Image = s.Image
                });

                return result > 0;
            }
        }

        public async Task<bool> Delete(string id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"DELETE FROM Services WHERE Id = @id";
                dbCon.Open();
                int result = await dbCon.ExecuteAsync(query, new { id = id });
                return result > 0;
            }
        }

        public async Task<List<Service>> GetAll()
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT * FROM Services";
                dbCon.Open();
                return (List<Service>)await dbCon.QueryAsync<Service>(query);

            }
        }

        public async Task<Service> GetByID(string id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT * FROM Services WHERE Id = @id";
                dbCon.Open();
                return await dbCon.QuerySingleOrDefaultAsync<Service>(query, new { id = id });
            }
        }

        public async Task<bool> Update(Service s)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"UPDATE Services SET Name = @Name, Description = @Description, Price = @Price, ServiceCategoryID = @ServiceCategoryID, Image = @Image WHERE Id = @Id";
                dbCon.Open();
                int result = await dbCon.ExecuteAsync(query, new
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    ServiceCategoryID = s.ServiceCategoryId,
                    Image = s.Image
                });
                return result > 0;
            }
        }
    }
}
