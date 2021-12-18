using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Entities;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportsDatabaseContext _context;

        public ReportService(ReportsDatabaseContext context) {
            _context = context;
        }
        
        public async Task<Report> Create(Guid employeeId)
        {
            var tasks = _context.Tasks.Include("AssignedEmployee").Where(t =>
                t.AssignedEmployee.Id == employeeId).ToList();
            var report = new Report(Guid.NewGuid(), DateTime.Now, employeeId, tasks);
            return report;
        }
        
        public async Task<Report> AddTask(Guid reportId, Guid taskId)
        {
            TaskModel task = _context.Tasks.Include("AssignedEmployee").FirstOrDefaultAsync(t =>
                t.Id == taskId).Result;
            _context.Reports.Include("Tasks").FirstOrDefaultAsync(r => 
                r.Id == reportId).Result.Tasks.Add(task);
            _context.SaveChanges();
            Report report = _context.Reports.FirstOrDefaultAsync(r => r.Id == reportId).Result;
            return report;
        }

        public async Task<Report> CreateTeamLeadReport()
        {
            var tasks = _context.Tasks.Include("AssignedEmployee").ToList();
            var report = new Report(Guid.NewGuid(), DateTime.Now, Guid.Empty, tasks);
            return report;
        }

        public async Task<Report> FindById(Guid id)
        {
            return await _context.Reports.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}