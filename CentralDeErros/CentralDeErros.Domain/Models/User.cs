using CentralDeErros.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralDeErros.Domain.Models
{
    public class User : ModelBase
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool Active { get; set; }

    }
}
