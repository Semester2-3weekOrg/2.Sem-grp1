using System.IO;

namespace TheMovies.Data
{
    internal class DataHandler<T>
    {
        private static string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static string folder = Path.Combine(projectPath, "Data");
        private static string subFolder = Path.Combine(folder, "SavedFiles");

        private readonly string _filePath;
        private readonly Func<T, string> _serializeFunc;
        private readonly Func<string, T> _deserializeFunc;

        public DataHandler(Func<T, string> serializeFunc, Func<string, T> deserializeFunc)
        {


            _filePath = Path.Combine(subFolder, $"{typeof(T).Name}.csv");
            _serializeFunc = serializeFunc ?? throw new ArgumentNullException(nameof(serializeFunc));
            _deserializeFunc = deserializeFunc ?? throw new ArgumentNullException(nameof(deserializeFunc));
        }

        public List<T> Load()
        {
            if (!File.Exists(_filePath))
                return new List<T>();

            return File.ReadAllLines(_filePath)
                       .Where(line => !string.IsNullOrWhiteSpace(line))
                       .Select(_deserializeFunc)
                       .ToList();
        }

        public void Save(IEnumerable<T> data)
        {
            File.WriteAllLines(_filePath, data.Select(_serializeFunc));
        }
    }
}
