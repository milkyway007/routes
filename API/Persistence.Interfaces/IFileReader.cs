namespace Persistence.Interfaces
{
    public interface IFileReader
    {
        public IEnumerable<string> Read(string path);
    }
}
