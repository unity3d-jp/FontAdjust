using UnityEngine;
using UnityEditor;
using System.Collections;

namespace FontAdjust
{
    public class FontDataViewer : EditorWindow
    {

        [MenuItem("Tools/FontAdjust/Debug/FontDataView")]
        public static void CreateWindow()
        {
            EditorWindow.GetWindow<FontDataViewer>();
        }

        private Vector2 scrollPos;
        private Font currentFont;
        private char charaInfoCh = 'A';
        private FontMetricsData parsedFontMetricsData;

        void OnGUI()
        {
            var oldFont = currentFont;
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            currentFont = (Font)EditorGUILayout.ObjectField(currentFont, typeof(UnityEngine.Font), false);
            if (currentFont == null)
            {
                EditorGUILayout.EndScrollView(); 
                return;
            }
            EditorGUILayout.LabelField("Information");
            OutputData("ascent", "" + currentFont.ascent);
            OutputData("dynamic", "" + currentFont.dynamic);
            OutputData("fontSize", "" + currentFont.fontSize);
            OutputData("lineHeight", "" + currentFont.lineHeight);
            OutputData("path", AssetDatabase.GetAssetPath(currentFont));

            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField("Parsed Data");
            if (oldFont != currentFont)
            {
                parsedFontMetricsData = FontMetricsData.CreateFontMetricsData(AssetDatabase.GetAssetPath(currentFont));
            }
            if (parsedFontMetricsData == null)
            {
                EditorGUILayout.EndScrollView();
                return;
            }
            OutputData("ascent", "" + parsedFontMetricsData.ascent);
            OutputData("descent", "" + parsedFontMetricsData.descent);
            OutputData("emHeight", "" + parsedFontMetricsData.emHeight);
            OutputData("leading", "" + parsedFontMetricsData.leading);
            OutputData("lineSpace", "" + parsedFontMetricsData.lineSpace);

            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField("Calculate Data");
            OutputData("ascent", "" + parsedFontMetricsData.GetCalculatedAscent(currentFont.fontSize));
            OutputData("descent", "" + parsedFontMetricsData.GetCalculatedDescent(currentFont.fontSize));
            OutputData("leading", "" + parsedFontMetricsData.GetCalculatedLeading(currentFont.fontSize));
            OutputData("lineSpace", "" + parsedFontMetricsData.GetCalculatedLineSpace(currentFont.fontSize));

            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField("CharacterInfo");
            var str = EditorGUILayout.TextField(charaInfoCh.ToString());
            if (str.Length > 0)
            {
                charaInfoCh = str[0];
            }
            CharacterInfo aInfo;
            if (!currentFont.GetCharacterInfo(charaInfoCh, out aInfo, currentFont.fontSize))
            {
                currentFont.RequestCharactersInTexture(charaInfoCh.ToString(), currentFont.fontSize);
                if (!currentFont.GetCharacterInfo(charaInfoCh, out aInfo, currentFont.fontSize))
                {
                    EditorGUILayout.EndScrollView();
                    return;
                }
            }
            OutputData("advance", "" + aInfo.advance);
            OutputData("bearing", "" + aInfo.bearing);
            OutputData("glyphHeight", "" + aInfo.glyphHeight);
            OutputData("maxY", "" + aInfo.maxY);
            OutputData("minY", "" + aInfo.minY);
            OutputData("size", "" + aInfo.size);

            EditorGUILayout.EndScrollView();
            //        currentFont.characterInfo[0].
            //        currentFont.GetCharacterInfo
        }

        private void OutputData(string title, string value)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("  " + title);
            EditorGUILayout.LabelField(value);
            EditorGUILayout.EndHorizontal();
        }
    }
}