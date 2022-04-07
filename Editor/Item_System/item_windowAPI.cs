using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace NaiveAPI
{
    public class item_windowAPI : EditorWindow
    {
        public enum itemEditorPage
        {
            create,
            editItem,
            editGroup
        }
        public enum itemCustomInfoPage
        {
            editInfo,
            editDataList
        }
        public void dataPreset()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Samples")) AssetDatabase.CreateFolder("Assets", "Samples");
            if (!AssetDatabase.IsValidFolder("Assets/Samples/NaiveUnity Pack")) AssetDatabase.CreateFolder("Assets/Samples", "NaiveUnity Pack");
            if (!AssetDatabase.IsValidFolder("Assets/Samples/NaiveUnity Pack/config")) AssetDatabase.CreateFolder("Assets/Samples/NaiveUnity Pack", "config");
            if (!AssetDatabase.IsValidFolder("Assets/Samples/NaiveUnity Pack/config/custom item infomation")) AssetDatabase.CreateFolder("Assets/Samples/NaiveUnity Pack/config", "custom item infomation");

            if (!File.Exists(dataPath.itemGroup)) file_System.SaveDataAsJson(new itemEditorData(), "Assets/Samples/NaiveUnity Pack/config/", "item group list.json");
            if (!File.Exists(dataPath.customInfoList)) file_System.SaveDataAsJson(new char(), dataPath.customInfoFloder, "custom info list.json");
        }

        public void stringListLayout(ref List<string> list,ref Vector2 scrollPoint , int scrollMax)
        {
            scrollPoint = EditorGUILayout.BeginScrollView(scrollPoint, GUILayout.Width(position.width), GUILayout.Height(scrollMax));
            for (int i = 0; i < list.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                list[i] = EditorGUILayout.TextField(list[i]);
                if (GUILayout.Button("Delete"))
                {
                    list.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
            GUILayout.Space(10);
            if (GUILayout.Button("Add new slot"))
            {
                list.Add("");
            }
        }

        public class dataPath
        {
            public static string itemGroup { get { return "Assets/Samples/NaiveUnity Pack/config/item group list.json"; } }
            public static string customInfoList { get { return "Assets/Samples/NaiveUnity Pack/config/custom item infomation/custom info list.json"; } }
            public static string customInfoFloder { get { return "Assets/Samples/NaiveUnity Pack/config/custom item infomation/"; } }

            

        }

        public class saveList<T>
        {
            public List<T> list = new List<T>();
        }
        public class itemEditorData
        {
            public itemEditorPage page;
            public DefaultAsset itemFolder;
            public DefaultAsset prefabFolder;
            public List<string> list = new List<string>();
        }
        [Serializable]
        public class itemCustomInfoData
        {
            public string name;
            public List<valueData> valueDatas = new List<valueData>();

            [Serializable]
            public class valueData
            {
                public string name ;
                public string type ;
            }
        }
    }
}
