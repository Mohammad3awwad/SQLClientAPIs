using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsDA.Model
{
    public class TeamsDTO
    {
        public int TeamId { get;private set; }

        [Required(ErrorMessage = "The Team name is required.")]
        [MinLength(5, ErrorMessage = "The minimum length of team name should be 5 ")]
        [MaxLength(55, ErrorMessage = "The max length of team name should be 55 ")]
        public string? TeamName { get; private set; }

        [MinLength(5, ErrorMessage = "The minimum length of team name should be 5 ")]
        [MaxLength(55, ErrorMessage = "The max length of team name should be 55 ")]

        [Required]
        public int NumberOfTeamMembers {  get; private set; }

        [Required]
        public int NumberOfProjects {  get; private set; }

        public TeamsDTO(int teamId, string teamName, int numberOfTeamMembers, int numberOfProjects)
        {
            this.TeamId = teamId;
            this.TeamName = teamName;
            this.NumberOfTeamMembers = numberOfTeamMembers;
            this.NumberOfProjects = numberOfProjects;
        }
    }
}
