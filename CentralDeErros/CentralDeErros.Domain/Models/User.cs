using CentralDeErros.Domain.Models.Base;

namespace CentralDeErros.Domain.Models
{
    public class User : ModelBase
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; } = true;
    }
}
