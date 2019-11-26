using CentralDeErros.Domain.Models.Base;

namespace CentralDeErros.Domain.Models
{
    public class ErrorLog : ModelBase
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public bool Shelved { get; set; }
        public string Environment { get; set; }
    }
}
