namespace ResourceManager.Settings
{
    public class SystemIntegrationSettings
    {
        public const string SectionName = "SystemIntegration";

        public bool ShowTrayIcon { get; set; }

        public bool MinimizeToTrayOnStartup { get; set; }
    }
}
