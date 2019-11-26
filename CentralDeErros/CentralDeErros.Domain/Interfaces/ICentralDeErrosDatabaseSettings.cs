namespace CentralDeErros.Domain.Interfaces
{
    public interface IAuditDatabaseSettings
    {
        string AuditCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
