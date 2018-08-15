using System.ComponentModel.DataAnnotations;
using project_BD.Models.Enums;

namespace project_BD.Models.Classes
{
    public class Employee
    {
        [Key]
        public int _id { get; set; }
        public string _family { get; set; }
        public string _name { get; set; }
        public string _patronymic { get; set; }
        public string _email { get; set; }
        public Skill _skill { get; set; }

        public Employee() { }
        public Employee(string family, string name, string patronymic, string email, Skill skill)
        {
            _family = family;
            _name = name;
            _patronymic = patronymic;
            _email = email;
            _skill = skill;
        }
    }
}