using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CentralDeErros.CrossCutting.Utils
{
    public class Utils
    {
        public static bool ValidaEmail(string email)
        {
            bool emailValido = false;
            string emailRegex = string.Format("{0}{1}",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
            try
            {
                emailValido = Regex.IsMatch(
                    email,
                    emailRegex);
            }
            catch (RegexMatchTimeoutException)
            {
                emailValido = false;
            }
            return emailValido;
        }
    }
}
