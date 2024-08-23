using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Exceptions
{
    internal class NotEnoughUsersException:Exception
    {
        public NotEnoughUsersException(string message):base(message)
        {
            
        }
    }
}
