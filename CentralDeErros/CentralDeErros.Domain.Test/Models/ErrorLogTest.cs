using CentralDeErros.CrossCutting.CustomTypes;
using CentralDeErros.Data.Context;
using CentralDeErros.Domain.Test.Models.Base;
using System;
using Xunit;

namespace CentralDeErros.Domain.Test.Models
{
    public sealed class UserModelTest : ModelBaseTest
    {
        public UserModelTest()
            : base(new CentralDeErrosContext())
        {
            Model = "CentralDeErros.Domain.Models.ErrorLog";
            Table = "error_log";
        }

        [Fact]
        public void Should_Has_Table()
        {
            AssertTable();
        }

        [Fact]
        public void Should_Has_Primary_Key()
        {
            AssertPrimaryKeys("Id");
        }

        [Theory]
        [InlineData("Id", false, typeof(Guid), null)]
        [InlineData("CreatedAt", false, typeof(DateTime), null)]
        [InlineData("UpdatedAt", false, typeof(DateTime), null)]
        [InlineData("Code", true, typeof(string), null)]
        [InlineData("Message", true, typeof(string), null)]
        [InlineData("Level", true, typeof(string), null)]
        [InlineData("Archieved", false, typeof(bool), null)]
        [InlineData("Environment", false, typeof(ServerEnvironment), null)]
        public void Should_Has_Field(string fieldName, bool isNullable, Type fieldType, int? fieldSize)
        {
            AssertField(fieldName, isNullable, fieldType, fieldSize);
        }
    }
}