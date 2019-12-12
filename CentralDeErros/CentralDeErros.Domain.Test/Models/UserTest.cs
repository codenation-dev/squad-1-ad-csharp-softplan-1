using CentralDeErros.CrossCutting.CustomTypes;
using CentralDeErros.Data.Context;
using CentralDeErros.Domain.Test.Models.Base;
using System;
using Xunit;

namespace CentralDeErros.Domain.Test.Models
{
    public sealed class UserTest : ModelBaseTest
    {
        public UserTest()
            : base(new CentralDeErrosContext())
        {
            Model = "CentralDeErros.Domain.Models.User";
            Table = "user";
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
        [InlineData("Name", true, typeof(string), null)]
        [InlineData("Login", true, typeof(string), null)]
        [InlineData("Email", true, typeof(string), null)]
        [InlineData("Password", true, typeof(string), null)]
        public void Should_Has_Field(string fieldName, bool isNullable, Type fieldType, int? fieldSize)
        {
            AssertField(fieldName, isNullable, fieldType, fieldSize);
        }
    }
}