namespace ReadingEnhancer.DataAccess.Configurations
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string EnhancedTextsCollection { get; set; }
        public string UsersCollection { get; set; }
    }
}