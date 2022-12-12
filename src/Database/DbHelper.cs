using System.Runtime.InteropServices;

namespace Bad.Database
{
    public static class DbHelper
    {
        public static string DbConnectionString(IConfiguration conf)
        {
            var connectionString = conf.GetSection("Database").GetValue<string>("ConnectionString");

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connectionString = conf.GetSection("Database").GetValue<string>("MacConnectionString");
            }

            return connectionString;
        }
    }
}
