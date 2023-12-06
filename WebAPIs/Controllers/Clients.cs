using ClientsDA;
using ClientsDA.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using WebAPIs.InterFacesForDI;

namespace WebAPIs.Controllers
{
    [Route("api/MyClients")]
    [ApiController]
    public class Clients : ControllerBase
    {
        private readonly IConnectSQL SQLCon;
        private readonly ILogger<Clients> logger;
        public Clients(IConnectSQL _SQLCon, ILogger<Clients> _logger)
        {
            this.SQLCon = _SQLCon;
            this.logger = _logger;
        }

        //The below endpoint to insert a new client
        [HttpPost("NewClient")]
        [ProducesResponseType(typeof(JsonResult),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult),StatusCodes.Status400BadRequest)]
        public JsonResult NewClient(ClientsModel clientsModel)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return new JsonResult("Please check your input...");
                }
                ClientsOperations clientsOperations = new ClientsOperations(SQLCon.Connect());
                int IfInserted = clientsOperations.InsertAClient(clientsModel); ;
                if(IfInserted == 0 || IfInserted==-1) 
                {
                    return new JsonResult("Please check if the Project Name already exist....");
                }
                else if(IfInserted >0) 
                {
                    return new JsonResult("All Done....");
                }
                else
                {
                    return new JsonResult("An Exception happend while inserting the data, please check the if the project name is already exist, or call the system adminstrator");
                }
            }
            catch (Exception ex) 
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        //The below EndPoind to update a Clien's Info
        [HttpPut("UpdateClientInfo")]
        [ProducesResponseType(typeof(JsonResult),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult),StatusCodes.Status400BadRequest)]
        public JsonResult UpdateClientInfo(ClientsModel CM)
        {
            if(!ModelState.IsValid) 
            {
                logger.LogError("The input Fields were not correct");
                return new JsonResult("Please check Input Fields...");
            }
            try
            {
                ClientsOperations OP = new ClientsOperations(SQLCon.Connect());
                int ResultValue = OP.UpdateClient(CM);
                logger.LogInformation("The data was sent to the server....");
                if(ResultValue==0)
                {
                    logger.LogInformation("The input was not correct");
                    return new JsonResult("Please check the Entered values...");
                }
                else if(ResultValue==1)
                {
                    logger.LogInformation("The Client's Info was updated successfuly");
                    return new JsonResult("Updated Successfuly...");
                }
                else
                {
                    throw new Exception("The Client is Not Found!");
                }
            }
            catch(Exception ex)
            {
                logger.LogError("An Exception occourd");
                return new JsonResult(BadRequest("An Exception occourd:" + ex.Message));
            }

        }
        
        //The below endpoint to retreive a specific client from SQL...
        [HttpGet("SpecificClient")]
        [ProducesResponseType(typeof(JsonResult),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult),StatusCodes.Status400BadRequest)]
        public JsonResult GetSpecificClient(int ClientId) 
        {
            logger.LogInformation("Looking for the client...");
            List<ClientsModel> clients = new List<ClientsModel>();    
            try
            {
                ClientsOperations CO = new ClientsOperations(SQLCon.Connect());
                clients = CO.GetAClient(ClientId);
                logger.LogInformation("Found a result without any exception(s)");
                return new JsonResult(clients);
            }
            catch(Exception ex)
            {
                logger.LogError("An exception occourd while looking for the client...");
                return new JsonResult(BadRequest("Something went wrong, please check the following Error Message: " + ex));
            }
        }


        //The below EndPoint Used to retreive all Clients from SQL
        [HttpGet("AllClients")]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status500InternalServerError)]
        public JsonResult GetAllClients()
        {
            logger.LogInformation("Looking for All Clients...");
            IEnumerable<ClientsModel> clients = new List<ClientsModel>();
            try
            {
                ClientsOperations clientsOp = new ClientsOperations(SQLCon.Connect());
                clients = clientsOp.GetAllClients();
                int ListValues = 0;
                ListValues = clients.Count();
                if(ListValues == 0)
                {
                    logger.LogError("Somethong went wrong while retrieving the Client's List");
                    return new JsonResult("Something went Wrong..");
                }
                logger.LogInformation("The List of client has been retrieved successfuly");
                return new JsonResult(clients);
            }
            catch(Exception ex) 
            {
                logger.LogError("An exception has been occour while retrieving the client's list");
               return new JsonResult(ex.Message);
            }


        }

        [HttpDelete("DeleteAClient")]
        [ProducesResponseType(typeof(JsonResult),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult),StatusCodes.Status400BadRequest)]
        public JsonResult DeleteClients(int ClientId) 
        {
            try
            {
                if (ClientId == 0)
                {
                    logger.LogError("Client Id is not valid");
                    return new JsonResult("Please Check your input....");
                }
                else
                {
                    ClientsOperations operations=new ClientsOperations(SQLCon.Connect());
                    int IsSuccess = operations.DeletAClient(ClientId);
                    if(IsSuccess==1)
                    {
                        logger.LogInformation("Deleted");
                        return new JsonResult("Deleted");
                    }
                    if(IsSuccess==0)
                    {
                        logger.LogInformation("Not Deleted, Please check the Client's Id value");
                        return new JsonResult("Not Deleted, Please check the Client's Id value");
                    }
                    if(IsSuccess==-1)
                    {
                        logger.LogInformation("Something went wrong while deleting, Please check the Client's Id value");
                        return new JsonResult("Something went wrong while deleting, Please check the Client's Id value");
                    }
                    else
                    {
                        logger.LogInformation("Something went wrong while deleting, Please check with DB Team....");
                        return new JsonResult("Something went wrong while deleting, Please check with DB Team....");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("An exception has been occoured...");
                return new JsonResult(BadRequest(ex.Message));
            }
        }
        
    }
}
