using EmpDA;
using EmpDA.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPIs.InterFacesForDI;

namespace WebAPIs.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class Employees : ControllerBase
    {
        private readonly IConnectSQL connect;
        private readonly ILogger<Employees> logger;

        public Employees(IConnectSQL _connect, ILogger<Employees> _logger)
        {
            this.connect = _connect;
            this.logger = _logger;
        }
        [HttpPost("NewEmp")]
        [ProducesResponseType(typeof(JsonResult),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult),StatusCodes.Status500InternalServerError)]
        public JsonResult NewEmp(EmpModel Emp)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    logger.LogInformation("make sure of all required fields please");
                    return new JsonResult(BadRequest(Emp));
                }
                else
                {
                    EmpOperations EO = new EmpOperations(connect.Connect());
                    int IsSuccess = EO.NewEmp(Emp);
                    if(IsSuccess ==0 || IsSuccess==-1)
                    {
                        logger.LogInformation("Please Check All input fields, Perhaps the employee already exist or the team name is not valid");
                        return new JsonResult(BadRequest("Please Check All input fields, Perhaps the employee already exist or the team name is not valid"));
                    }
                    if(IsSuccess==-2)
                    {
                        logger.LogInformation("Something went wrong with SQL ");
                        return new JsonResult(StatusCodes.Status500InternalServerError);
                    }
                    if(IsSuccess>0)
                    {
                        logger.LogInformation("The Employee has been added");
                        return new JsonResult(Ok("The new employee has been added"));
                    }
                    else
                    {
                        logger.LogInformation("Internal server error, check with administrator");
                        return new JsonResult(StatusCodes.Status500InternalServerError);
                    }
                } 
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message);
                return new JsonResult(ex.Message);
            }
        }


        [HttpDelete("DeleteEmp")]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status500InternalServerError)]
        public JsonResult DeleteEmp(EmpDTO Emp)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return new JsonResult(BadRequest("Please enter a valid employee Id, name and team name"+Emp));
                }
                else
                {
                    EmpOperations EO= new EmpOperations(connect.Connect());
                    int IsSuccess=EO.DeleteEmp(Emp);
                    if(IsSuccess>0)
                    {
                        logger.LogInformation("Deleted");
                        return new JsonResult(Ok("Deleted"));
                    }
                    else if(IsSuccess==-1)
                    {
                        logger.LogInformation("Please check employee's Id, name and team name");
                        return new JsonResult(Ok("Please check employee's Id, name and team name"));
                    }
                    else if(IsSuccess==-2)
                    {
                        logger.LogInformation("Internal server error");
                        return new JsonResult(StatusCodes.Status500InternalServerError);
                    }
                    else
                    {
                        logger.LogInformation("Something went wrong, call system adminstrato");
                        throw new Exception("Something went wrong, call system adminstrato");
                    }

                        
                }
            }
            catch(Exception ex) 
            {
                logger.LogInformation("An exception has been occoured "+ex.Message);
                return new JsonResult("An exception has been occoured " + ex.Message);
            }
        }
    }
}
