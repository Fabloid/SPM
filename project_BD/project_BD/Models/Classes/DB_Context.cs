using System.Data.Entity;

namespace project_BD.Models.Classes
{
    public class DB_Context: DbContext
    {
        public DbSet<Employee> _employees { get; set; }
        public DbSet<Company> _companies { get; set; }
        public DbSet<Project> _projects { get; set; }
        public DbSet<Employees_in_Project> _employees_in_project { get; set; }
    }
}