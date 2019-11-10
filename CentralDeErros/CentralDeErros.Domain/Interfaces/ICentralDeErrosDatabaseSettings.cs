namespace CentralDeErros.Domain.Interfaces
{
    public interface ICentralDeErrosDatabaseSettings
    {
        string ErrorLogCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
