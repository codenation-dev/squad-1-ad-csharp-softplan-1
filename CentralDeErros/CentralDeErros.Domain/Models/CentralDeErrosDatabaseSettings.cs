using CentralDeErros.Domain.Interfaces;

namespace CentralDeErros.Domain.Models
{
    public class CentralDeErrosDatabaseSettings : ICentralDeErrosDatabaseSettings
    {
        public string ErrorLogCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
