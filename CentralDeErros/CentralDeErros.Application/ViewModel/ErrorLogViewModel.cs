using CentralDeErros.CrossCutting.CustomTypes;
using System;

namespace CentralDeErros.Application.ViewModel
{
    public class ErrorLogViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public string Origin { get; set; }
        public bool Archieved { get; set; }
        public ServerEnvironment Environment { get; set; }
    }
}
