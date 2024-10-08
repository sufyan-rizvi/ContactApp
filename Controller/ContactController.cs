﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactApp.Models;
using ContactApp.Repository;
using ContactApp.Services;

namespace ContactApp.Controller
{
    internal class ContactController
    {
        public static void Menu()
        {

            bool continueMenu = true;
            while (continueMenu)
            {
                try
                {
                    DisplayContactMenu();
                    int choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    switch (choice)
                    {
                        case 1:
                            AddContact();
                            break;
                        case 2:
                            RemoveContact();
                            break;
                        case 3:
                            UpdateContact();
                            break;
                        case 4:
                            ViewAllContacts();
                            break;
                        case 5:
                            UserSerializer.SerializeContacts();
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

        public static void DisplayContactMenu()
        {
            Console.Write("\n" +
                "1. Add Contact\n" +
                "2. Delete Contact\n" +
                "3. Update Contact\n" +
                "4. View All Contact\n" +
                "5. Return to Staff Menu\n" +
                "You Choice: ");
        }
        public static void AddContact()
        {
            Console.Write("Enter First Name: ");
            string fName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lName = Console.ReadLine();
           
            ContactRepository.AddContact(new Contact
            {
                FName = fName,
                LName = lName,
                IsActive = true
            });

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nContact Added Successfully!");
            Console.ResetColor();
        }

        public static void UpdateContact()
        {
            ViewAllContacts();
            Console.Write("Enter the id you want to update: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var contact = ContactRepository.GetById(id);
            ContactRepository.CheckActive(contact);

            Console.Write("Enter New First Name for contact: ");
            string fname = Console.ReadLine();

            Console.Write("Enter New Last Name for contact: ");
            string lname = Console.ReadLine();

            var position = ContactRepository.GetPositionInList(id);

            ContactRepository.UpdateContact(new Contact
            {
                FName = fname,
                LName = lname,
                UserId = contact.UserId,
                IsActive = contact.IsActive,
                ContactId = contact.ContactId,
                ContactDetails = contact.ContactDetails
                
            }, position);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nContact Updated Successfully!");
            Console.ResetColor();

        }

        public static void RemoveContact()
        {
            ViewAllContacts();
            Console.Write("Enter Contact Id to remove: ");
            int id = Convert.ToInt32(Console.ReadLine());
            ContactRepository.RemoveContact(id);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nContact Deleted Successfully!");
            Console.ResetColor();
        }

        public static void ViewAllContacts()
        {
            ContactRepository.ViewAllContacts().ForEach(contact =>
            {
                if (contact.IsActive)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(contact);
                    Console.ResetColor();
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(contact);
                    Console.ResetColor();
                }
            });
        }
    }
}
