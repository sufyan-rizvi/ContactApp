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
    internal class ContactDetailController
    {
        public static void Menu()
        {
            SelectContact();
            bool continueMenu = true;
            while (continueMenu)
            {
                try
                {
                    DisplayContactDetailMenu();
                    int choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    switch (choice)
                    {
                        case 1:
                            AddDetail();
                            break;
                        case 2:
                            DeleteDetail();
                            break;
                        case 3: 
                            UpdateDetail();
                            break;
                        case 4:
                            
                            ViewAllDetails();
                            break;
                        case 5:
                            UserSerializer.SerializeContactDetails();
                            continueMenu = false;
                            break;

                        default:
                            Console.WriteLine("Enter a valid Option !\n");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n" + e.Message + "\n");
                    Console.ResetColor();

                }
            }
        }

        
        public static void DisplayContactDetailMenu()
        {
            Console.Write("\n" +
                "1. Add Contact Details\n" +
                "2. Delete Contact Details\n" +
                "3. Update Contact Details\n" +
                "4. View All Contact Details\n" +
                "5. Return to Staff Menu\n" +
                "Your Choice: ");
        }
        public static void SelectContact()
        {
            ContactController.ViewAllContacts();

            Console.Write("\nEnter Contact Id that you wish to access: ");            
            int contactId = Convert.ToInt32(Console.ReadLine());
            Contact currentContact = ContactRepository.GetById(contactId);
            ContactRepository.CheckActive(currentContact);
            new ContactDetailRepository(currentContact);

        }
        public static void AddDetail()
        {
            Console.Write("Enter Email or Number for contact: ");
            string type = Console.ReadLine();            

            ContactDetailRepository.AddContactDetail(new ContactDetail
            {
                Type = type
            });
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDetail Added Successfully!");
            Console.ResetColor();
        }

        public static void DeleteDetail()
        {
            ViewAllDetails();

            Console.Write("Enter the Id you want to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());
            ContactDetailRepository.DeleteDetail(id);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nDetail deleted Successfully!");
            Console.ResetColor();
        }

        public static void ViewAllDetails()
        {
            ContactDetailRepository.ViewAllContactDetails().ForEach(detail => {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(detail);
                Console.ResetColor();               
                });
        }

        public static void UpdateDetail()
        {
            ViewAllDetails();

            Console.Write("Enter the id you want to update: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var detail = ContactDetailRepository.GetById(id);

            Console.Write("Enter new Email or Phone Number for contact Detail: ");
            string type = Console.ReadLine();            

            ContactDetailRepository.UpdateDetail(new ContactDetail
            {
                Type = type,
                ContactDetailId = detail.ContactDetailId,
                ContactId = detail.ContactId,

            });

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDetail Updated Successfully!");
            Console.ResetColor();
        }


    }
}
