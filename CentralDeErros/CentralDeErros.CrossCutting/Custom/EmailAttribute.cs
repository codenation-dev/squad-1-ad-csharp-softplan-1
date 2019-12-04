using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CentralDeErros.CrossCutting.Custom
{
    public class EmailAttribute
    {
        public EmailAttribute(string errorMessage = "O E-mail é inválido") : base(errorMessage)
        {
        }
        public override bool IsValid(object value)
        {
            return Utils.Utils.ValidaEmail(value.ToString());
        }
    }
}
