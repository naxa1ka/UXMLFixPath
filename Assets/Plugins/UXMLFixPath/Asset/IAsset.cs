
using UnityEditor;

namespace Nxlk.UXMLFixPath
{
    public interface IAsset
    {
        GUID GUID { get; }
        string Path { get; }
    }
}