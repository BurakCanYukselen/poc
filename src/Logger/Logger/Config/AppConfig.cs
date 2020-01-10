namespace Logger
{
    public interface IAppConfig
    {
        string LocalDB { get; set; }
    }

    public class AppConfig : IAppConfig
    {
        public string LocalDB { get; set; }
    }
}
