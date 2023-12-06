using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsDA.Model
{
    public class ProjectsModel
    {
       public int ProjectId { get; private set; }

        [Required(ErrorMessage ="Enter the project name")]
        [MinLength(3,ErrorMessage =("The minumum length of the project's name is 3"))]
        [MaxLength(20,ErrorMessage =("The max length of the project's name is 20"))]
        public string ProjectName { get; private set; }

        [Required(ErrorMessage = "Enter a team name")]
        [MinLength(3, ErrorMessage = ("The minumum length of the team name is 5 "))]
        [MaxLength(20, ErrorMessage = ("The max length of the team name is 55"))]
        public string AssignedTo { get; private set; }

        [Required(ErrorMessage = "Enter a the started date")]
        public DateTime StartedDate { get; private set; }

        [Required(ErrorMessage = "Enter the project's status")]
        [MinLength(3, ErrorMessage = ("The minumum length of the team name is 5 "))]
        [MaxLength(20, ErrorMessage = ("The max length of the team name is 55"))]
        public string ProjectStatus { get;private set; }    

        public ProjectsModel(int ProjectId, string ProjectName,string AssignedTo,DateTime StartedDate,string ProjectStatus)
        {
            this.ProjectId = ProjectId;
            this.ProjectName = ProjectName;
            this.AssignedTo = AssignedTo;
            this.StartedDate = StartedDate;
            this.ProjectStatus = ProjectStatus;
        }
    }
}
