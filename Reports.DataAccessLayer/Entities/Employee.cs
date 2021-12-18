using System;

namespace Reports.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Guid TeamLeadId { get; set; }
    
        private Employee()
        {
        }

        public Employee(Guid id, string name, Guid teamLeadId)
        {
            Id = id;
            Name = name;
            TeamLeadId = teamLeadId;
        }
    }
}