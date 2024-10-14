using System;
using UnityEditor;

namespace Nxlk.UXMLFixPath
{
    public class Asset : IAsset
    {
        private readonly Lazy<string> _path;
        
        public GUID GUID { get; }
        public string Path => _path.Value;

        public Asset(GUID guid, string path) : this(guid, new Lazy<string>(path))
        {
        } 
        
        public Asset(GUID guid, Func<string> path) : this(guid, new Lazy<string>(path))
        {
        } 
        
        public Asset(GUID guid, Lazy<string> path)
        {
            GUID = guid;
            _path = path;
        }

        public override string ToString() => $"{nameof(GUID)}: {GUID}, {nameof(Path)}: {Path}";
    }
}