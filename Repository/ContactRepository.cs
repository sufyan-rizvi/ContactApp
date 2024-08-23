using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactApp.Exceptions;
using ContactApp.Models;
using ContactApp.Services;

namespace ContactApp.Repository
{
    internal class ContactRepository
    {
        public static User CurrentUser { get; set; }
        public static List<Contact> CurrentContacts { get; set; }
        



        public ContactRepository(User user)
        {
            CurrentUser = user;
            CurrentContacts = CurrentUser.Contacts;
            
            
        }

        public static List<Contact> ViewAllContacts()
        {
            AnyContactsInList();
            return CurrentContacts;
        }

        public static void AddContact(Contact contact)
        {
            if (CurrentContacts.Count == 0)
                contact.ContactId = 1;            
            else 
                contact.ContactId = CurrentContacts.Last().ContactId + 1;
            

            contact.UserId = CurrentUser.UserId;
            CurrentContacts.Add(contact);

        }

        public static void RemoveContact(int id)
        {
            Contact contact = GetById(id);
            contact.IsActive = false;
        }

        public static void AnyContactsInList()
        {
            if (CurrentContacts.Count == 0)
            {
                throw new ListEmptyException("No contacts in List ! Add a Contact First !");
            }
        }

        public static Contact GetById(int id)
        {
            Contact contact = CurrentContacts.Where(contact => contact.ContactId == id).FirstOrDefault()!;
            if (contact != null)
                return contact;
            throw new NoElementFoundException("No such Contact exists !");
        }

        public static void CheckActive(Contact contact)
        {
            if (!contact.IsActive)
                throw new NotActiveException("The contact is inactive");
        }

        public static void UpdateContact(Contact contact, int position)
        {
            CurrentContacts[position] = contact;
        }

        public static int GetPositionInList(int id)
        {
            for (int i = 0; i < CurrentContacts.Count; i++)
            {
                if (CurrentContacts[i].ContactId == id)
                    return i;
            }
            throw new NoElementFoundException("No such User Exists !");
        }

        public static void UpdateDetailsOfContactList(List<ContactDetail> details, int contactId)
        {
            for (int i = 0; i < CurrentContacts.Count; i++)
            {
                if (CurrentContacts[i].ContactId == contactId)
                {
                    CurrentContacts[i].ContactDetails = details;
                    return;
                }
            }

        }

    }
}
