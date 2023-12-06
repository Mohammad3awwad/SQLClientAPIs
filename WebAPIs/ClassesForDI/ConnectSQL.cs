using Microsoft.AspNetCore.Server.IIS.Core;
using WebAPIs.InterFacesForDI;

namespace WebAPIs.ClassesForDI
{
    // This class Implement IConnectSQL Interface to return Connection String Using DI approach
    public class ConnectSQL: IConnectSQL
    {
        private readonly IConfiguration Con;
        public ConnectSQL(IConfiguration _Con)
        {
            this.Con = _Con;
        }
        public string Connect()
        {
            return Con.GetConnectionString("DB1Connection").ToString();

        }
    }
}
