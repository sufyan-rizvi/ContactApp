using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Models
{
    internal class ContactDetail
    {
        public int ContactDetailId { get; set; }
        public string Type { get; set; }
        public Contact contact { get; set; }
        public int ContactId {  get; set; }

        public override string ToString()
        {
            return $"\n" +
                $"Contact Detail Id: {ContactDetailId}\n" +
                $"Type: {Type}\n" +
                $"Contact Id: {ContactId}\n";
        }
    }
}
