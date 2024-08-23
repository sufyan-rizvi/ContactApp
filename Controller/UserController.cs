using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactApp.Models;
using ContactApp.Repository;
using ContactApp.Services;

namespace ContactApp.Controller
{
    internal class UserController
    {

        public static void Menu()
        {
            bool continueMenu = true;
            while (continueMenu)
            {
                DisplayMenu();
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        AddUser();
                        break;
                    case 2:
                        DeleteUser();
                        break;
                    case 3:
                        MakeAdmin();
                        break;
                    case 4:
                        UpdateUser();
                        break;

                    case 5:
                        ViewAllUsers();
                        break;
                    case 6:
                        continueMenu = false;
                        UserSerializer.Serialize(UserRepository.Users);
                        Logout();
                        break;
                    default:
                        Console.WriteLine("Enter a Valid Option \n");
                        break;
                }
            }
        }
        public static void DisplayMenu()
        {
            Console.Write("\n" +
                "1. Add User \n" +
                "2. Delete User\n" +
                "3. Make a User Admin\n" +
                "4. Update User\n" +
                "5. View All Users\n" +
                "6. Logout and Exit !\n" +
                "Your Choice: ");
        }

        public static User Login()
        {
            Console.WriteLine("=========== Login Page ===========\n");
            Console.Write("Enter your Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            User user = UserRepository.GetById(id);
            UserRepository.CheckActive(user);
            Console.WriteLine($"Welcome {user.FName + " " + user.LName}\n");
            Console.WriteLine("Successfully Logged In !\n");
            return user;
        }

        public static void Logout()
        {
            Console.WriteLine("Logged Out !");
            Environment.Exit(0);

        }

        public static void AddUser()
        {
            Console.Write("\nEnter your First Name: ");
            string fname = Console.ReadLine()!;

            Console.Write("Enter your Last Name: ");
            string sname = Console.ReadLine()!;

            
            bool isAdmin = false;
            bool notValid = true;
            while (notValid)
            {
                Console.Write("Is this user an Admin? Y/N: ");
                switch (Console.ReadLine()!.ToLower())
                {
                    case "y":
                        isAdmin = true;
                        notValid = false;
                        break;

                    case "n":
                        isAdmin = false;
                        notValid = false;
                        break;
                    default:
                        Console.WriteLine("Enter a valid Option");
                        break;
                }
            }
            bool isActive = true;

            UserRepository.AddUser(new User
            {
                FName = fname,
                LName = sname,
                IsAdmin = isAdmin,
                IsActive = isActive

            });
        }

        public static void DeleteUser()
        {
            Console.Write("Enter the UserId you want to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());
            UserRepository.SetInactive(id);
        }

        public static void UpdateUser()
        {
            Console.Write("Enter the id you want to update !");
            int id = Convert.ToInt32(Console.ReadLine());
            var user = UserRepository.GetById(id);

            Console.Write("Enter New First Name for user: ");
            string fname = Console.ReadLine();

            Console.Write("Enter New Last Name for user: ");
            string lname = Console.ReadLine();

            var position = UserRepository.GetPositionInList(id);

            UserRepository.UpdateUser(new User
            {
                FName = fname,
                LName = lname,
                UserId = user.UserId,
                IsActive = user.IsActive,
                IsAdmin = user.IsAdmin,
            }, position);

        }

        public static void MakeAdmin()
        {
            Console.Write("Enter the UserId you want to make Admin: ");
            int id = Convert.ToInt32(Console.ReadLine());
            UserRepository.SetAdmin(id);
        }

        public static void ViewAllUsers()
        {
            UserRepository.ViewAllUsers().ForEach(user => {

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(user);
                Console.ResetColor();
            });
        }
    }
}
