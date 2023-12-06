using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDA.Model
{
    public class EmpModel
    {
        public int EmpId { get; private set; }

        [Required(ErrorMessage ="Please Enter employye's first name")]
        [MinLength(3,ErrorMessage ="please Enter a valid employee name")]
        [MaxLength(15,ErrorMessage = "please Enter a valid employee name")]
        public string FirstName { get; private set; }

        [Required(ErrorMessage = "Please Enter employye's last name")]
        [MinLength(3, ErrorMessage = "please Enter a valid employee name")]
        [MaxLength(15, ErrorMessage = "please Enter a valid employee name")]
        public string LastName { get; private set; }


        [Required(ErrorMessage =("Please Enter employye's salary"))]
        [Range(260,4000,ErrorMessage ="The salary value is not valid, Consider the lowest and highest salary for your employee")]
        public decimal Salary { get; private set; }

        [Required(ErrorMessage ="Please enter The team name that the employee works in")]
        [MinLength(2, ErrorMessage = "please Enter a valid team name")]
        [MaxLength(20, ErrorMessage = "please Enter a valid team name")]
        public string WorkingOn { get; private set; }
        public EmpModel(string firstName, string lastName, decimal salary, string workingOn)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
            this.WorkingOn = workingOn;
        }
    }
}
