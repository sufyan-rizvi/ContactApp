using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Exceptions
{
    internal class NotAdminException:Exception
    {
        public NotAdminException(string message):base(message)
        {
            
        }
    }
}
