using System;
using System.Threading.Tasks;
using Reports.DAL.Entities;
using Reports.Entities;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        Task<Report> Create(Guid employeeId);
        Task<Report> AddTask(Guid reportId, Guid taskId);
        Task<Report> CreateTeamLeadReport();
        Task<Report> FindById(Guid id);
    }
}