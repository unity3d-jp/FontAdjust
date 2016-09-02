using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;


namespace FontAdjust
{
    public class FontAdjustWindow : EditorWindow
    {
        [MenuItem("Tools/FontAdjust/FontAdjustWindow")]
        public static void CreateWindow()
        {
            EditorWindow.GetWindow<FontAdjustWindow>();
        }

        private FontAdjustCore adjustCore = new FontAdjustCore();

        private int selectIndex = 0;
        private string[] selectStr = { "position up", "position down" };

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Mode",GUILayout.Width(50.0f) );
            selectIndex = EditorGUILayout.Popup(selectIndex, selectStr);
            EditorGUILayout.EndHorizontal();

            adjustCore.SetPositionUp(selectIndex == 0);

            if (GUILayout.Button("Execute each prefab"))
            {
                BulkConvertBatch.BulkConvertUtility.DoAllComponentsInPrefab<Text>(adjustCore.Execute, "Execute Font Adjust");
            }
            if (GUILayout.Button("Execute All Scene"))
            {
                BulkConvertBatch.BulkConvertUtility.DoAllComponentsInAllScene<Text>(adjustCore.Execute );
            }
            if (GUILayout.Button("Execute Current Scene"))
            {
                BulkConvertBatch.BulkConvertUtility.DoAllComponentsInCurrentScene<Text>(adjustCore.Execute);
            }
        }
    }
}
