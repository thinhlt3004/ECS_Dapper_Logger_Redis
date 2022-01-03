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
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly IDbConnection _dbCtx;

        public DepartmentRepo(Connection dbCtx)
        {
            _dbCtx = dbCtx.ConnectionDB;
        }

        public async Task<bool> Create(Department d)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"INSERT INTO Departments(Id, DepartmentName, Description) VALUES (@Id, @DepartmentName, @Description)";
                dbCon.Open();
                int result = await dbCon.ExecuteAsync(query, new 
                {
                    Id = d.Id,
                    DepartmentName = d.DepartmentName,
                    Description = d.Description
                });
                return result > 0;
            }
        }

        public async Task<bool> Delete(string id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"DELETE FROM Departments WHERE Id = @id";
                dbCon.Open();
                int result = await dbCon.ExecuteAsync(query, new
                {
                    id = id
                });
                return result > 0;
            }
        }

        public async Task<List<Department>> GetAll()
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT * FROM Departments";

                dbCon.Open();

                return (List<Department>)await dbCon.QueryAsync<Department>(query);
            }
        }

        public async Task<Department> GetByID(string id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"SELECT * FROM Departments WHERE Id = @id";
                dbCon.Open();

                return (Department) await dbCon.QuerySingleOrDefaultAsync<Department>(query, new 
                {
                    id = id
                });
            }
        }

        public async Task<bool> Update(Department d)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                var query = @"UPDATE Departments SET DepartmentName=@DepartmentName, Description=@Description WHERE Id=@Id";
                dbCon.Open();

                int result = await dbCon.ExecuteAsync(query, new 
                {
                    Id = d.Id,
                    DepartmentName = d.DepartmentName,
                    Description = d.Description
                });

                return result > 0;

            }
        }
    }
}
