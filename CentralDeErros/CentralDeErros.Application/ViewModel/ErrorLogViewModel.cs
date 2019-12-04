using System;
using System.Collections.Generic;
using System.Text;

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
        public bool Archieved { get; set; }
        public string Environment { get; set; }
    }
}
