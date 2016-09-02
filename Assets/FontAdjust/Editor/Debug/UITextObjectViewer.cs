using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

namespace FontAdjust
{
    public class UITextObjectViewer : EditorWindow
    {
        [MenuItem("Tools/FontAdjust/Debug/UITextView")]
        public static void CreateWindow()
        {
            EditorWindow.GetWindow<UITextObjectViewer>();
        }

        Text currentText;

        void OnGUI()
        {
            currentText = (Text)EditorGUILayout.ObjectField(currentText, typeof(Text), true);
            if (currentText == null) { return; }
            OutputData("lineSpacing", "" + currentText.lineSpacing);
            OutputData("flexibleHeight", "" + currentText.flexibleHeight);
            var r = currentText.GetPixelAdjustedRect();

            OutputData("GetPixelAdjustedRect", "" + r.ToString());
        }
        private void OutputData(string title, string value)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(title);
            EditorGUILayout.LabelField(value);
            EditorGUILayout.EndHorizontal();
        }
    }
}