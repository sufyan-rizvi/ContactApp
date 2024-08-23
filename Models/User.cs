using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Models
{
    internal class User
    {
        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public List<Contact> Contacts { get; set; } = new List<Contact>();// nav property

        public override string ToString()
        {
            return $"\n" +
                $"User Id: {UserId}\n" +
                $"Name: {FName} {LName}\n" +
                $"Admin Status: {IsAdmin}\n" +
                $"Active Status: {IsActive}\n";
        }
    }
}
