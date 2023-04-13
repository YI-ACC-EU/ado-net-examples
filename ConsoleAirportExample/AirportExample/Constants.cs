namespace AirportExample;

public static  class Constants
{
    public const string ConnectionString =
        @"Server=127.0.0.1; Database=Aeroporti; Integrated Security=true; TrustServerCertificate=True";


    public static class CrudOperations
    {
        public const string ExitChoice = "X";
        public const string Create = "C";
        public const string Update = "U";
        public const string Delete = "D";
        public static Dictionary<string, string> Operations() =>
            new()
            {
                {ExitChoice, "Torna al menu" },
                {Create, "Crea"},
                {Update, "Modifica"},
                {Delete, "Elimina"}
            };
    }
}