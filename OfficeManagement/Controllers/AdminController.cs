using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
namespace OfficeManagement.Controllers
{
    public class AdminController : Controller
    {
        EmployeeDbContext context = new EmployeeDbContext();


        public ActionResult Index()
        {
            if (Session["ADMINUSERNAME"] != null)
            {
                return View();
            }
            else {
                return RedirectToAction("Signin","Home");
            }
            return View();
        }
        public ActionResult OfficeMangement()
        {
            return View();
        }


        [HttpGet]
        public ActionResult EmployeeDetails()
        {
            return View(context.Employees.ToList());
        }
        [HttpPost]
        public ActionResult EmployeeDetails(String searchname)
        {
            List<Employee> employees;

            if (String.IsNullOrEmpty(searchname))
            {
                employees = context.Employees.ToList();
            }
            else
            {
                employees = context.Employees.Where(x => x.Employee_name.StartsWith(searchname)).ToList();

            }
            return View(employees);
        }

        public JsonResult GetEmployeeName(string term)
        {
            List<string> employees;

            employees = context.Employees.Where(x => x.Employee_name.StartsWith(term))
                                     .Select(y => y.Employee_name).ToList();


            return Json(employees, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            Employee emp = context.Employees.SingleOrDefault(e => e.Employee_id == id);
            return View(emp);
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.deptid = new SelectList(context.Departments, "Department_id", "Department_name");
            return View();
        }


        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                context.Employees.Add(emp);
                context.SaveChanges();
                return RedirectToAction("EmployeeDetails");
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.deptid = new SelectList(context.Departments, "Department_id", "Department_name");
            Employee emp = context.Employees.SingleOrDefault(e => e.Employee_id == id);
            return View(emp);
        }


        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            try
            {
                Employee employee_edit = context.Employees.SingleOrDefault(e => e.Employee_id == emp.Employee_id);
                employee_edit.Employee_name = emp.Employee_name;
                employee_edit.Employee_email = emp.Employee_email;
                employee_edit.Employee_address = emp.Employee_address;
                employee_edit.Phone_number = emp.Phone_number;
                employee_edit.Employee_Type = emp.Employee_Type;

                employee_edit.Department_id = emp.Department_id;

                employee_edit.Employee_password = emp.Employee_password;


                context.SaveChanges();
                return RedirectToAction("EmployeeDetails");
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Employee emp = context.Employees.SingleOrDefault(e => e.Employee_id == id);
            return View(emp);
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_emp(int id)
        {
            try
            {
                Employee emp_delete = context.Employees.SingleOrDefault(e => e.Employee_id == id);
                context.Employees.Remove(emp_delete);
                context.SaveChanges();
                return RedirectToAction("EmployeeDetails");
            }
            catch
            {
                return View();
            }
        }

        /////////////////////// Department Panel///////////////////////////// 

        public ActionResult DepartmentList()
        {

            return View(context.Departments.ToList());
        }

        public ActionResult DepartmentDetails(int id)
        {
            Department dept = context.Departments.SingleOrDefault(d => d.Department_id == id);
            return View(dept);
        }


        [HttpGet]
        public ActionResult DepartmentCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DepartmentCreate(Department dept)
        {
            try
            {

                context.Departments.Add(dept);
                context.SaveChanges();
                return RedirectToAction("DepartmentList");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DepartmentEdit(int id)
        {
            ViewBag.deptid = new SelectList(context.Departments, "Department_id", "Department_name");
            Department dept = context.Departments.SingleOrDefault(d => d.Department_id == id);
            return View(dept);
        }

        [HttpPost]
        public ActionResult DepartmentEdit(Department dept)
        {
            try
            {
                Department Department_edit = context.Departments.SingleOrDefault(d => d.Department_id == dept.Department_id);
                Department_edit.Department_name = dept.Department_name;

                context.SaveChanges();
                return RedirectToAction("DepartmentList");
            }
            catch
            {
                return View();
            }
        }

        
        //////////////////////Voter Section /////////////////////////////////////      


        public ActionResult VoteSection()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditVoterDepartment()
        {
            
            ViewBag.deptid = new SelectList(context.Departments, "Department_id", "Department_name");
            return View(context.Employees.ToList());
        }

        [HttpPost]
        public ActionResult EditVoterDepartment(string deptid)
        {
            ViewBag.deptid = new SelectList(context.Departments, "Department_id", "Department_name");

            ViewBag.messageString = deptid;

            var employees = (from emp in context.Employees
                             where emp.Department_id.ToString() == deptid 
                             select emp).ToList();
            foreach (var emp_edit in employees)
            {
                emp_edit.Voter_flag = 1;
            }
            context.SaveChanges();


            var voteremployee = (from emp in context.Employees
                                 where emp.Voter_flag == 1
                                 select emp).ToList();

            return View(voteremployee);


        }


        //logout
        public ActionResult Logout()
        {
            Session["ADMINUSERNAME"] = null;
            return RedirectToAction("Signin", "Home");
        }


        /////////////////////Profile///////////
        [HttpGet]
        public ActionResult InnerEdit()
        {
            int a = Convert.ToInt32(Session["ADMINUSERNAME"]);
            // TODO: Add update logic here
            Employee emp = context.Employees.SingleOrDefault(e => e.Employee_id == a);
            return View(emp);
        }

        [HttpPost]
        public ActionResult InnerEdit(Employee emp)
        {
            try
            {
                Employee employee_edit = context.Employees.SingleOrDefault(e => e.Employee_id == emp.Employee_id);
                employee_edit.Employee_name = emp.Employee_name;
                employee_edit.Employee_email = emp.Employee_email;
                employee_edit.Employee_address = emp.Employee_address;
                employee_edit.Phone_number = emp.Phone_number;
                employee_edit.Employee_Type = emp.Employee_Type;

                employee_edit.Department_id = emp.Department_id;

                employee_edit.Employee_password = emp.Employee_password;


                context.SaveChanges();
                return RedirectToAction("AdminProfile");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// ////////////////////////////////voter candidate
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult SelectCandidates(string searchname)
        {
            List<Employee> employees;

            if (String.IsNullOrEmpty(searchname))
            {
                employees = context.Employees.ToList();
            }
            else
            {
                employees = context.Employees.Where(x => x.Employee_name.StartsWith(searchname)).ToList();

            }
            return View(employees);

            
        }
        [HttpGet]
        public ActionResult Add(int id)
        {
            //int a = Convert.ToInt32(Session["ADMINUSERNAME"]);
            // TODO: Add update logic here
            Employee emp = context.Employees.SingleOrDefault(e => e.Employee_id == id);
            return View(emp);
        }
        [HttpPost]
        public ActionResult Add(Employee emp)
        {
            Employee empToUpd = context.Employees.SingleOrDefault(e => e.Employee_id == emp.Employee_id);

            empToUpd.Employee_id = emp.Employee_id;
            empToUpd.Employee_name = emp.Employee_name;
            empToUpd.Candidate_flag = emp.Candidate_flag;
            empToUpd.Department_id = emp.Department_id;
            context.SaveChanges();
            return RedirectToAction("SelectCandidates");
        }

        public ActionResult ShowCandidates()
            {
                var employees = (from emp in context.Employees
                                 where emp.Candidate_flag == 1
                                 select emp).ToList();

                return View(employees);


            }
        public ActionResult ShowResult()
            {
                var employees = (from emp in context.Employees
                                 where emp.Candidate_flag == 1
                                 select emp).ToList();
                return View(employees);
            }

        [HttpGet]
        public ActionResult ShowFinalResult()
        {
            int a = context.Employees.Max(e => e.Count);
            var employees = (from emp in context.Employees
                             where emp.Count == a && emp.Candidate_flag==1
                             select emp).ToList();
            return View(employees);
        }
        public ActionResult AdminProfile()
        {
            int a = Convert.ToInt32(Session["ADMINUSERNAME"]);
            Employee emp = context.Employees.SingleOrDefault(e => e.Employee_id == a);
            return View(emp);
        }
        [HttpPost]
        public ActionResult ShowFinalResult(Employee empo)
        {
           
            var empSetFinalFlag= (from emp in context.Employees
                           where emp.FinalResult_flag==0
                            select emp).ToList();
            foreach (var emptoEdit in context.Employees)
            {
                emptoEdit.FinalResult_flag = 1;
                emptoEdit.Voter_flag = 0;
                
                
            }
            context.SaveChanges();
          
            var anyName= (from emp in context.Employees
                           where emp.FinalResult_flag==0
                            select emp).ToList();
            return RedirectToAction("ShowFinalResult");
        }

        public ActionResult Reset()

        {

            var empSetFinalFlag = (from emp in context.Employees
                                   where emp.FinalResult_flag == 1
                                   select emp).ToList();
            foreach (var emptoEdit in context.Employees)
            {
                emptoEdit.FinalResult_flag = 0;
                emptoEdit.Voter_flag = 0;
                emptoEdit.Candidate_flag = 0;
                emptoEdit.Count = 0;

            }
            context.SaveChanges();


            return View();

        }
    }
}