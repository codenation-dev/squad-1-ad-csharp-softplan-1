
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace CentralDeErros.Application.ViewModel
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Login { get; set; }
        [RegularExpression(@"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$", ErrorMessage = "Email inválido.")]
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        [Required]
        [IgnoreDataMember]
        public string Password { get; set; }
        public bool Active { get; set; }
        public string AccessToken { get; set; }

    }
}
