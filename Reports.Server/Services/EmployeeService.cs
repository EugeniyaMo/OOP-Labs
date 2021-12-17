using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Reports.Entities;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ReportsDatabaseContext _context;

        public EmployeeService(ReportsDatabaseContext context) {
            _context = context;
        }

        public async Task<Employee> Create(string name, Guid bossId)
        {
            var employee = new Employee(Guid.NewGuid(), name, bossId);
            EntityEntry<Employee> employeeFromDb = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> FindByName(string name)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Name == name);;
        }

        public async Task<Employee> FindById(Guid id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> Update(Guid employeeId, string newName, Guid newTeamLeadId)
        {
            var newEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if (newEmployee == default)
                return false;
            if (!String.IsNullOrEmpty(newName))
                newEmployee.Name = newName;
            if (newTeamLeadId != default)
                newEmployee.TeamLeadId = newTeamLeadId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Employee> Delete(Guid id)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return employee;
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }
    }
}