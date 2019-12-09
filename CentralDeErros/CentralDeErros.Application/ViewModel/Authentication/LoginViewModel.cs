using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CentralDeErros.Application.ViewModel.Authentication
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Login { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
