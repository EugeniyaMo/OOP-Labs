using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;
using Reports.Entities;
using Reports.Tools;

namespace Reports.Server.Services
{
    public interface ITaskService
    {
        Task<TaskModel> Create(Guid employeeId, string comment);
        Task<List<TaskModel>> GetAll();
        Task<TaskModel> FindById(Guid id);
        Task<bool> AddComment(Guid taskId, string newComment);
        Task<bool> UpdateStatus(Guid taskId, TaskType.Type newStatus);
        Task<bool> UpdateAssignedEmployee(Guid taskId, Guid newEmployeeId);
        //Task<TaskModel> FindByCreationTime(DateTime creationTime);
        Task<List<TaskModel>> GetTasksByAssignedEmployee(Guid employeeId);
        Task<List<TaskModel>> GetUpdateTasksByEmployee(Guid employeeId);
        Task<TaskModel> Delete(Guid id);
    }
}