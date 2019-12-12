using Xunit;
using CentralDeErros.CrossCutting.Utils;

namespace CentralDeErros.CrossCutting.Test.Utils
{
    public class UtilsTest
    {
        [Fact]
        public void Should_Correct_Convert_String_To_MD5()
        {
            string source = "Test123@123#4";
            string sourceMD5 = "712470f9c5ccde75a7f35abe6fcccbf0";
            string resultMD5 = CentralDeErros.CrossCutting.Utils.Utils.ToHashMD5(source);

            Assert.Equal(sourceMD5, resultMD5);
        }
    }
}
