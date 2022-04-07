using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace NaiveAPI
{
    public class window_itemCustomInfoEditor : item_windowAPI
    {
        public static window_itemCustomInfoEditor Instance { get; private set; }
        public static bool IsOpen
        {
            get { return Instance != null; }
        }
        [MenuItem("Window/NaiveAPI/Custom Infomation Editor")]
        public static void ShowWindow()
        {
            GetWindow<window_itemCustomInfoEditor>("CustomInfo Editor");
        }


        private string[] valueType = new string[5];

        private Vector2 scrollPos = new Vector2();
        private List<itemCustomInfoData> dataList = new List<itemCustomInfoData>();
        private List<string> dataListLayout = new List<string>();
        private itemCustomInfoPage page = itemCustomInfoPage.editInfo;
        private int dataListIndex = 0;
        private string newValueName;
        private int newValueType;

        private void OnEnable()
        {
            Instance = this;
            minSize = new Vector2(300, 300);
            dataPreset();
            loadData();
            reflushDataListLayout();

            valueType[0] = "int";
            valueType[1] = "float";
            valueType[2] = "bool";
            valueType[3] = "string";
            valueType[4] = "GameObject";
        }
        private void OnGUI()
        {
            switch (page)
            {
                case itemCustomInfoPage.editInfo:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Custom Info", GUILayout.Width(75));
                    
                    dataListIndex = EditorGUILayout.Popup(dataListIndex, dataListLayout.ToArray(), GUILayout.Width((position.width - 75) / 1.5f));
                    if (GUILayout.Button("edit"))
                    {
                        page = itemCustomInfoPage.editDataList;
                    }
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(10);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("  Type", GUILayout.Width(40));
                    newValueType = EditorGUILayout.Popup(newValueType,valueType);
                    EditorGUILayout.LabelField("  Name", GUILayout.Width(45));
                    newValueName = EditorGUILayout.TextField(newValueName);
                    EditorGUILayout.EndHorizontal();
                    if (GUILayout.Button("add Value"))
                    {
                        itemCustomInfoData.valueData add = new itemCustomInfoData.valueData();
                        add.name = newValueName;
                        newValueName = "";
                        add.type = valueType[newValueType];
                        dataList[dataListIndex].valueDatas.Add(add);
                        saveData();
                    }
                    GUILayout.Space(10);
                    try { infoLayout(); } catch { }

                    if(GUILayout.Button("Generate Script"))
                    {
                        reflushCsScript();
                    }

                    break;

                case itemCustomInfoPage.editDataList:
                    {
                        EditorGUILayout.LabelField("Custom Info List");
                        infoListLayout();
                        EditorGUILayout.BeginHorizontal();
                        if (GUILayout.Button("Save"))
                        {
                            page = itemCustomInfoPage.editInfo;
                            reflushDataListLayout();
                            saveData();
                        }
                        if (GUILayout.Button("cancel"))
                        {
                            page = itemCustomInfoPage.editInfo;
                            loadData();
                            reflushDataListLayout();
                        }
                        EditorGUILayout.EndHorizontal();
                        break;
                    }
            }
        }

        private void reflushCsScript()
        {
            DirectoryInfo dir = new DirectoryInfo(dataPath.customInfoFloder);
            FileInfo[] info = dir.GetFiles("*.cs*");
            foreach (FileInfo f in info)
            {
                File.Delete(f.ToString());
            }

            reflushDataListLayout();
            for(int i = 0; i < dataListLayout.Count; i++)
            {
                string code;
                code = scriptCode.one + dataListLayout[i] + scriptCode.two + dataListLayout[i] + scriptCode.three;
                foreach (itemCustomInfoData.valueData j in dataList[i].valueDatas)
                {
                    code += "    public " + j.type + ' ' + j.name + ";\n";
                }
                code += scriptCode.four;
                File.WriteAllText(dataPath.customInfoFloder + dataListLayout[i]+ ".cs", code);
            }

            AssetDatabase.Refresh();
        }

        private void saveData()
        {
            saveList<itemCustomInfoData> saveList = new saveList<itemCustomInfoData>();
            saveList.list = dataList;
            file_System.SaveDataAsJson(saveList, dataPath.customInfoFloder, "custom info list.json");
        }
        private void loadData()
        {
            saveList<itemCustomInfoData> saveList = new saveList<itemCustomInfoData>();
            file_System.LoadDataAsJson(dataPath.customInfoList, saveList);
            dataList = saveList.list;
        }
        public void infoListLayout()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(150));
            for (int i = 0; i < dataList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                dataList[i].name = EditorGUILayout.TextField(dataList[i].name);
                if (GUILayout.Button("Delete"))
                {
                    dataList.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
            GUILayout.Space(10);
            if (GUILayout.Button("Add new slot"))
            {
                dataList.Add(new itemCustomInfoData());
            }
        }
        public void infoLayout()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(150));
            for (int i = 0; i < dataList[dataListIndex].valueDatas.Count; i++)
            {
                itemCustomInfoData.valueData next = dataList[dataListIndex].valueDatas[i];
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(next.type,GUILayout.Width(80));
                EditorGUILayout.LabelField("|   "+next.name, GUILayout.Width(150));
                if (GUILayout.Button("Delete"))
                {
                    dataList[dataListIndex].valueDatas.RemoveAt(i);
                    saveData();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
            GUILayout.Space(10);
        }
        public void reflushDataListLayout()
        {
            dataListLayout.Clear();
            foreach(itemCustomInfoData i in dataList)
            {
                dataListLayout.Add(i.name);
            }
        }


        private class scriptCode
        {
            public static string one { get { return "using UnityEditor;\nusing UnityEngine;\n[CreateAssetMenu(menuName = \"NaiveAPI/Item System/Custom Infomation/"; } }
            public static string two { get { return "\")]\npublic class "; } }
            public static string three { get { return " : ScriptableObject\n{\n"; } }
            public static string four { get { return "    public Object relatedOn;\n    private void OnEnable()\n    {\n        if (relatedOn == null && AssetDatabase.GetAssetPath(this) != \"\")\n        {\n            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(this));\n            AssetDatabase.Refresh();\n        }\n    }\n}"; } }
        }
        
    }
}
