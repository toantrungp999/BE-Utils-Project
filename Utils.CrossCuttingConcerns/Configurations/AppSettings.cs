namespace Utils.CrossCuttingConcerns.Configurations
{
    public class SchemaSettings
    {
        public string MainSchema { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

    public class AppSettings
    {
        public SchemaSettings SchemaSettings { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }

        public string Secret { get; set; }
    }
}
