using System;
using System.Collections.Generic;
using System.Linq;
using project_BD.Models.Enums;
using System.Data.Entity;

namespace project_BD.Models.Classes
{
    public class DB_Control
    {
        public DB_Context _db = new DB_Context();
        private Employee _employee = new Employee();
        private Company _company = new Company();
        private Project _project = new Project();
        private Employees_in_Project _employees_in_project = new Employees_in_Project();

        //adding an employee to the database
        public void Add_employee(Employee employee)
        {
            _db._employees.Add(new Employee(employee._family, employee._name, employee._patronymic, employee._email, employee._skill));
            _db.SaveChanges();
        }
        //get a list of skills of employees
        public string[] Get_enums_skills()
        {
            string[] enums;
            Array enums_items;

            enums = new string[Enum.GetValues(typeof(Skill)).Length];
            enums_items = Enum.GetValues(typeof(Skill));
            for (int i = 0; i < Enum.GetValues(typeof(Skill)).Length; i++)
            {
                enums[i] = enums_items.GetValue(i).ToString();
            }
            return enums;
        }
        //getting a list of employees from the database
        public IEnumerable<Employee> Get_list_employees()
        {
            IEnumerable<Employee> employees = _db._employees;
            return employees;
        }
        //removing an employee from the database
        public void Del_employee(int id)
        {
            if (id>0)
            {
                Employee employee = Get_info_employee(id);
                _db._employees.Remove(employee);
                _db.SaveChanges();
            }
        }
        //change information about an employee in the database
        public void Edit_employee(int id,Employee employee)
        {
            _db._employees.Load();
            Employee edit_employee = _db._employees.Find(id);
            edit_employee._family = employee._family;
            edit_employee._name = employee._name;
            edit_employee._patronymic = employee._patronymic;
            edit_employee._email = employee._email;
            edit_employee._skill = employee._skill;
            _db.SaveChanges();
        }
        //getting information about an employee
        public Employee Get_info_employee(int id)
        {
            List<Employee> employee = _db._employees.Where(e => e._id == id).ToList();            
            return employee[0];
        }
        //adding a company
        public void Add_company(Company company)
        {
            _db._companies.Add(new Company(company._name, company._type));
            _db.SaveChanges();
        }
        //getting a list of companies
        public IEnumerable<Company> Get_list_companies(Type_company type)
        {
            IEnumerable<Company> companies = _db._companies.Where(c => c._type == type);
            return companies;
        }
        //getting a list of employees with skill lead, senior
        public IEnumerable<Employee> Get_top_employee()
        {
            IEnumerable<Employee> employees = _db._employees.Where(e => (e._skill == Skill.Lead || e._skill == Skill.Senior));
            return employees;
        }
        //adding a project
        public void Add_project(Project project)
        {
            _db._projects.Add(new Project(project._name, project._id_lead, project._id_company_custoner, project._id_company_performer, 
                project._date_begin, project._date_end, project._priority,project._comment));
            _db.SaveChanges();
        }
        //getting a list of projects
        public IEnumerable<Project> Get_list_projects(int sort)
        {
            IEnumerable<Project> projects = _db._projects;
            switch (sort)
            {
                case 1:
                    {
                        projects = _db._projects.OrderBy(p=>p._name);
                        break;
                    }
                case 2:
                    {
                        projects = _db._projects.OrderBy(p => p._date_begin);
                        break;
                    }
                case 3:
                    {
                        projects = _db._projects.OrderBy(p => p._date_end);
                        break;
                    }
                case 4:
                    {
                        projects = _db._projects.OrderBy(p => p._priority);
                        break;
                    }
            }            
            return projects;
        }
        //delete project
        public void Del_project(int id)
        {
            if (id > 0)
            {
                Project project = Get_info_project(id);
                _db._projects.Remove(project);
                _db.SaveChanges();
                List<Employees_in_Project> employees_in_projects = _db._employees_in_project.Where(p => p._id_project == id).ToList();
                if (employees_in_projects.Count > 0)
                {
                    for (int i = 0; i < employees_in_projects.Count; i++)
                    {
                        _db._employees_in_project.Remove(employees_in_projects[i]);
                    }
                    _db.SaveChanges();
                }
            }
        }
        //filtering the list of projects
        public IEnumerable<Project> Filter_project(IEnumerable<Project> list_projects, Project_filter filter)
        {
            IEnumerable<Project> temp = list_projects;
            if (filter._prior!=-1)
                temp = temp.Where(p => p._priority == filter._prior);
            if (filter._lead!=0)
                temp = temp.Where(p => p._id_lead == filter._lead);
            if (filter._from != DateTime.ParseExact("00010101", "yyyyMdd", null) && filter._befor != DateTime.ParseExact("00010101", "yyyyMdd", null))
                temp = temp.Where(p => p._date_begin >= filter._from && p._date_begin <= filter._befor);

            //list_projects.Where(p => p._date_begin >= filter._from && p._date_begin <= filter._befor);

            return temp;
        }
        //getting information about the project
        public Project Get_info_project(int id)
        {
            List<Project> project = _db._projects.Where(p => p._id == id).ToList();
            return project[0];
        }
        //add 0 to a number if it is less than 10
        public string Transform_number(string num)
        {
            if (num.Length == 1)
                return "0" + num;
            else
                return num;            
        }
        //getting a list of employees in the project
        public List<Employee> Get_employees_in_project(int id)
        {
            List<Employee> emp = new List<Employee>();
            List<Employee> employees = Get_list_employees().ToList();
            List<Employees_in_Project> num_employees = _db._employees_in_project.Where(p => p._id_project == id).ToList();
            for (int i = 0; i < num_employees.Count; i++)
            {
                for (int j = 0; j < employees.Count; j++)
                {
                    if (num_employees[i]._id_employee==employees[j]._id)
                    {
                        emp.Add(employees[j]);
                    }
                }
            }
            return emp;
        }
        //geting a list of employees not related to the project
        public List<Employee> Get_employees_not_in_project(int id)
        {
            List<Employee> emp = new List<Employee>();
            Project projects = Get_info_project(id);
            List<Employee> employees = Get_list_employees().Where(e=>e._id!=projects._id_lead).ToList();            
            List<Employees_in_Project> num_employees = _db._employees_in_project.Where(p => p._id_project == id).ToList();
            if (num_employees.Count != 0)
            {
                for (int i = 0; i < employees.Count; i++)
                {
                    for (int j = 0; j < num_employees.Count; j++)
                    {
                        if (employees[i]._id == num_employees[j]._id_employee)
                        {
                            employees.Remove(employees[i]);
                        }
                    }
                }
                return employees;
            }
            else
            return employees;
        }
        //adding an employee to the project
        public void Add_employees_in_project(int id,int employee)
        {
            _db._employees_in_project.Add(new Employees_in_Project(id, employee));
            _db.SaveChanges();
        }
        //remove an employee from the project
        public void Del_employee_in_project(int id,int id_employee)
        {
            if (id != 0 && id_employee != 0)
            {
                Employees_in_Project employees_in_project = _db._employees_in_project.Where(p => p._id_project == id).Where(e => e._id_employee == id_employee).ToList()[0];
                _db._employees_in_project.Remove(employees_in_project);
                _db.SaveChanges();
            }
        }
        //change information about the project
        public void Edit_project(int id,Project project)
        {
            _db._projects.Load();
            Project edit_project = _db._projects.Find(id);
            edit_project._name=project._name;
            edit_project._id_lead = project._id_lead;
            edit_project._id_company_custoner = project._id_company_custoner;
            edit_project._id_company_performer = project._id_company_performer;
            edit_project._date_end = project._date_end;
            edit_project._priority = project._priority;
            edit_project._comment = project._comment;
            _db.SaveChanges();
        }
    }
}