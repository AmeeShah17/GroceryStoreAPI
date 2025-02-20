using GroceryStoreAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GroceryStoreAPI.Repository
{
    public class UserRepository
    {
        #region Configuration
        private readonly string connectionstring;

        public UserRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("GroceryStoreAPI");
        }
        #endregion

        #region Selectall

        public IEnumerable<UserModel> SelectAll()
        {
            var user = new List<UserModel>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_USER_SELECTALL";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                user.Add(new UserModel
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = Convert.ToString(reader["UserName"]),
                    Email = Convert.ToString(reader["Email"]),
                    Password = Convert.ToString(reader["Password"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                });
            }
            return user;
        }
        #endregion

        #region GetbyID

        public UserModel GetbyID(int UserID)
        {
            UserModel user = null;
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_USER_SELECTBYPK";
            command.Parameters.AddWithValue("@UserID", UserID);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                user = new UserModel
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = Convert.ToString(reader["UserName"]),
                    Email = Convert.ToString(reader["Email"]),
                    Password = Convert.ToString(reader["Password"]),
                    Created = Convert.ToDateTime(reader["Created"]),
                    Modified = Convert.ToDateTime(reader["Modified"])
                };
            }
            return user;
        }
        #endregion

        #region Delete
        public bool UserDelete(int UserID)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_USER_DELETE";
            command.Parameters.AddWithValue("@UserID", UserID);
            var rowaffected = command.ExecuteNonQuery();
            return rowaffected > 0;
        }
        #endregion
        public bool UserInsert(UserModel user)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_USER_INSERT";
            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Created", DateTime.Now); 
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
        public bool UserRegister(UserRegisterModel user)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_Register";
            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
            #region Update
            public bool UserUpdate(UserModel user)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_USER_UPDATE";
            command.Parameters.AddWithValue("@UserID", user.UserID);
            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value; 
            int RowAffected = command.ExecuteNonQuery();
            return RowAffected > 0;
        }
        #endregion

        #region Login
        public UserModel Login(UserLoginModel user)
        {
            UserModel userData = null;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_User_Login", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("UserName", user.UserName);
                cmd.Parameters.AddWithValue("Password", user.Password);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userData = new UserModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        Email = reader["Email"].ToString(),
                        Created = Convert.ToDateTime(reader["Created"]),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    };
                }
            }
            return userData;

        }
        #endregion

        public UserModel GetUserProfile(int UserID)
        {
            UserModel user = null;
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_USER_PROFILE";
            command.Parameters.AddWithValue("@UserID", UserID);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                user = new UserModel
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = Convert.ToString(reader["UserName"]),
                    Email = Convert.ToString(reader["Email"]),
                    Password = Convert.ToString(reader["Password"]),
                   
                };
            }
            return user;
        }

    }
}
