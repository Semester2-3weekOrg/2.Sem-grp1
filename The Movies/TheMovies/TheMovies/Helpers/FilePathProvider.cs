using System.IO;

namespace TheMovies.Helpers
{
    public static class FilePathProvider
    {
        private static readonly string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static readonly string folder = Path.Combine(projectPath, "Data");
        private static readonly string subDataFolder = Path.Combine(folder, "SavedData");

        public static string GetSavedDataFilesPath(string fileName)
        {
            if (!Directory.Exists(subDataFolder))
            {
                Directory.CreateDirectory(subDataFolder);
            }
            return Path.Combine(subDataFolder, fileName);
        }
    }
}
