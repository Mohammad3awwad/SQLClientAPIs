using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using TeamsDA;
using TeamsDA.Model;
using WebAPIs.InterFacesForDI;

namespace WebAPIs.Controllers
{
    [Route("api/MyTeams")]
    [ApiController]
    public class Teams : ControllerBase
    {
        private readonly IConnectSQL connectSQL;
        private readonly ILogger<Teams> logger;
        public Teams(IConnectSQL _connectSQL, ILogger<Teams> _logger)
        {
            this.connectSQL = _connectSQL;
            this.logger = _logger;
        }

        [HttpGet("AllTeams")]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status404NotFound)]
        public JsonResult getTeams()
        {
            IEnumerable <TeamsDTO> teams= new List<TeamsDTO>();
            try
            {
                TeamsOperations TO = new TeamsOperations(connectSQL.Connect());
                
                teams = TO.AllTeams();
                if (teams.Count() == 0)
                {
                    logger.LogInformation("No teams found");
                    TeamsDTO FirstElement = new TeamsDTO(0," No Teams found", 0,0);
                    logger.LogInformation("No data found...");
                    return new JsonResult(NotFound(FirstElement));
                }
                else
                {
                    logger.LogInformation("retrieved a list of teams");
                    return new JsonResult(Ok(teams));
                }
            }
            catch(Exception ex) 
            {
                logger.LogError(ex.Message);
                return new JsonResult(ex.Message);
            }
        }


        [HttpPost("AddATeam")]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status400BadRequest)]
        public JsonResult AddTeam(TeamsModel teams)
        {
            try
            {
                logger.LogInformation("Begin Inserting to the database...");
                if (!ModelState.IsValid)
                {
                    logger.LogInformation("Not All inputs are correct...");
                    return new JsonResult("Please check all input fields....");
                }
                else
                {
                    TeamsOperations TO = new TeamsOperations(connectSQL.Connect());
                    int IsSuccess = TO.NewTeam(teams);
                    if (IsSuccess == 0)
                    {
                        logger.LogInformation("The team's name already exist");
                        return new JsonResult("The new team can't be added, please check if the team's name already exist");
                    }
                    if (IsSuccess == 1)
                    {
                        logger.LogInformation("The team has been added");
                        return new JsonResult("The team has been added");
                    }
                    else
                    {
                        return new JsonResult(BadRequest("Please check if the team name is already exist"));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation("An exception has been occoured");
                return new JsonResult(BadRequest(ex.Message));
            }
        }


        [HttpPut("ChangeTeamName")]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status404NotFound)]
        public JsonResult UpdateTeam(TeamsModel teams)
        {
            try
            {
                if (!ModelState.IsValid || string.IsNullOrEmpty(teams.ChangeTeamName.ToString()))
                {
                    logger.LogInformation("The Team Name field was empty or ...");
                    return new JsonResult(BadRequest("Please Enter a valid Team Name..."));
                }
                else 
                {
                    TeamsOperations TO = new TeamsOperations(connectSQL.Connect());
                    int IsSucces = TO.ChangeName(teams);
                    if(IsSucces == 0) 
                    {
                        logger.LogInformation("The team name you provided does not exist");
                        return new JsonResult(NotFound("The team name you provided does not exist"));
                    }
                    else if (IsSucces == 1) 
                    {
                        logger.LogInformation("The Team name has been changed...");
                        return new JsonResult(Ok("The Team name has been changed..."));
                    }
                    else if(IsSucces==-1)
                    {
                        logger.LogInformation("500, InternalServerError, you need SQL Team to see the issue");
                        return new JsonResult("500, InternalServerError, you need SQL Team to see the issue"); 
                    }
                    else
                    {
                        throw new Exception("An Exception has been occurred, Cal the SQL team");
                    }    
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return new JsonResult(ex.Message);
            }


        }


        [HttpDelete("DeleteAteam")]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status404NotFound)]
        public JsonResult DeleteTeam(TeamsModel teams)
        {
            int IsUccess = 0;
            try
            {
                if(!ModelState.IsValid)
                {
                    return new JsonResult(BadRequest("Check the team name..."));
                   
                }
                else
                {
                    TeamsOperations TO = new TeamsOperations(connectSQL.Connect());
                    IsUccess = TO.DeleteATeam(teams);
                    if(IsUccess==0) 
                    {
                        logger.LogInformation("The team does not exist...");
                        return new JsonResult(NotFound("The team does not exist..."));
                    }
                    else if(IsUccess>0)
                    {
                        logger.LogInformation("Deleted");
                        return new JsonResult(Ok("Deleted"));
                    }
                    else if (IsUccess ==-1)
                    {
                        logger.LogInformation("An exception occurred while deleting, call the system adminstrator and SQL team");
                        return new JsonResult(StatusCodes.Status500InternalServerError);
                    }
                    else
                    {
                        throw new Exception("Something went wrong in the SQL...");
                    }
                }
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
                return new JsonResult(ex.Message);
            }
        }

    } 
}
