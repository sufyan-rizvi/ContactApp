using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Transactions;
using ContactApp.Models;
using ContactApp.Repository;
using ContactApp.Services;

namespace ContactApp.Controller
{
    internal class MainMenuController
    {
        public static void Menu()
        {
            UserRepository.Users = UserSerializer.Deserialize();            
            
            User currentUser;

            while (true)
            {
                try
                {
                    currentUser = UserController.Login();
                    break;

                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n" + e.Message + "\n");
                    Console.ResetColor();
                    
                }

            }
            if (currentUser.IsAdmin) 
                new UserRepository(currentUser);
            else
                new ContactRepository(currentUser);

            while (true)
            {
                if (currentUser.IsAdmin)
                {                    
                    try
                    {
                        UserController.Menu();
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n" + e.Message + "\n");
                        Console.ResetColor();

                    }
                }
                else
                {                    
                    StaffMenuChoice();
                }
            }

        }


        static void DisplayStaffMenu()
        {
            Console.Write("\n" +
                "1. Contact Menu\n" +
                "2. Contact Details Menu\n" +
                "3. Logout\n" +
                "Your Choice: ");

        }

        static void StaffMenuChoice()
        {
            DisplayStaffMenu();
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            switch (choice)
            {
                case 1:
                    ContactController.Menu();
                    break;

                case 2:
                    try
                    {
                        ContactDetailController.Menu();
                    }
                    catch(Exception e)
                    {
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine("\n" + e.Message + "\n");
                        Console.ResetColor();
                    }
                    break;

                case 3:
                    UserController.Logout();
                    break;

                default:
                    Console.WriteLine("Enter a valid option !\n");
                    break;

            }
        }

       
    }
}
