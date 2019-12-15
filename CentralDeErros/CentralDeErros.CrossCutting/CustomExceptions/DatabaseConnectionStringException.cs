using System;
using System.Collections.Generic;
using System.Text;

namespace CentralDeErros.CrossCutting.CustomExceptions
{
    public class DatabaseConnectionStringException : Exception
    {
        public DatabaseConnectionStringException()
        {
        }

        public DatabaseConnectionStringException(string message): base(message)
        {
        }
    }
}
