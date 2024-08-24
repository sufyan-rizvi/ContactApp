using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Models
{
    internal class Contact
    {
        public int ContactId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public bool IsActive { get; set; }
        public User user {get; set; }// nav property
        public int UserId { get; set; } 

        public List<ContactDetail> ContactDetails { get; set; } = new List<ContactDetail>();   

        public override string ToString()
        {
            return $"\nContact Id: {ContactId}\n" +
                $"Name: {FName +" "+ LName}\n" +
                $"Active Status: {IsActive}\n" +
                $"User Id: {UserId}\n";
        }
    }
}
