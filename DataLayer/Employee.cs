using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Name Requird"), MaxLength(100)]
        public String Employee_name { get; set; }
        
        [Key]
        [Required(ErrorMessage = "User id Requird")]
        public int Employee_id { get; set; }

        [Required(ErrorMessage = "Password Requird"), MaxLength(100)]
        [DataType(DataType.Password)]
        public String Employee_password { get; set; }

        [Required(ErrorMessage = "Email Address Requird"), MaxLength(20)]
        [DataType(DataType.EmailAddress)]
        public String Employee_email { get; set; }
       

        [Required(ErrorMessage = "Address Requird"), MaxLength(100)]
        public String Employee_address { get; set; }

        

        [Required(ErrorMessage = "Phone Number Requird"), MaxLength(20)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(?:\+?88)?01[15-9]\d{8}$", ErrorMessage = "Entered phone format is not valid Please Enter +880 ")]
        public string Phone_number { get; set; }


        public String Employee_Type { get; set; }

        public int Candidate_flag { get; set; }

        public int Voter_flag { get; set; }

        public int FinalResult_flag { get; set; }

        public bool Choise { get; set; }

        public int Count { get; set; }
        

        [Required(ErrorMessage = "Department ID Requird")]
        public int Department_id { get; set; }
        [ForeignKey("Department_id")]




        public Department Department { get; set; }

    }
}
