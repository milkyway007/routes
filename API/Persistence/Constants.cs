using System.Reflection;

namespace Persistence
{
    public static class Constants
    {
        public static readonly string AIRPORT_PATH =
            Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +
            @"\Data\airports.csv";
        public static readonly string ROUTE_PATH =
            Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +
            @"\Data\routes.csv";
        public const string DELIMETER = ",(?=(?:[^\"\n]*\"[^\n]*\")*[^\"\n]*$)";
    }
}
