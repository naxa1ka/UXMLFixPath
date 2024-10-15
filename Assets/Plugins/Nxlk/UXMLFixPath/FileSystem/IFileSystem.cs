namespace Nxlk.UXMLFixPath
{
    public interface IFileSystem
    {
        string GetFullPath(string path);
        string ReadAllText(string path);
        void WriteAllText(string path, string content);
    }
}