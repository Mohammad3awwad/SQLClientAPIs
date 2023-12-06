using ClientsDA.Model;
using System.Data;
using System.Data.SqlClient;
namespace ClientsDA
{
    public class ClientsOperations
    {
        private  SqlConnection con = new SqlConnection();
        private  SqlCommand cmd = new SqlCommand();
        private readonly string ConString;
        public ClientsOperations(string _ConString)
        {
            this.ConString = _ConString;    
        }
        public int InsertAClient(ClientsModel CO)
        {
            int IsSuccess = 0;
            try
            {
                using(con=new SqlConnection(ConString)) 
                {
                    using(cmd=new SqlCommand("NewClient",con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClientName",CO.ClientName.ToString());
                        cmd.Parameters.AddWithValue("@ProjectName", CO.ProjectName.ToString());
                        IsSuccess = cmd.ExecuteNonQuery();
                        con.Close();
                        return IsSuccess;
                    }
                }
            }
            catch(Exception ex)
            { 
                return IsSuccess;
            }
        }
        public int UpdateClient(ClientsModel CM)
        {
            try
            {
                
                using(con=new SqlConnection(ConString)) 
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateClient",con)) 
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClientId",Convert.ToInt32(CM.ClientId));
                        cmd.Parameters.AddWithValue("@ClientName",CM.ClientName.ToString());
                        cmd.Parameters.AddWithValue("@ProjectName", CM.ProjectName.ToString());
                        int IsSuccess=cmd.ExecuteNonQuery();
                        con.Close();
                        return IsSuccess;
                    }
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        public List<ClientsModel>GetAClient(int ClientId)
        {
            List<ClientsModel> client = new List<ClientsModel>();
            try
            {
                using(con=new SqlConnection(ConString)) 
                {
                    using(cmd=new SqlCommand("GetAClient",con)) 
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClientId", ClientId);
                        SqlDataReader DR = cmd.ExecuteReader();
                        if (DR.HasRows)
                        {
                            DR.Read();
                            client.Add(new ClientsModel(Convert.ToInt32(DR["ClientId"]), DR["ClientName"].ToString(), DR["ProjectName"].ToString()));
                            return client;
                        }
                        else
                        {
                            client.Clear();
                            client.Add(new ClientsModel(0,"Not Found","Not Found"));
                            return client;
                        }    
                        
                    }
                }
            }
            catch(Exception ex)
            {
                client.Clear();
                client.Add(new ClientsModel(0,"Something went wrong", "Something went wrong"));
                return client;
            }
        }
        public IEnumerable<ClientsModel> GetAllClients()
        {
            List<ClientsModel> clients = new List<ClientsModel>();
            try
            {
                using (con = new SqlConnection(ConString))
                {
                    using (cmd = new SqlCommand("GetAllClients", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader DR = cmd.ExecuteReader();
                        while (DR.Read()) 
                        {
                            clients.Add(new ClientsModel(Convert.ToInt32(DR["ClientId"]), DR["ClientName"].ToString(), DR["ProjectName"].ToString()));
                        }
                        con.Close();
                    }
                }
                return clients;
            }
            catch(Exception ex) 
            {
                clients.Clear();
                return clients;
            }
        }
        public int DeletAClient(int ClientId)
        {
            int IsSuccess = 0;
            try
            {
                using(con=new SqlConnection(ConString))
                {
                    using(cmd=new SqlCommand("DeleteAClient",con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClientId",ClientId);
                        IsSuccess= cmd.ExecuteNonQuery();
                        return IsSuccess;
                    }
                }
            }
            catch(Exception ex)
            {
                IsSuccess= -1;
                return IsSuccess;
            }
        }
        
    }
}
