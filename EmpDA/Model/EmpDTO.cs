using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDA.Model
{
    public class EmpDTO
    {
        [Required(ErrorMessage ="Enter the employee's Id")]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Please Enter employye's first name")]
        [MinLength(3, ErrorMessage = "please Enter a valid employee name")]
        [MaxLength(15, ErrorMessage = "please Enter a valid employee name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter The team name that the employee works in")]
        [MinLength(2, ErrorMessage = "please Enter a valid team name")]
        [MaxLength(20, ErrorMessage = "please Enter a valid team name")]
        public string WorkingOn { get; set; }
    }
}
