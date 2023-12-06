using EmpDA.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EmpDA
{
    public class EmpOperations
    {
        private readonly string SqlCon;
        private SqlConnection con = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        public EmpOperations(string _SqlCon)
        {
            this.SqlCon = _SqlCon;
        }
        public int NewEmp(EmpModel emp)
        {
            int IsSuccess = 0;
            try
            {
                using(con=new SqlConnection(SqlCon))
                {
                    using(cmd=new SqlCommand("NewEmp",con)) 
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName",emp.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                        cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                        cmd.Parameters.AddWithValue("@workingOn", emp.WorkingOn);
                        IsSuccess = cmd.ExecuteNonQuery();
                        con.Close();
                        return IsSuccess;
                    }
                }
            }
            catch(Exception ex) 
            {
                IsSuccess = -2;
                return IsSuccess;
            }
        }



        
        public int DeleteEmp(EmpDTO Emp)
        {
            int IsSuccess = 0;
            try
            {
                using (con = new SqlConnection(SqlCon))
                {
                    using (cmd = new SqlCommand("DeleteEmp", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmpId", Emp.EmpId);
                        cmd.Parameters.AddWithValue("@Empname", Emp.FirstName);
                        cmd.Parameters.AddWithValue("@WorkingOn", Emp.WorkingOn);
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
