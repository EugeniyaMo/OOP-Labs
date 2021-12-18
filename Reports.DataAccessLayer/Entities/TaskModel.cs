using System;
using Reports.Entities;
using Reports.Tools;

namespace Reports.DAL.Entities
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public TaskType.Type Status { get; set; }

        public DateTime CreationTime { get; set; }
        public string Comment { get; set; }
        public Employee AssignedEmployee { get; set; }

        public TaskModel(Guid id, TaskType.Type status, DateTime creationTime, string comment, Employee assignedEmployee)
        {
            Id = id;
            Status = status;
            CreationTime = creationTime;
            Comment = comment;
            AssignedEmployee = assignedEmployee;
        }
        
        public TaskModel()
        {}
    }
}