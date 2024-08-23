 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContactApp.Models;

namespace ContactApp.Services
{
    internal class ContactDetailSerializer
    {
        const string pathDetail = @"C:\Users\DELL\Desktop\Sufyan\AproSCM\ContactApp\Assets\contactdetail.json";


        public static void Serialize(List<ContactDetail> details)
        {
            string json = JsonSerializer.Serialize(details);
            File.WriteAllText(pathDetail, json);
        }

        public static List<ContactDetail> Deserialize()
        {
            if (File.Exists(pathDetail))
            {
                string read = File.ReadAllText(pathDetail);
                return JsonSerializer.Deserialize<List<ContactDetail>>(read)!;
            }
            return new List<ContactDetail>();
        }
    }
}
