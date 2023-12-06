using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TeamsDA.Model
{
    public class TeamsModel
    {
        [Required]
        public int TeamId {  get; private set; }

        [Required(ErrorMessage = "The Team name is required.")]
        [MinLength(5,ErrorMessage = "The minimum length of team name should be 5 ")]
        [MaxLength(55,ErrorMessage = "The max length of team name should be 55 ")]
        public string TeamName { get; private set; }

        [MinLength(5, ErrorMessage = "The minimum length of team name should be 5 ")]
        [MaxLength(55, ErrorMessage = "The max length of team name should be 55 ")]
        public string? ChangeTeamName { get; private set; }

        [Required]
        public int NumberOfTeamMembers { get; private set; }

        [Required]
        public int NumberOfProjects { get; private set; }

        public TeamsModel(string TeamName, string ChangeTeamName, int NumberOfTeamMembers, int NumberOfProjects)
        {
            this.TeamId = TeamId;
            this.TeamName = TeamName;
            this.NumberOfTeamMembers = NumberOfTeamMembers;
            this.NumberOfProjects = NumberOfProjects;
        }

    }
}
