using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class adminFunctions
    {
        String connString = @"Data Source=SHAHERYAR\SQLEXPRESS; Initial Catalog=ums; Integrated Security=True;Persist Security Info=True;";
        public Admin validateAdmin(String login, String passwd)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String query = String.Format(@"Select * from admin where login=@login and password=@passwd");
                query += "; select Scope_Identity()";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlParameter param = new SqlParameter();
                param.ParameterName = "login";
                param.SqlDbType = System.Data.SqlDbType.VarChar;
                param.Value = login;
                cmd.Parameters.Add(param);
                
                param = new SqlParameter();
                param.ParameterName = "passwd";
                param.SqlDbType = System.Data.SqlDbType.VarChar;
                param.Value = passwd;
                cmd.Parameters.Add(param);

                var read = cmd.ExecuteReader();
                if(read.Read() == true)
                {
                    Admin obj = new Admin();
                    obj.AdminID = read.GetInt32(read.GetOrdinal("AdminID"));
                    obj.AdminName = read.GetString(read.GetOrdinal("AdminName"));
                    obj.login = read.GetString(read.GetOrdinal("Login"));
                    obj.password = read.GetString(read.GetOrdinal("Password"));
                    return obj;
                }
                else
                    return null;
            }
        }
    }
}
