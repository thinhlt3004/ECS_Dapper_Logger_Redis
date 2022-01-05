using Dapper;
using ECS_Dapper_Logger_Redis.DAL;
using ECS_Dapper_Logger_Redis.Models;
using ECS_Dapper_Logger_Redis.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Repositories.Implements
{
    public class ServiceRepo : IServiceRepo
    {
        private readonly IDbConnection _dbCtx;
        private readonly IDistributedCache _distributedCache;
        public ServiceRepo(Connection dbCtx, IDistributedCache distributedCache)
        {
            _dbCtx = dbCtx.ConnectionDB;
            _distributedCache = distributedCache;
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

        public async Task<List<Service>> GetAll(int pages)
        {
            var getServices = await _distributedCache.GetAsync($"get-all-service/{pages}");
            if(getServices != null)
            {
                var stringServices = Encoding.UTF8.GetString(getServices);
                var servicesRes = JsonConvert.DeserializeObject<List<Service>>(stringServices);
                return servicesRes;
            }
            using (IDbConnection dbCon = _dbCtx)
            {
                int prevRow = pages - 1;
                var totalPrev = prevRow * 2;
                var query = @"SELECT * FROM Services ORDER BY Id ASC OFFSET (@totalPrev) ROWS FETCH NEXT (2) ROWS ONLY";
                dbCon.Open();
                var serviceList = (List<Service>)await dbCon.QueryAsync<Service>(query, new 
                {
                   totalPrev = totalPrev,
                });

                var option = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(5));
                var serializeServices = JsonConvert.SerializeObject(serviceList);
                var byteServices = Encoding.UTF8.GetBytes(serializeServices);
                await _distributedCache.SetAsync($"get-all-service/{pages}" ,byteServices, option);
                return serviceList;
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
