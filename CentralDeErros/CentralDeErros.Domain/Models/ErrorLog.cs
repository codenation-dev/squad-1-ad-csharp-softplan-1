using CentralDeErros.Domain.Models.Base;
using CentralDeErros.CrossCutting.CustomTypes;

namespace CentralDeErros.Domain.Models
{
    public class ErrorLog : ModelBase
    {
        public string Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public bool Archieved { get; set; } = false;
        public ServerEnvironment Environment { get; set; } = ServerEnvironment.Unknown;
    }
}
