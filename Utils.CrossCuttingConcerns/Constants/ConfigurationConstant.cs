namespace Utils.CrossCuttingConcerns.Constants
{
    public static class ConfigurationConstant
    {
        public const int DefaultCacheSize = 1024;

        public const string CorsPolicy = nameof(CorsPolicy);

        public const string AllowedOrigins = "Cors:AllowedOrigins";

        public const string DefaultContentType = "application/json";

        public const string DefaultApiVersion = "1.0";

        public const string AuthorizationHeader = "Authorization";

        public const string TimeZoneKey = "X-Timezone-Offset";

        public const string Swagger = nameof(Swagger);

        public const string SecretKey = "AppSettings:Secret";
    }
}
