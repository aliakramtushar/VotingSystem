using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Web.UI.WebControls;
using OfficeManagement.Models;


namespace OfficeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        EmployeeDbContext context = new EmployeeDbContext();


        public ActionResult Employee()
        {

            return View();
        }

        public ActionResult EmployeeHome()
        {
            //var a = Convert.ToInt32(Session["EMPLOYEEUSERNAME"]);
            //var updateempflag = context.Employees.Where(e => e.Employee_id == a).FirstOrDefault();

            //updateempflag.Voter_flag = 0;
            //context.SaveChanges();

            return View();
            
            
            
        }
        public ActionResult EmployeeProfile()
        {
            int a = Convert.ToInt32(Session["EMPLOYEEUSERNAME"]);
            Employee emp = context.Employees.SingleOrDefault(e => e.Employee_id == a);
            return View(emp);
        }
        public ActionResult Index()
        {
            if (Session["EMPLOYEEUSERNAME"] !=null)
            {
                return View();
            }else{
                return RedirectToAction("Signin","Home");
            }
            return View();
        }

        public ActionResult Logout()
        {

            Session["EMPLOYEEUSERNAME"] = null;
            return RedirectToAction("Signin", "Home");
        }
        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Employee/Edit/5
        [HttpGet]
        public ActionResult Edit()
        {
            int a = Convert.ToInt32(Session["EMPLOYEEUSERNAME"]);
            // TODO: Add update logic here
            Employee emp = context.Employees.SingleOrDefault(e => e.Employee_id == a);
            return View(emp);
        }
        //
        // POST: /Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            int a = Convert.ToInt32(Session["EMPLOYEEUSERNAME"]);
            Employee employee_edit = context.Employees.SingleOrDefault(e => e.Employee_id == emp.Employee_id);
            employee_edit.Employee_name = emp.Employee_name;
            employee_edit.Employee_email = emp.Employee_email;
            employee_edit.Employee_address = emp.Employee_address;
            employee_edit.Phone_number = emp.Phone_number;
            employee_edit.Employee_password = emp.Employee_password;
            context.SaveChanges();
            return RedirectToAction("EmployeeProfile");

        }

        [HttpGet]
        public ActionResult VottingInterface()
        {
            int a = Convert.ToInt32(Session["EMPLOYEEUSERNAME"]);
            if (context.Employees.SingleOrDefault(e => (e.Employee_id == a) && e.Voter_flag == 1) != null)
            {
                VoterList voterlist = new VoterList();

                return View(voterlist);
            }
            else if (context.Employees.SingleOrDefault(e => (e.Employee_id == a) && e.Voter_flag == 0) != null)
            {

                return RedirectToAction("WhenNoVote");
               
            }
            return RedirectToAction("EmployeeHome");
        }

        public ActionResult WhenNoVote()
        {
            return View();
        
        }


        [HttpPost]
        public ActionResult VottingInterface(VoterList voterlist)
        {   
            
            if(string.IsNullOrEmpty(voterlist.SelectedVoter))
            {
                return RedirectToAction("VottingInterface");
                
            }
            else
            {
                var voter=Convert.ToInt32(voterlist.SelectedVoter);
                var updateEmployee = context.Employees.Where(e => e.Employee_id == voter).FirstOrDefault();

                updateEmployee.Count += 1;

                context.SaveChanges();


                var a = Convert.ToInt32(Session["EMPLOYEEUSERNAME"]);
                var updateempflag = context.Employees.Where(e => e.Employee_id == a).FirstOrDefault();

                updateempflag.Voter_flag = 0;
                context.SaveChanges();

                return View("Success");
            }
        }

        public ActionResult ShowResultFromEmployee()
        {
           
            int a = context.Employees.Max(e => e.Count);
            var employees = (from emp in context.Employees
                             where emp.Count == a && emp.FinalResult_flag == 1
                             select emp).ToList();
            return View(employees);
            
          //  return RedirectToAction("");
        }
       

    }
}
