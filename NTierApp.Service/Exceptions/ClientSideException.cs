using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApp.Service.Exceptions
{
    public class ClientSideException : Exception
    {
        //Client taraflı bir hataların modeli
        public ClientSideException(string message) : base(message)
        {


        }
         
    }
}
