using CentralDeErros.Domain.Models.Base;

namespace CentralDeErros.Domain.Models
{
    public class ErrorLog : ModelBase
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public bool Archieved { get; set; } = false;
        public string Environment { get; set; } = string.Empty;
    }
}
