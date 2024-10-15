using System.Linq;
using UnityEditor;

namespace Nxlk.UXMLFixPath
{
    public class UxmlAssetPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths
        )
        {
            if (
                !Enumerable
                    .Concat(importedAssets, movedAssets)
                    .Concat(movedFromAssetPaths)
                    .Any(assetPath => assetPath.EndsWith(".uxml"))
            )
            {
                return;
            }

            new UpdateUXMLPathsCommand().Execute();
        }
    }
}
