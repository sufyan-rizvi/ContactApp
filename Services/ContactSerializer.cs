using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContactApp.Models;

namespace ContactApp.Services
{
    internal class ContactSerializer
    {
        const string pathContact = @"C:\Users\DELL\Desktop\Sufyan\AproSCM\ContactApp\Assets\contact.json";

        public static void Serialize(List<Contact> contacts)
        {
            string json = JsonSerializer.Serialize(contacts);
            File.WriteAllText(pathContact, json);
        }

        public static List<Contact> Deserialize()
        {
            if (File.Exists(pathContact))
            {
                string read = File.ReadAllText(pathContact);
                return JsonSerializer.Deserialize<List<Contact>>(read)!;
            }
            return new List<Contact>();
        }
    }
}
