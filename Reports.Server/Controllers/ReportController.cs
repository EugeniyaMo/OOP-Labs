using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Components.Route("/tasks")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("/reports/create")]
        public async Task<Report> Create([FromQuery][Required] Guid employeeId)
        {
            return await _service.Create(employeeId);
        }

        [HttpPut]
        [Route("/reports/addTask")]
        public async Task<Report> AddTask([FromQuery][Required]Guid reportId, [FromQuery][Required]Guid taskId)
        {
            return await _service.AddTask(reportId, taskId);
        }
        
        [HttpPost]
        [Route("/reports/createTeamLeadReport")]
        public async Task<Report> CreateTeamLeadReport()
        {
            return await _service.CreateTeamLeadReport();
        }
        
        [HttpGet]
        [Route("/reports/createTeamLeadReport")]
        public async Task<Report> FindById([FromQuery][Required]Guid id)
        {
            return await _service.FindById(id);
        }
    }
}