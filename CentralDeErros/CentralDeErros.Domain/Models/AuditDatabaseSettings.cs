using CentralDeErros.Domain.Interfaces;

namespace CentralDeErros.Domain.Models
{
    public class AuditDatabaseSettings : IAuditDatabaseSettings
    {
        public string AuditCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
