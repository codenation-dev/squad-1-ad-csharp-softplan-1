using CentralDeErros.CrossCutting.CustomExceptions;
using System;
using Xunit;

namespace CentralDeErros.CrossCutting.Test.CustomExceptions
{
    public class DatabaseConnectionStringExceptionTest
    {
        private void ThrowDatabaseConnectionStringException(string message = null)
        {
            if (message == null)
            {
                throw new DatabaseConnectionStringException();
            }
            throw new DatabaseConnectionStringException(message);
        }

        [Fact]
        public void Should_correct_create_db_connection_string_exception()
        {
            string message = "Error teste";

            Assert.Throws<DatabaseConnectionStringException>(() => ThrowDatabaseConnectionStringException());
            Assert.Throws<DatabaseConnectionStringException>(() => ThrowDatabaseConnectionStringException(message));
        }
    }
}
