using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OfficeManagement.Controllers
{
    public class VottingController : Controller
    {
        //
        // GET: /Votting/
        EmployeeDbContext context = new EmployeeDbContext();

        public ActionResult Index()
        {
            

            return View();
        }


        public ActionResult Vote()
        {

            var models = context.Employees.Where(e => e.Candidate_flag == 1);

            return View(models);
        }

        
        public ActionResult Votter()
        {

            return null;

        }

    }
}
