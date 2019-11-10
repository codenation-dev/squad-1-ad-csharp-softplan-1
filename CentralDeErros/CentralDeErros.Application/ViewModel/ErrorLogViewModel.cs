using System;
using System.Collections.Generic;
using System.Text;

namespace CentralDeErros.Application.ViewModel
{
    public class ErrorLogViewModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public bool Shelved { get; set; }
        public string Environment { get; set; }
    }
}
