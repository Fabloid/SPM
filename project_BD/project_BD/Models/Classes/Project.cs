using System;
using System.ComponentModel.DataAnnotations;

namespace project_BD.Models.Classes
{
    public class Project
    {
        [Key]
        public int _id { get; set; }
        public string _name { get; set; }
        public int _id_lead { get; set; }
        public int _id_company_custoner { get; set; }
        public int _id_company_performer { get; set; }
        public DateTime _date_begin { get; set; }
        public DateTime _date_end { get; set; }
        public int _priority { get; set; }
        public string _comment { get; set; }

        public Project() { }
        public Project(string name, int id_lead, int id_company_custoner, int id_company_performer, DateTime date_begin, DateTime date_end,int priority, string comment)
        {
            _name = name;
            _id_lead = id_lead;
            _id_company_custoner = id_company_custoner;
            _id_company_performer = id_company_performer;
            _date_begin = date_begin;
            _date_end = date_end;
            _priority = priority;
            _comment = comment;
        }        
    }
}