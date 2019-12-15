using CentralDeErros.CrossCutting.CustomExceptions;
using CentralDeErros.CrossCutting.CustomTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;

namespace CentralDeErros.Data.Context
{
    public static class ConfigureConnectioExtension
    {
        public static void ConfigureConnection(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            DatabaseConfiguration databaseConfiguration = new DatabaseConfiguration(optionsBuilder, configuration);
            databaseConfiguration.ConfigureConnection();
        }
    }
}