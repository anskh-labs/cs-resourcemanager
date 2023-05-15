using ResourceManager.Configuration;
using ResourceManager.Helpers;

namespace ResourceManager.ViewModels
{
    public class EbookOptionsContentViewModel : PageContentViewModel
    {
        public EbookOptionsContentViewModel()
        {
            MenuTitle = "Options";
            MenuIcon = AssetManager.Instance.GetImage("Options.png");
            Title = "Ebook Options";
            PageColor = Constants.EbookPageColor;

            FolderPath = App.Settings.EbookSettings.FolderPath;
            SupportExtensions = App.Settings.EbookSettings.SupportExtensions;
            CoverPath = App.Settings.EbookSettings.CoverPath;
            CoverFileExtensions =  App.Settings.EbookSettings.CoverFileExtensions;
            DeleteSourceFile =  App.Settings.EbookSettings.DeleteSourceFile.ToString();
        }
        public string FolderPath { get; }
        public string SupportExtensions { get; }
        public string CoverPath { get; }
        public string CoverFileExtensions { get; }
        public string DeleteSourceFile { get; }
    }
}