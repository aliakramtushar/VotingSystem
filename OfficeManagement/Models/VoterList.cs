using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;

namespace OfficeManagement.Models
{
    public class VoterList
    {
        public string SelectedVoter { get; set; }
        public List<Employee> Employees
        {
            get
            {
                EmployeeDbContext context = new EmployeeDbContext();
                var employees = (from emp in context.Employees
                                 where emp.Candidate_flag == 1
                                 select emp).ToList();
                return employees;
            }

        }
    }
}