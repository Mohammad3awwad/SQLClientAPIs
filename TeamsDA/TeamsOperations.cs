using System.Data;
using System.Data.SqlClient;
using TeamsDA.Model;

namespace TeamsDA
{
    public  class TeamsOperations
    {
        private readonly string SQLCon;
        private SqlConnection con = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        public TeamsOperations(string _SQLCon)
        {
            this.SQLCon = _SQLCon;
        }

        public IEnumerable<TeamsDTO> AllTeams()
        {
            List<TeamsDTO> teams = new List<TeamsDTO>();    
            try
            {
                using(con=new SqlConnection(SQLCon)) 
                {
                     using(cmd=new SqlCommand("AllTeams",con)) 
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader DR=cmd.ExecuteReader();
                        if(DR.HasRows)
                        {
                           while(DR.Read())
                            {
                                teams.Add(new TeamsDTO(Convert.ToInt32(DR["TeamId"]), DR["TeamName"].ToString(),
                                         Convert.ToInt32(DR["NumberOfTeamMembers"]), Convert.ToInt32(DR["NumberOfProjects"])));
                            
                            }
                           return teams;
                        }
                        else
                        {
                            teams.Clear();
                            return teams;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                teams.Add(new TeamsDTO(0,null,0,0));
                return teams;
            }
        }
        public int NewTeam(TeamsModel teams)
        {
            int IsSuccess = 0;
            try
            {
                using (con = new SqlConnection(SQLCon))
                {
                    using (cmd = new SqlCommand("AddTeam",con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TeamName",teams.TeamName.ToString());
                        cmd.Parameters.AddWithValue("@NumberOfTeamMembers",Convert.ToInt32(teams.NumberOfTeamMembers));
                        cmd.Parameters.AddWithValue("@NumberOfProjects",Convert.ToInt32(teams.NumberOfProjects));
                        IsSuccess = (int)cmd.ExecuteNonQuery();
                        return IsSuccess;
                    }
                }
            }
            catch (Exception ex)
            {
                return IsSuccess;
            }
        }

        public int ChangeName(TeamsModel teams) 
        {
            int IsSuccess = 0;
            try
            {
                using (con=new SqlConnection(SQLCon))
                {
                    using(cmd=new SqlCommand("ChangeTeamName",con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OldTeamName", teams.TeamName.Trim().ToString());
                        cmd.Parameters.AddWithValue("@NewTeamName", teams.ChangeTeamName.Trim().ToString());
                        IsSuccess=(int)cmd.ExecuteNonQuery();
                        return IsSuccess;
                    }
                }
            }
            catch(Exception ex) 
            {
                return IsSuccess;
            }
        }

        public int DeleteATeam(TeamsModel teams)
        {
            int IsSuccess = 0 ;
            try
            {
                using(con=new SqlConnection(SQLCon))
                {
                    using(cmd=new SqlCommand("DeleteATeam",con))
                    {
                        con.Open();
                        cmd.CommandType=CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TeamtName", teams.TeamName.Trim().ToString());
                        IsSuccess=cmd.ExecuteNonQuery();
                        return IsSuccess;
                    }
                }
            }
            catch(Exception ex)
            {
                IsSuccess = -1;
                return IsSuccess;
            }
        }

    }
}
