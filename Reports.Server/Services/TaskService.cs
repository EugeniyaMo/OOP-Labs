using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Entities;
using Reports.Server.Database;
using Reports.Tools;

namespace Reports.Server.Services
{
    public class TaskService : ITaskService
    {
        private readonly ReportsDatabaseContext _context;
        
        public TaskService(ReportsDatabaseContext context) {
            _context = context;
        }

        public async Task<TaskModel> Create(Guid employeeId, string comment)
        {
            Employee employee = _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId).Result;
            var newTask = new TaskModel(Guid.NewGuid(), TaskType.Type.Active,
                DateTime.Now, comment, employee);
            await _context.Tasks.AddAsync(newTask);
            await _context.SaveChangesAsync();
            return newTask;
        }
        
        public async Task<List<TaskModel>> GetAll()
        {
            return await _context.Tasks.Include("AssignedEmployee").ToListAsync();
        }

        public async Task<TaskModel> FindById(Guid id)
        {
            return await _context.Tasks.Include("AssignedEmployee").FirstOrDefaultAsync(t => t.Id == id);
        }

        /* public async Task<TaskModel> FindByCreationTime(DateTime creationTime)
        {
            return _context.Tasks.Where(t => t.CreationTime == creationTime).ToList();
        } */
        
        public async Task<bool> AddComment(Guid taskId, string newComment)
        {
            TaskModel task = await _context.Tasks.Include("AssignedEmployee").FirstOrDefaultAsync(t =>
                t.Id == taskId);
            if (task == null)
                return false;
            var taskChange = new TaskChange(Guid.NewGuid(), task, DateTime.Now, "add comment", task.AssignedEmployee.Id);
            if (!String.IsNullOrEmpty(newComment))
                task.Comment = newComment;
            await _context.TaskChanges.AddAsync(taskChange);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatus(Guid taskId, TaskType.Type newStatus)
        {
            TaskModel task = await _context.Tasks.Include("AssignedEmployee").FirstOrDefaultAsync(t =>
                t.Id == taskId);
            if (task == null)
                return false;
            var taskChange = new TaskChange(Guid.NewGuid(), task, DateTime.Now, "update status",
                task.AssignedEmployee.Id);
            task.Status = newStatus;
            await _context.TaskChanges.AddAsync(taskChange);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> UpdateAssignedEmployee(Guid taskId, Guid newEmployeeId)
        {
            TaskModel task = await _context.Tasks.Include("AssignedEmployee").FirstOrDefaultAsync(t =>
                t.Id == taskId);
            if (task == null)
                return false;
            var taskChange = new TaskChange(Guid.NewGuid(), task, DateTime.Now, "update assigned employee",
                newEmployeeId);
            Employee employee = _context.Employees.FirstOrDefaultAsync(e => e.Id == newEmployeeId).Result;
            task.AssignedEmployee = employee;
            await _context.TaskChanges.AddAsync(taskChange);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TaskModel>> GetTasksByAssignedEmployee(Guid employeeId)
        {
            return _context.Tasks.Include("AssignedEmployee").Where(t =>
                t.AssignedEmployee.Id == employeeId).ToList();
        }

        public async Task<List<TaskModel>> GetUpdateTasksByEmployee(Guid employeeId)
        {
            var changes = _context.TaskChanges.Include("").Where(c =>
                c.EmployeeId == employeeId).ToList();
            var tasks = new List<TaskModel>();
            foreach (TaskChange change in changes)
            {
                if (change.EmployeeId == employeeId)
                    tasks.Add(change.PreviousTask);
            }

            return tasks;
        }

        public async Task<TaskModel> Delete(Guid id)
        {
            TaskModel task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return task;
        }
    }
}