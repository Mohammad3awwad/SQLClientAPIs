using ProjectsDA.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsDA
{
    public class ProjectOperations
    {
        private readonly string ConString;
        private SqlConnection con = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();

        public ProjectOperations(string _ConString)
        {
            this.ConString = _ConString;    
        }

        public int newProject(ProjectsModel project)
        {
            int IsSuccess = 0;
            try
            {
                using(con=new SqlConnection(ConString)) 
                {
                    using(cmd=new SqlCommand("NewProject",con)) 
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectName",project.ProjectName.ToString());
                        cmd.Parameters.AddWithValue("@AssignedTo", project.AssignedTo.ToString()) ;
                        cmd.Parameters.AddWithValue("@StartedDate",project.StartedDate.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@ProjectStatus",project.ProjectStatus.ToString());
                        IsSuccess= cmd.ExecuteNonQuery();
                        return IsSuccess;

                    }
                }
            }
            catch (Exception ex)
            {
                IsSuccess = -2;
                return IsSuccess;

            }
        }
    }
}
