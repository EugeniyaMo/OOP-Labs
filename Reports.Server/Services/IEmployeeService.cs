using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        Task<Employee> Create(string name, Guid bossId);
        Task<Employee> FindByName(string name);
        Task<Employee> FindById(Guid id);
        Task<bool> Update(Guid employeeId, string newName, Guid newTeamLeadId);
        Task<Employee> Delete(Guid id);
        Task<List<Employee>> GetAll();
    }
}