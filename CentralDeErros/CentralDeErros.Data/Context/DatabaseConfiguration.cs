using CentralDeErros.CrossCutting.CustomExceptions;
using CentralDeErros.CrossCutting.CustomTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;

namespace CentralDeErros.Data.Context
{
    public class DatabaseConfiguration
    {
        private readonly IConfiguration _configuration;
        private readonly DbContextOptionsBuilder _optionsBuilder;

        public DatabaseConfiguration(DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            _configuration = configuration;
            _optionsBuilder = optionsBuilder;
        }
        public void ConfigureConnection()
        {
            DatabaseType databaseType = GetDatabaseType();

            switch (databaseType)
            {
                case DatabaseType.PostgreSQL:
                    _optionsBuilder.UseNpgsql(GetConnectionString(databaseType));
                    break;
                case DatabaseType.SQLServer:
                    _optionsBuilder.UseSqlServer(GetConnectionString(databaseType));
                    break;
                case DatabaseType.LocalDB:
                    _optionsBuilder.UseSqlServer(GetConnectionString(databaseType));
                    break;
                default:
                    _optionsBuilder.UseSqlServer(GetConnectionString(DatabaseType.LocalDB));
                    break;
            }
        }

        private DatabaseType GetDatabaseType()
        {
            DatabaseType databaseType = _configuration.GetConnectionString(ConfigurationConst.DatabaseType).ToDatabaseType();
            return databaseType == DatabaseType.Unknow ? Environment.GetEnvironmentVariable(ConfigurationConst.DatabaseType).ToDatabaseType() : databaseType;
        }

        public string GetConnectionString(DatabaseType databaseType = DatabaseType.LocalDB)
        {
            if (databaseType == DatabaseType.LocalDB)
            {
                return "Server = (localdb)\\mssqllocaldb; Database = CentralDeErrosDb; Trusted_Connection = True; MultipleActiveResultSets = true";
            }

            if (Environment.GetEnvironmentVariable(ConfigurationConst.DeployEnvironment) == DeployEnvironment.Heroku)
            {
                return GetHerokuConnectionString();
            }

            if (Environment.GetEnvironmentVariable(ConfigurationConst.ConnectionString) != null)
            {
                return Environment.GetEnvironmentVariable("ConfigurationConst.ConnectionString");
            }

            if (_configuration.GetConnectionString(ConfigurationConst.DefaultConnection) != null)
            {
                return _configuration.GetConnectionString(ConfigurationConst.DefaultConnection);
            }

            throw new DatabaseConnectionStringException("Connection string not found.");
        }

        private string GetHerokuConnectionString()
        {
            var databaseUrl = Environment.GetEnvironmentVariable(ConfigurationConst.DatabaseURL);
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };

            return builder.ToString();
        }
    }
}