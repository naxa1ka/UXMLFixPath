using UnityEditor;
using UnityEngine;

namespace Nxlk.UXMLFixPath
{
    public class UXMLPathsUpdaterEditorWindow : EditorWindow
    {
        [MenuItem("Nxlk/Update UXML Paths")]
        public static void ShowWindow()
        {
            GetWindow<UXMLPathsUpdaterEditorWindow>("UXML Path Updater");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Update UXML Paths"))
            {
                new UpdateUXMLPathsCommand().Execute();
            }
        }
    }
}
