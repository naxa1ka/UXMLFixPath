using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Nxlk.UXMLFixPath
{
    public class UnityAssetDatabase : IAssetDatabase
    {
        public IEnumerable<IAsset> FindAssets(IAssetFilter filter)
        {
            return AssetDatabase
                .FindAssets(filter.AsString())
                .Select(guidAsString => new Asset(() => GUIDToAssetPath(new GUID(guidAsString))));
        }

        public string GUIDToAssetPath(GUID guid)
        {
            return AssetDatabase.GUIDToAssetPath(guid);
        }

        public void Refresh()
        {
            AssetDatabase.Refresh();
        }
    }
}
