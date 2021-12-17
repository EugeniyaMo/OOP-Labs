using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Entities
{
    public class TaskChange
    {
        public Guid Id { get; set; }
        public TaskModel PreviousTask { get; set; }
        public DateTime CreationTime { get; set; }
        public string Comment { get; set; }
        public Guid EmployeeId { get; set; }

        public TaskChange(Guid id, TaskModel task, DateTime creationTime, string comment, Guid employeeId)
        {
            Id = id;
            PreviousTask = task;
            CreationTime = creationTime;
            Comment = comment;
            EmployeeId = employeeId;
        }
        
        private TaskChange()
        {}

    }
}