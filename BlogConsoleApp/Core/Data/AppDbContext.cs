using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace Core.Data
{
    public class AppDbContext
    {

        //Movcud USerlerin butun datalarini gostermekk
        public static void GetUsersData(SqlConnection connection)
        {
            string Usersquery = "Select* from Users";
            using (SqlCommand command1 = new SqlCommand(Usersquery, connection))
            using (SqlDataReader reader1 = command1.ExecuteReader())
            {
                while (reader1.Read())
                {
                    Console.WriteLine($"User ID: {reader1["ID"]}, User Name : {reader1["UserName"]}, FullName: {reader1["FullName"]}, Age: {reader1["Age"]} ");
                }
            }
        }
        //1. Blogs'un Title, Users'in userName ve fullName columnlarini qaytaran view yazirsiniz.
        public static void GetBlogsUsersData(SqlConnection connection)
        {
            string query = "select * from InfoViewBloger";
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Blog Titles: {reader["Blog Titles"]},  Bloger User Name: {reader["Bloger User Name"]},  Bloger Full Name: {reader["Bloger User Name"]}  ");
                }
            }
        }
        //2. Blogs'un Title, Categories'in Name columnlarini qaytaran view.
        public static void GetBlogsCatagoryesData(SqlConnection connection)
        {
            string query = "select * from BlogsAndCatagoryNames";
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Blog Titles: {reader["Blog Titles"]}, Catagory Names{reader["Catagory Names"]}");
                }
            }
        }

        //3. @userId parametri qebul edib hemin parametre uygun userin yazdigi commentleri qaytaran procedure yazirsiniz.

        public static void GetUserCommentData(SqlConnection connection, int UserId)
        {
            using (SqlCommand command = new SqlCommand("usp_get_comment_content", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userId", UserId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($" User Id : {reader["User Id"]}, User Full Name : {reader["User Full Name"]},  Comment : {reader["Comment content"]}");
                    }
                }


            }
        }

        //4. @userId parametri qebul edib hemin parametre uygun userin bloglarini qaytaran procedure yazirsiniz.

        public static void GetBlogersData(SqlConnection connection, int UserID)
        {
            using (SqlCommand command = new SqlCommand("usp_get_user_blogs", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userid", UserID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($" User Id : {reader["User Id"]}, User Full Name : {reader["User Full Name"]},  Blogs title : {reader["Blogs title"]}");

                    }
                }

            }
        }
        //5. Parametr olaraq, @categoryId qebul edib, hemin parametre Bloglarin sayini geri qaytaran function yazirsiniz.

        public static int GetBlogCountByCategoryId(SqlConnection connection, int CategoryID)
        {
            int BlogsCount = 0;
            string query = "select dbo.getCountBlogs(@categoryId) as 'Blogs Count'";
            ;

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@categoryId", CategoryID);
                BlogsCount = (int)command.ExecuteScalar();

            }

            return BlogsCount;

        }
        //6. Parametr olaraq, @userId qebul edib, hemin user'in yaratdigi bloglari table kimi geri qaytaran function yazirsiniz.

        public static void GetBlogsTable(SqlConnection connection, int userId)
        {
            string query = "select * from GetBlogTable(@userid)";
            ;

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userid", userId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"BlogId: {reader["BlogId"]}, BlogTitle: {reader["BlogTitle"]}, BlogDescription: {reader["BlogDescription"]}");
                    }
                }
            }


        }
    }
}
