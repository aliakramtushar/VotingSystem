using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace OfficeManagement.Controllers
{
    public class HomeController : Controller
    {
        EmployeeDbContext context = new EmployeeDbContext();
        public ActionResult Index()
        {
            context.Employees.ToList();
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signin(Employee emp)
        {
            if (context.Employees.SingleOrDefault(e => (e.Employee_id.ToString() == emp.Employee_name || e.Employee_email == emp.Employee_name) && e.Employee_password == emp.Employee_password && e.Employee_Type == "Admin") != null)
            {
  
                Session["ADMINUSERNAME"] = emp.Employee_name.ToString();
                return RedirectToAction("Index","Admin");
                
            }
            else if (context.Employees.SingleOrDefault(e => e.Employee_id.ToString() == emp.Employee_name && e.Employee_password == emp.Employee_password && e.Employee_Type == "Employee") != null)
            {
                Session["EMPLOYEEUSERNAME"] = emp.Employee_name.ToString();
                return RedirectToAction("EmployeeHome", "Employee");


            }

            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Employee()
        {
            return View();
        }
    }
}
