using System.Web.Mvc;
using project_BD.Models.Classes;
using project_BD.Models.Enums;

namespace project_BD.Controllers
{
    public class HomeController : Controller
    {
        public DB_Control _control = new DB_Control();
        //--------------------------------------------------
        public ActionResult Index()
        {
            Project_filter project_filter = new Project_filter();
            project_filter._prior = -1;
            Session["filter"] = project_filter;

            return View();
        }
        //--------------------------------------------------
        [HttpGet]
        public ActionResult Employees(int id=0)
        {
            _control.Del_employee(id);
            Employees_get_post();

            return View();
        }
        
        public ActionResult Employees()
        {
            Employees_get_post();
            return View();
        }

        [HttpPost]
        public ActionResult Employees(Employee employee)
        {
            _control.Add_employee(employee);
            Employees_get_post();

            return View();
        }

        public void Employees_get_post()
        {
            ViewBag.Skills = _control.Get_enums_skills();
            ViewBag.Employees = _control.Get_list_employees();
        }
        //--------------------------------------------------
        [HttpGet]
        public ActionResult Employee(int id)
        {
            ViewBag.Employee = _control.Get_info_employee(id);
            ViewBag.Skills = _control.Get_enums_skills();

            return View();
        }

        [HttpPost]
        public ActionResult Employee(Employee employee,int id=0)
        {
            _control.Edit_employee(id, employee);
            Employees_get_post();

            return View("~/Views/Home/Employees.cshtml");
        }
        //--------------------------------------------------
        [HttpGet]
        public ActionResult Projects(Project_filter filter, int id=0,int id_project=0, int filtering=0)
        {
            _control.Del_project(id_project);
            Projects_get_post();
            if (filtering == 0)
            {
                if (Session["filter"] != null)
                    filter = (Project_filter)Session["filter"];
                else
                    filter._prior = -1;
            }                
            else
            {
                Session["filter"] = filter;
            }
            ViewBag.Projects = _control.Filter_project(_control.Get_list_projects(id),filter);

            return View();
        }

        [HttpPost]
        public ActionResult Projects(Project project )
        {
            _control.Add_project(project);
            Projects_get_post();
            ViewBag.Projects = _control.Get_list_projects(0);
            
            return View();
        }

        public void Projects_get_post()
        {
            ViewBag.Top = _control.Get_top_employee();
            ViewBag.Customers = _control.Get_list_companies(0);
            ViewBag.Performer = _control.Get_list_companies((Type_company)1);            
            ViewBag.Employees = _control.Get_list_employees();
        }
        //--------------------------------------------------
        [HttpGet]
        public ActionResult Companies(Type_company id=0, int id_company=0)
        {
            _control.Del_company(id_company);
            Companies_get_post(id);

            return View();
        }

        [HttpPost]
        public ActionResult Companies(Company company, Type_company id = 0)
        {
            _control.Add_company(company);
            Companies_get_post(id);

            return View();
        }

        public void Companies_get_post(Type_company id = 0)
        {
            ViewBag.Title = id;
            ViewBag.Companies = _control.Get_list_companies(id);
        }
        //--------------------------------------------------
        [HttpGet]
        public ActionResult Project(int id = 0, int id_employee = 0)
        {
            _control.Del_employee_in_project(id, id_employee);
            Project_get_post(id, id_employee);
            
            return View();
        }

        [HttpPost]
        public ActionResult Project(string rec,Project project,int employee=0,int id_employee=0, int id=0)
        {
            string str = "";
            if (rec == "save")
            {
                _control.Edit_project(id, project);
                Projects_get_post();
                str = "~/Views/Home/Projects.cshtml";
            }                
            if (rec == "add")
            {
                _control.Add_employees_in_project(id, employee);
                Project_get_post(id, id_employee);
            }
            return View(str);
        }

        public void Project_get_post(int id, int id_employee)
        {
            ViewBag.Project = _control.Get_info_project(id);

            string num = _control.Get_info_project(id)._date_begin.Month.ToString();
            ViewBag.Month_begin = _control.Transform_number(num);
            num = _control.Get_info_project(id)._date_begin.Day.ToString();
            ViewBag.Day_begin = _control.Transform_number(num);
            num = _control.Get_info_project(id)._date_end.Month.ToString();
            ViewBag.Month_end = _control.Transform_number(num);
            num = _control.Get_info_project(id)._date_end.Day.ToString();
            ViewBag.Day_end = _control.Transform_number(num);

            ViewBag.Employees = _control.Get_employees_not_in_project(id);
            ViewBag.Top = _control.Get_top_employee();
            ViewBag.Customers = _control.Get_list_companies(0);
            ViewBag.Performer = _control.Get_list_companies((Type_company)1);

            ViewBag.Employee_in_project = _control.Get_employees_in_project(id);
        }
    }
}