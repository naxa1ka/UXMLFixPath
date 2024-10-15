using System.IO;

namespace Nxlk.UXMLSrcFixer
{
    public class FileSystem : IFileSystem
    {
        public string GetFullPath(string path)
        {
            return Path.GetFullPath(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public void WriteAllText(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}