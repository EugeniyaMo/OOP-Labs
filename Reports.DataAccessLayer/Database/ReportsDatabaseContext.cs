using System;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Entities;

namespace Reports.Server.Database
{
    public class ReportsDatabaseContext : DbContext
    {
        public ReportsDatabaseContext(DbContextOptions<ReportsDatabaseContext> options) : base(options)
        {
            // this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<TaskChange> TaskChanges { get; set; }
        public DbSet<Report> Reports { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<TaskModel>().HasOne(model => model.AssignedEmployee);
            modelBuilder.Entity<TaskChange>().HasOne(model => model.PreviousTask);
            base.OnModelCreating(modelBuilder);
        }

        private Employee GetEmployeeById(Guid id)
        {
            foreach (var employee in Employees)
            {
                if (employee.Id == id)
                    return employee;
            }

            return null;
        }
    }
}