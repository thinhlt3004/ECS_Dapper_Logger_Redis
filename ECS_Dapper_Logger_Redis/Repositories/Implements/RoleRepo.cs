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
    public class RoleRepo : IRoleRepo
    {
        private readonly IDbConnection _dbCtx;

        public RoleRepo(Connection dbCtx)
        {
            _dbCtx = dbCtx.ConnectionDB;
        }

        public async Task<List<Role>> GetAll()
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = "SELECT Id, Role as role1 FROM Roles";
                dbCon.Open();
                return (List<Role>)await dbCon.QueryAsync<Role>(query);
            }
        }

        public async Task<Role> GetByID(int id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT Id, Role as role1  FROM Roles WHERE Id=@id";
                dbCon.Open();
                return (Role) await dbCon.QuerySingleOrDefaultAsync<Role>(query, new
                {
                    id = id
                });
            }
        }
    }
}
