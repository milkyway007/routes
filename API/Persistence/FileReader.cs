using Persistence.Interfaces;

namespace Persistence
{
    public class FileReader : IFileReader
    {
        public IEnumerable<string> Read(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var lines = new List<string>();

            using (StreamReader reader = new StreamReader(path))
            {
                var line = reader.ReadLine();

                while (line != null)
                {
                    lines.Add(line);
                    line = reader.ReadLine();
                }
            }
            
            return lines;
        }
    }
}
