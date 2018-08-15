using System.ComponentModel.DataAnnotations;

namespace project_BD.Models.Classes
{
    public class Employees_in_Project
    {
        [Key]
        public int _id { get; set; }
        public int _id_project { get; set; }
        public int _id_employee { get; set; }

        public Employees_in_Project() { }
        public Employees_in_Project(int id_project, int id_employee)
        {
            _id_project = id_project;
            _id_employee = id_employee;
        }
    }
}