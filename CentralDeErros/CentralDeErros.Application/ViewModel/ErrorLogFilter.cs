using CentralDeErros.CrossCutting.CustomTypes;

namespace CentralDeErros.Application.ViewModel
{
    public class ErrorLogFilter
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? Level { get; set; }
        public string? Origin { get; set; }
        public bool? Archieved { get; set; }
        public ServerEnvironment? Environment { get; set; }
    }
}
