using CentralDeErros.CrossCutting.Helpers;
using Xunit;

namespace CentralDeErros.CrossCutting.Test.Helpers
{
    public class AppSettingsTest
    {
        [Fact]
        public void Should_Possible_Set_SecretKeyJWT_Property()
        {

            AppSettings appSettings = new AppSettings();
            string newSecretKey = "Test_String_Key";
            appSettings.SecretKeyJWT = newSecretKey;

            Assert.Equal(newSecretKey, appSettings.SecretKeyJWT);
        }
    }
}
