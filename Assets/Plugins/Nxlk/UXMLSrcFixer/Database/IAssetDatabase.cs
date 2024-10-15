using System.Collections.Generic;
using UnityEditor;

namespace Nxlk.UXMLSrcFixer
{
    public interface IAssetDatabase
    {
        IEnumerable<IAsset> FindAssets(IAssetFilter filter);
        void Refresh();
        string GUIDToAssetPath(GUID guid);
    }
}