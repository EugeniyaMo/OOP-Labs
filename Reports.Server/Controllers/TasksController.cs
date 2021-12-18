using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Entities;
using Reports.Server.Database;
using Reports.Server.Services;
using Reports.Tools;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/tasks/create")]
        public async Task<TaskModel> Create([FromQuery][Required] Guid employeeId, [FromQuery] string comment)
        {
            return await _service.Create(employeeId, comment);
        }
        
        [HttpGet]
        [Route("/tasks/getAll")]
        public async Task<List<TaskModel>> GetAll()
        {
            return await _service.GetAll();
        }
        
        [HttpGet]
        [Route("/tasks/findById")]
        public async Task<IActionResult> FindById([FromQuery][Required] Guid id)
        {
            if (id != Guid.Empty)
            {
                Task<TaskModel> result = _service.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }
        
        [HttpPut]
        [Route("/tasks/addComment")]
        public async Task<bool> AddComment([FromQuery][Required] Guid id, [FromQuery][Required] string newComment)
        {
            return await _service.AddComment(id, newComment);
        }
        
        [HttpPut]
        [Route("/tasks/updateStatus")]
        public async Task<bool> UpdateStatus([FromQuery][Required] Guid id, [FromQuery][Required] TaskType.Type newStatus)
        {
            return await _service.UpdateStatus(id, newStatus);
        }
        
        [HttpPut]
        [Route("/tasks/updateAssignedEmployee")]
        public async Task<bool> UpdateAssignedEmployee([FromQuery][Required] Guid id, [FromQuery][Required] Guid newEmployeeId)
        {
            return await _service.UpdateAssignedEmployee(id, newEmployeeId);
        }
        
        [HttpGet]
        [Route("/tasks/getTasksByAssignedEmployee")]
        public async Task<List<TaskModel>> GetTasksByAssignedEmployee([FromQuery][Required] Guid id)
        {
            return await _service.GetTasksByAssignedEmployee(id);
        }
        
        [HttpGet]
        [Route("/tasks/getUpdateTasksByEmployee")]
        public async Task<List<TaskModel>> GetUpdateTasksByEmployee([FromQuery][Required] Guid id)
        {
            return await _service.GetUpdateTasksByEmployee(id);
        }
        
        [HttpDelete]
        [Route("/tasks/delete")]
        public async Task<IActionResult> Delete([FromQuery][Required] Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = _service.Delete(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }
    }
}