using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

namespace FontAdjust
{
    public class FontAdjustTest : EditorWindow
    {
        [MenuItem("Tools/FontAdjust/Debug/CreateTest")]
        public static void CreateWindow()
        {
            EditorWindow.GetWindow<FontAdjustTest>();
        }

        private FontAdjustCore adjustCore = new FontAdjustCore();

        private GameObject testPrefab;

        void OnGUI()
        {
            EditorGUILayout.LabelField("Test");
            testPrefab = (GameObject)EditorGUILayout.ObjectField(testPrefab, typeof(GameObject), false);

            if (GUILayout.Button("CreateTest"))
            {
                CreateTest();
            }
            EditorGUILayout.LabelField("Adjust In this scene");
            if (GUILayout.Button("AjustTo53"))
            {
                adjustCore.SetPositionUp( false );
                BulkConvertBatch.BulkConvertUtility.DoAllComponentsInCurrentScene<Text>(adjustCore.Execute);
            }
            if (GUILayout.Button("AjustFrom53"))
            {
                adjustCore.SetPositionUp( true );
                BulkConvertBatch.BulkConvertUtility.DoAllComponentsInCurrentScene<Text>(adjustCore.Execute);
            }
        }




        private void CreateTest()
        {

            EditorSceneManager.OpenScene("Assets/test.unity");
            var fontList = GetProjectFontList();
            var rootObj = GameObject.Find("Canvas").transform;
            // reset
            var childList = new List<GameObject>();
            foreach (Transform child in rootObj)
            {
                childList.Add(child.gameObject);
            }
            foreach (var c in childList)
            {
                GameObject.DestroyImmediate(c, true);
            }
            // add children
            int idx = 0;
            foreach (var font in fontList)
            {
                var gmo = GameObject.Instantiate<GameObject>(this.testPrefab);
                gmo.name = idx.ToString();
                gmo.transform.parent = rootObj;
                SetupTestObject(gmo, font, idx);
                ++idx;
            }
            EditorUtility.SetDirty(rootObj);
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }

        private static void SetupTestObject(GameObject gmo, Font font, int idx)
        {
            RectTransform rectTrans = gmo.GetComponent<RectTransform>();
            rectTrans.localScale = Vector3.one;
            rectTrans.localPosition = Vector3.down * (idx + 1) * 30;
            Text text = gmo.GetComponentInChildren<Text>();
            text.text = font.name;
            text.font = font;
            text.gameObject.name = font.name;
        }

        private static List<Font> GetProjectFontList()
        {
            List<Font> projectFontList = new List<Font>();
            var guids = AssetDatabase.FindAssets("t:Font");
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var font = AssetDatabase.LoadAssetAtPath<Font>(path);
                projectFontList.Add(font);
            }
            return projectFontList;
        }
    }
}