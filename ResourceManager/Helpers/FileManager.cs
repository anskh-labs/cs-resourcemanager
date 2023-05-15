using System.Diagnostics;
using System.IO;

namespace ResourceManager.Helpers
{
    internal class FileManager
    {
        private static readonly FileManager _instance = new FileManager();
        private FileManager()
        { }
        public static FileManager Instance => _instance;

        public bool ExploreFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }
            //Clean up file path so it can be navigated OK
            filePath = Path.GetFullPath(filePath);
            Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            return true;
        }
    }
}
