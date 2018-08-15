using System.ComponentModel.DataAnnotations;
using project_BD.Models.Enums;

namespace project_BD.Models.Classes
{
    public class Company
    {
        [Key]
        public int _id { get; set; }
        public string _name { get; set; }
        public Type_company _type { get; set; }

        private DB_Context _db = new DB_Context();

        public Company() { }
        public Company(string name, Type_company type)
        {
            _name = name;
            _type = type;
        }
    }
}