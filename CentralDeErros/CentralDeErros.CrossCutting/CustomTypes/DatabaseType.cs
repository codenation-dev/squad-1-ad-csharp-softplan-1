namespace CentralDeErros.CrossCutting.CustomTypes
{
    public enum DatabaseType { Unknow, PostgreSQL, SQLServer, LocalDB }

    public static class DatabaseTypeExtension
    {
        public static DatabaseType ToDatabaseType(this string str)
        {
            if (str == null)
            {
                return DatabaseType.Unknow;
            }

            switch (str.ToUpper())
            {
                case "POSTGRESQL":
                    return DatabaseType.PostgreSQL;
                case "SQLSERVER":
                    return DatabaseType.SQLServer;
                case "LOCALDB":
                    return DatabaseType.LocalDB;
                default :
                    return DatabaseType.Unknow;
            }
        }
    }
}
