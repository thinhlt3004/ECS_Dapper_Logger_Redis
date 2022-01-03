using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Repositories.Interfaces
{
    public interface IReportRepo
    {
        Task<List<Report>> GetAll();

        Task<Report> GetById(int id);

        Task<bool> Create(Report r);

        Task<bool> Update(Report r);

        Task<bool> Delete(int id);
    }
}
