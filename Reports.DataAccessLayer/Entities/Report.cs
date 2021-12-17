using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid EmployeeId { get; set; }
        public List<TaskModel> Tasks { get; set; }
        
        public Report()
        {}

        public Report(Guid id, DateTime creationTime, Guid employeeId, List<TaskModel> tasks)
        {
            Id = id;
            CreationTime = creationTime;
            EmployeeId = employeeId;
            Tasks = tasks;
        }
    }
}