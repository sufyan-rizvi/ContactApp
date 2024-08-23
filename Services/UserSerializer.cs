using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContactApp.Models;
using ContactApp.Repository;

namespace ContactApp.Services
{
    internal class UserSerializer
    {
        const string pathUser = @"C:\Users\DELL\Desktop\Sufyan\AproSCM\ContactApp\Assets\user.json";
        

        public static void Serialize(List<User> users)
        {
            string json = JsonSerializer.Serialize(users);
            File.WriteAllText(pathUser, json);
        }

        public static List<User> Deserialize()
        {
            if(File.Exists(pathUser))
            {
                string read = File.ReadAllText(pathUser);
                return JsonSerializer.Deserialize<List<User>>(read)!;
            }
            return new List<User>();
        }

        public static void SerializeContactDetails()
        {
            if (ContactRepository.CurrentContacts == null)
                ContactRepository.CurrentContacts = new List<Contact>();
            if (ContactDetailRepository.CurrentContactDetails == null)
                ContactDetailRepository.CurrentContactDetails = new List<ContactDetail>();

            ContactRepository.UpdateDetailsOfContactList(ContactDetailRepository.CurrentContactDetails, ContactDetailRepository.CurrentContact.ContactId);
            UserRepository.UpdateContactsOfUser(ContactRepository.CurrentContacts, ContactRepository.CurrentUser.UserId);

            Serialize(UserRepository.Users);
        }

        public static void SerializeContacts()
        {
            if (ContactRepository.CurrentContacts == null)
                ContactRepository.CurrentContacts = new List<Contact>();
            
            UserRepository.UpdateContactsOfUser(ContactRepository.CurrentContacts, ContactRepository.CurrentUser.UserId);
            Serialize(UserRepository.Users);
        }
    }
}
