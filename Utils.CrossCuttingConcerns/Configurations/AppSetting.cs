namespace Utils.CrossCuttingConcerns.Configurations
{
    public class SchemaSettings
    {
        public string? MainSchema { get; set; }
    }

    public class AppSetting
    {
        public SchemaSettings? SchemaSettings { get; set; }

        public string Secret { get; set; }
    }
}
