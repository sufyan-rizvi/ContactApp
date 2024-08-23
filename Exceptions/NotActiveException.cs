using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Exceptions
{
    internal class NotActiveException:Exception
    {
        public NotActiveException(string message):base(message)
        {
            
        }
    }
}
