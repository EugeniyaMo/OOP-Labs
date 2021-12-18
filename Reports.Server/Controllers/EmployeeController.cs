using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/employees/create")]
        public async Task<Employee> Create([FromQuery][Required] string name, [FromQuery] Guid teamLeadId)
        {
            return await _service.Create(name, teamLeadId);
        }

        [HttpGet]
        [Route("/employees/find")]
        public async Task<IActionResult> Find([FromQuery] string name, [FromQuery] Guid id)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var result = _service.FindByName(name);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            if (id != Guid.Empty)
            {
                Task<Employee> result = _service.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }
        
        [HttpPut]
        [Route("/employees/update")]
        public async Task<IActionResult> Update([FromQuery][Required] Guid employeeId, [FromQuery] string newName, 
            [FromQuery] Guid newTeamLeadId)
        {
            if (!string.IsNullOrWhiteSpace(newName))
            {
                var result = _service.Update(employeeId, newName, newTeamLeadId);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            if (newTeamLeadId != Guid.Empty)
            {
                var result = _service.Update(employeeId, newName, newTeamLeadId);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("/employees/getAll")]
        public async Task<List<Employee>> GetAll()
        {
            return await _service.GetAll();
        }
        
        [HttpDelete]
        [Route("/employees/delete")]
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