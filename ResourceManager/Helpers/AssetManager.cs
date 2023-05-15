using System;
using System.Windows.Media.Imaging;

namespace ResourceManager.Helpers
{
    internal class AssetManager
    {
        private static readonly AssetManager _instance = new AssetManager();
        private AssetManager()
        { }
        public static AssetManager Instance => _instance;
        public BitmapImage GetImage(string resourceName)
        {
            var image = new BitmapImage();

            string resourceLocation =
                string.Format("pack://application:,,,/ResourceManager;component/Resources/Assets/Images/{0}", resourceName);

            try
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                image.UriSource = new Uri(resourceLocation);
                image.EndInit();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.ToString());
            }

            return image;
        }
    }
}
