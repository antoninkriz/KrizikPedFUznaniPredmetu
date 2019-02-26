namespace KarolinkaUznani.Common.Database.MySqlDatabase
{
    public class MySqlOptions
    {
        public string Server { get; set; }
    
        public uint Port { get; set; }
    
        public string Password { get; set; }
    
        public string User { get; set; }

        public string Database { get; set; }

        public bool Seed { get; set; }
    }
}