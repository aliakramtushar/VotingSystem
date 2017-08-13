using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
     public class Department
    {
        [Key]
        public int Department_id { get; set; }

        [Required(ErrorMessage = "Department Name is Required"), MaxLength(100)]
        [Index(IsUnique = true)]
        public string Department_name { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
