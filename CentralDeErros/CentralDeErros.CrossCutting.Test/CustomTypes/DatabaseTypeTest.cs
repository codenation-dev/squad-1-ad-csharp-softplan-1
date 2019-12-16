using CentralDeErros.CrossCutting.CustomTypes;
using Xunit;

namespace CentralDeErros.CrossCutting.Test.CustomTypes
{
    public class DatabaseTypeTest
    {
        [Theory]
        [InlineData("POSTGRESQL", DatabaseType.PostgreSQL)]
        [InlineData("SQLSERVER", DatabaseType.SQLServer)]
        [InlineData("LOCALDB", DatabaseType.LocalDB)]
        [InlineData("", DatabaseType.Unknow)]
        [InlineData("INCORRECT", DatabaseType.Unknow)]
        public void Should_return_correct_DataType(string str, DatabaseType expected)
        {
            Assert.Equal(expected, str.ToDatabaseType());
        }
    }
}
