using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsDA;
using ProjectsDA.Model;
using WebAPIs.InterFacesForDI;

namespace WebAPIs.Controllers
{
    [Route("api/Projects")]
    [ApiController]
    public class Projects : ControllerBase
    {
        private readonly IConnectSQL SqlCon;
        private readonly ILogger<Projects> logger;

        public Projects(IConnectSQL _SqlCon,ILogger<Projects> _logger)
        {
            this.SqlCon= _SqlCon;
            this.logger = _logger;
        }

        //You need to add the Clients before adding their projects 
        [HttpPost("NewProject")]
        [ProducesResponseType(typeof(JsonResult),StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(JsonResult),StatusCodes.Status200OK)]

        public JsonResult NewProject(ProjectsModel project)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new JsonResult(BadRequest(project));
                }
                else
                {
                    ProjectOperations PO = new ProjectOperations(SqlCon.Connect());
                    int IsSuccess = PO.newProject(project);
                    if (IsSuccess == 0 || IsSuccess == -1)
                    {
                        return new JsonResult(BadRequest("The data could not be inserted, check the project's info"));
                    }
                    if (IsSuccess > 0)
                    {
                        return new JsonResult(Ok("The project has been added"));
                    }
                    if(IsSuccess==-2)
                    {
                        return new JsonResult(StatusCodes.Status500InternalServerError+(" Internal Exception, check if the Client is exist or not"));
                    }
                    else
                    {
                        return new JsonResult(StatusCodes.Status500InternalServerError+ "An unknown error occurred. Please contact support.");
                    }
                }
            }
            catch (Exception ex) 
            {
                return new JsonResult(StatusCodes.Status500InternalServerError+ "Exception: " + ex.Message);
            }
        }
    }
}
