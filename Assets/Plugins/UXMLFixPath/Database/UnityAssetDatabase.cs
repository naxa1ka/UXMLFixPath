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
                .Select(guidAsString =>
                {
                    var guid = new GUID(guidAsString);
                    return new Asset(guid, () => GUIDToAssetPath(guid));
                });
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
