using System.Runtime.InteropServices;

namespace Bad
{
    public static class DbHelper
    {
        public static string DbConnectionString(IConfiguration conf)
        {
            var connectionString = conf.GetSection("Database").GetValue<string>("ConnectionString");
            return connectionString;
        }
    }
}
