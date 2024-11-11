using System;
using System.Security.Cryptography.X509Certificates;
using Core.Data;
using Microsoft.Data.SqlClient;
using static Azure.Core.HttpHeader;
namespace BlogConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            string connetionString = "Server=PROUDD\\SQLEXPRESS;Database=BlogsDB;Trusted_Connection=True;TrustServerCertificate=True;";
            using(SqlConnection connection = new SqlConnection(connetionString))
            {
                connection.Open();
                Console.WriteLine("BlogsDb Databese ile connection ugurlu oldu))");

                bool TrueValue = true;
                bool TrueValue1 = true;
                while(TrueValue)
                {
                    Console.WriteLine("\n======================= Blog App Menu ========================");
                    Console.WriteLine("1: USERS");
                    Console.WriteLine("2: Blogs And Users");
                    Console.WriteLine("3: Blogs and Catagoryes");
                    Console.WriteLine("4: User Comments");
                    Console.WriteLine("5: Blog Users");
                    Console.WriteLine("6: Blogs Count");
                    Console.WriteLine("7: Blogs Table");
                    Console.WriteLine("0: Exiting");
                    Console.Write("Bir secim edin: ");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("-------------------------- USERS  ------------------------------------");
                            AppDbContext.GetUsersData(connection);
                            break;

                        case "2":
                            Console.Clear();
                            Console.WriteLine("-------------------------- Blogs And Users ------------------------------------");
                            AppDbContext.GetBlogsUsersData(connection);
                            break;
                        
                        case "3":
                            Console.Clear();
                            Console.WriteLine("-------------------------- Blogs and Catagoryes ------------------------------------");
                            AppDbContext.GetBlogsCatagoryesData(connection);
                            break;

                        case "4":
                            Console.Clear();
                            Console.WriteLine("-------------------------- User Comments ------------------------------------");
                            Console.WriteLine("Commentini gormek istediyiniz userin ID - sini daxil edin.");
                            TrueValue = int.TryParse(Console.ReadLine(), out int UserId);
                            {
                                if (!TrueValue) { Console.WriteLine("Int tipinde Id daxil edin."); }
                            }
                            AppDbContext.GetUserCommentData(connection, UserId);
                            break;

                        case "5":
                            Console.Clear();
                            Console.WriteLine("-------------------------- Blog Users ------------------------------------");
                            Console.WriteLine("Bloglarini gormek istediyiniz userin ID - sini daxil edin.");
                            TrueValue1 = int.TryParse(Console.ReadLine(), out  int UserID);
                            {
                                if (!TrueValue1) { Console.WriteLine("Int tipinde Id daxil edin."); }
                            }
                            AppDbContext.GetBlogersData(connection, UserID);
                            break;

                        case "6":
                            Console.Clear();
                            Console.WriteLine("-------------------------- Blogs Count ------------------------------------");
                            Console.WriteLine("Blog sayini gormek  istediyiniz Categorynin ID - sini daxil edin.");
                            TrueValue1 = int.TryParse(Console.ReadLine(), out int CategoryID);
                            {
                                if (!TrueValue1) { Console.WriteLine("Int tipinde Id daxil edin."); }
                            }
                            Console.WriteLine($"{CategoryID} ID-li categoriyada {AppDbContext.GetBlogCountByCategoryId(connection, CategoryID)} sayda blog var.");
                            break;

                        case "7":
                            Console.Clear();
                            Console.WriteLine("-------------------------- Blogs Table ------------------------------------");
                            Console.WriteLine("Blog sayini gormek  istediyiniz Categorynin ID - sini daxil edin.");
                            TrueValue1 = int.TryParse(Console.ReadLine(), out int userId);
                            {
                                if (!TrueValue1) { Console.WriteLine("Int tipinde Id daxil edin."); }
                            }
                            AppDbContext.GetBlogsTable(connection, userId);
                            break;

                        case "0":
                            Console.Clear();
                            TrueValue = false;
                            Console.WriteLine("Proqramdan Cixildi)");
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("Duzgun secim edin zahmet deyilse...");
                            break;
                    }

                }
              
            }

        }
    }
}
