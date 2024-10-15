using System;

namespace Nxlk.UXMLSrcFixer
{
    public class Asset : IAsset
    {
        private readonly Lazy<string> _path;

        public string Path => _path.Value;

        public Asset(string path)
            : this(new Lazy<string>(path)) { }

        public Asset(Func<string> path)
            : this(new Lazy<string>(path)) { }

        public Asset(Lazy<string> path)
        {
            _path = path;
        }

        public override string ToString() => $"{nameof(Path)}: {Path}";
    }
}
