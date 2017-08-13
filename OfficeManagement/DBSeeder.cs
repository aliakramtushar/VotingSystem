using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DataLayer;

namespace OfficeManagement
{
    public class DBSeeder : DropCreateDatabaseIfModelChanges<EmployeeDbContext>
    {
        protected override void Seed(EmployeeDbContext context)
        {
            

            Department dept = new Department()
            {
                Department_name = "Admin",
                Employees = new List<Employee>()
                {   
                    new Employee()
                    {
                        Employee_name = "Heron",
                        Employee_id    =101,
                        Employee_password="asd",
                        Employee_email="hheron0@gmail.com",
                        Employee_address="Dhaka",
                        Phone_number ="+8801749560934",
                        Employee_Type="Admin",
                        Candidate_flag=0,
                        Voter_flag=0,
                        FinalResult_flag=0,
                        Choise = false,
                        Count = 0,

                        Department_id= 1
                        

                        
                    }
                }
            };

           
            context.Departments.Add(dept);
            
            base.Seed(context);
        }
    }
}