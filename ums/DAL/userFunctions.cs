using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using Entities;

namespace DAL
{
    public class userfunctions
    {
        String connString = @"Data Source=SHAHERYAR\SQLEXPRESS; Initial Catalog=ums; Integrated Security=True;Persist Security Info=True;";

        public user validateUser(String login, String passwd)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String query = String.Format(@"Select * from users where login=@login and password=@passwd");
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
                if (read.Read() == true)
                {
                    user obj = new user();
                    obj.userid = read.GetInt32(read.GetOrdinal("UserID"));
                    obj.name = read.GetString(read.GetOrdinal("Name"));
                    obj.login = read.GetString(read.GetOrdinal("Login"));
                    obj.passwd = read.GetString(read.GetOrdinal("Password"));
                    obj.email = read.GetString(read.GetOrdinal("Email"));
                    obj.gender = read.GetString(read.GetOrdinal("Gender"))[0];
                    obj.address = read.GetString(read.GetOrdinal("Address"));
                    obj.age = read.GetInt32(read.GetOrdinal("Age"));
                    obj.nic = read.GetString(read.GetOrdinal("NIC"));
                    obj.dob = read.GetDateTime(read.GetOrdinal("DOB"));
                    if (read.GetBoolean(read.GetOrdinal("IsCricket")))
                        obj.cricket = 1;
                    else
                        obj.cricket = 0;
                    if (read.GetBoolean(read.GetOrdinal("Hockey")))
                        obj.hockey = 1;
                    else
                        obj.hockey = 0;
                    if (read.GetBoolean(read.GetOrdinal("Chess")))
                        obj.chess = 1;
                    else
                        obj.chess = 0;
                    obj.imageName = read.GetString(read.GetOrdinal("ImageName"));
                    obj.createdon = read.GetDateTime(read.GetOrdinal("CreatedOn"));
                    return obj;
                }
                else
                    return null;
            }
        }

        public user GetUserById(int userid)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String query = String.Format(@"Select * from users where UserID=@userid");
                query += "; select Scope_Identity()";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlParameter param = new SqlParameter();
                param.ParameterName = "userid";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Value = userid;
                cmd.Parameters.Add(param);

                var read = cmd.ExecuteReader();
                if (read.Read() == true)
                {
                    user obj = new user();
                    obj.userid = read.GetInt32(read.GetOrdinal("UserID"));
                    obj.name = read.GetString(read.GetOrdinal("Name"));
                    obj.login = read.GetString(read.GetOrdinal("Login"));
                    obj.passwd = read.GetString(read.GetOrdinal("Password"));
                    obj.email = read.GetString(read.GetOrdinal("Email"));
                    obj.gender = read.GetString(read.GetOrdinal("Gender"))[0];
                    obj.address = read.GetString(read.GetOrdinal("Address"));
                    obj.age = read.GetInt32(read.GetOrdinal("Age"));
                    obj.nic = read.GetString(read.GetOrdinal("NIC"));
                    obj.dob = read.GetDateTime(read.GetOrdinal("DOB"));
                    if (read.GetBoolean(read.GetOrdinal("IsCricket")))
                        obj.cricket = 1;
                    else
                        obj.cricket = 0;
                    if (read.GetBoolean(read.GetOrdinal("Hockey")))
                        obj.hockey = 1;
                    else
                        obj.hockey = 0;
                    if (read.GetBoolean(read.GetOrdinal("Chess")))
                        obj.chess = 1;
                    else
                        obj.chess = 0;
                    obj.imageName = read.GetString(read.GetOrdinal("ImageName"));
                    obj.createdon = read.GetDateTime(read.GetOrdinal("CreatedOn"));
                    return obj;
                }
                else
                    return null;
            }
        }

        public int addUser(user usr)
        {
            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String query = String.Format(@"INSERT INTO Users(Name,Login,Password,Gender,Address,Age,NIC,DOB,IsCricket,Hockey,Chess,imagename,CreatedOn,Email) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}');", usr.name, usr.login, usr.passwd, usr.gender, usr.address, usr.age, usr.nic, usr.dob, usr.cricket, usr.hockey, usr.chess, usr.imageName, usr.createdon,usr.email);
                
                SqlCommand cmd = new SqlCommand(query,conn);

                var read = cmd.ExecuteNonQuery();
                int record = Convert.ToInt32(read);
                if (record == 1)
                    return record;
                else
                    return record;
            }
        }

        public bool validateUserName(String login)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String query = String.Format(@"Select * from users where login=@login");
                query += "; select Scope_Identity()";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlParameter param = new SqlParameter();
                param.ParameterName = "login";
                param.SqlDbType = System.Data.SqlDbType.VarChar;
                param.Value = login;
                cmd.Parameters.Add(param);

                var read = cmd.ExecuteScalar();
                int record = Convert.ToInt32(read);
                if (record > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool validateEmail(String email)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String query = String.Format(@"Select * from users where Email=@email");
                query += "; select Scope_Identity()";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlParameter param = new SqlParameter();
                param.ParameterName = "email";
                param.SqlDbType = System.Data.SqlDbType.VarChar;
                param.Value = email;
                cmd.Parameters.Add(param);

                var read = cmd.ExecuteScalar();
                int record = Convert.ToInt32(read);
                if (record > 0)
                    return true;
                else
                    return false;
            }
        }
        
        public bool updateUser(user usr)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // Query
                String query = String.Format(@"UPDATE USERS SET Name='{0}', Login='{1}', Password='{2}', Gender='{3}', Address='{4}', Age='{5}', NIC='{6}', DOB='{7}', IsCricket='{8}', Hockey='{9}', Chess='{10}', ImageName='{11}', Email='{12}' WHERE UserID='{13}'",usr.name,usr.login,usr.passwd,usr.gender,usr.address,usr.age,usr.nic,usr.dob,usr.cricket,usr.hockey,usr.chess,usr.imageName,usr.email,usr.userid);
                SqlCommand cmd = new SqlCommand(query, conn);
                var update = cmd.ExecuteNonQuery();
                if ((int)update > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool updateUserPasswordByEmail(String passwd, String email)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // Query
                String query = String.Format(@"UPDATE USERS SET Password='{0}' WHERE Email='{1}' ", passwd,email);
                SqlCommand cmd = new SqlCommand(query, conn);
                var update = cmd.ExecuteNonQuery();
                if ((int)update > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool deleteUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // Query
                String query = String.Format(@"DELETE FROM USERS WHERE userid='{0}'", id);
                SqlCommand cmd = new SqlCommand(query, conn);
                var del = cmd.ExecuteNonQuery();
                if ((int)del > 0)
                    return true;
                else
                    return false;
            }
        }

        public List<user> GetAllUsers()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String query = String.Format(@"Select * from users;");
                SqlCommand cmd = new SqlCommand(query, conn);
                List<user> list = new List<user>();
                var read = cmd.ExecuteReader();
                while (read.Read() == true)
                {
                    user obj = new user();
                    obj.userid = read.GetInt32(read.GetOrdinal("UserID"));
                    obj.name = read.GetString(read.GetOrdinal("Name"));
                    obj.login = read.GetString(read.GetOrdinal("Login"));
                    obj.passwd = read.GetString(read.GetOrdinal("Password"));
                    obj.email = read.GetString(read.GetOrdinal("Email"));
                    obj.gender = read.GetString(read.GetOrdinal("Gender"))[0];
                    obj.address = read.GetString(read.GetOrdinal("Address"));
                    obj.age = read.GetInt32(read.GetOrdinal("Age"));
                    obj.nic = read.GetString(read.GetOrdinal("NIC"));
                    obj.dob = read.GetDateTime(read.GetOrdinal("DOB"));
                    if (read.GetBoolean(read.GetOrdinal("IsCricket")))
                        obj.cricket = 1;
                    else
                        obj.cricket = 0;
                    if (read.GetBoolean(read.GetOrdinal("Hockey")))
                        obj.hockey = 1;
                    else
                        obj.hockey = 0;
                    if (read.GetBoolean(read.GetOrdinal("Chess")))
                        obj.chess = 1;
                    else
                        obj.chess = 0;
                    obj.imageName = read.GetString(read.GetOrdinal("ImageName"));
                    obj.createdon = read.GetDateTime(read.GetOrdinal("CreatedOn"));
                    list.Add(obj);
                }
                return list;
            }
        }
    }
}
