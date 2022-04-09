using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace NaiveAPI
{
    public class window_itemEditor : item_windowAPI
    {
        public static window_itemEditor Instance { get; private set; }
        public static bool IsOpen
        {
            get { return Instance != null; }
        }
        [MenuItem("Window/NaiveAPI/Item Editor")]
        public static void ShowWindow()
        {
            GetWindow<window_itemEditor>("CustomInfo Editor");
        }

        private itemEditorPage pageSelected = itemEditorPage.create;
        private itemEditorPage lastPage = itemEditorPage.create;
        private DefaultAsset targetFolder = null,iconFolder=null;
        private string targetFolderPath;
        private bool isLoadIconFromPrefab = false;
        private SerializedObject serializedObject;
        private item_itemType targetItem;
        private int stackLimit = 1;
        private List<int> groupIndex = new List<int>();
        private List<string> groupList = new List<string>();
        private Vector2 itemGroupScrollPosition = new Vector2(0,0);
        private Vector2 groupScrollPosition = new Vector2(0,0);
        private Vector2 allScrollPosition = new Vector2(0,0);

        private int dataListIndex = 0;
        private List<itemCustomInfoData> dataList = new List<itemCustomInfoData>();
        private List<string> dataListLayout = new List<string>();
        UnityEngine.Object customInfoObject;
        // for image setting
        private int x = 0, y = 0, scale = 0;
        // itemType
        [HideInInspector]
        private Texture2D icon,iconFromPrefab;
        [HideInInspector]
        private string itemName,displayName;
        [HideInInspector]
        private GameObject prefab;


        private void OnEnable()
        {
            Instance = this;
            minSize = new Vector2(300, 300);
            dataPreset();
            loadData();
            if (groupIndex.Count == 0) groupIndex.Add(0);
            

            reflushDataListLayout();
        }

        private void OnDestroy()
        {
            saveData();
        }

        private void OnGUI()
        {
            allScrollPosition = EditorGUILayout.BeginScrollView(allScrollPosition,GUILayout.Width(position.width),GUILayout.Height(position.height));


            if (pageSelected == itemEditorPage.create)
            {
                if (GUILayout.Button("Create Mode"))
                {
                    pageSelected = itemEditorPage.editItem;
                    lastPage = itemEditorPage.editItem;
                    saveData();
                }
            }
            else if(pageSelected == itemEditorPage.editItem)
            {
                if (GUILayout.Button("Edit Mode"))
                {
                    pageSelected = itemEditorPage.create;
                    lastPage = itemEditorPage.create;
                    groupIndex.Clear();
                    groupIndex.Add(0);
                    saveData();
                }
            }

            switch (pageSelected)
            {
                case itemEditorPage.create:
                    {
                        targetItem = null;
                        targetFolder = (DefaultAsset)EditorGUILayout.ObjectField("Generate locate", targetFolder, typeof(DefaultAsset), false);
                        targetFolderPath = AssetDatabase.GetAssetPath(targetFolder);


                        itemName = EditorGUILayout.TextField("Item Name", itemName);
                        displayName = EditorGUILayout.TextField("Display Name", displayName);
                        stackLimit = EditorGUILayout.IntField("Stack limit", stackLimit);
                        prefab = (GameObject)EditorGUILayout.ObjectField("GameObject (prefab)", prefab, typeof(GameObject), false);
                        

                        if (!isLoadIconFromPrefab)
                        {
                            groupLayout();
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Item icon", GUILayout.Width(75));
                            icon = (Texture2D)EditorGUILayout.ObjectField(icon, typeof(Texture2D), false);
                            if (GUILayout.Button("Load form\nPrefab"))
                            {
                                isLoadIconFromPrefab = !isLoadIconFromPrefab;
                                x = 0;
                                y = 0;
                                scale = 0;
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        else
                        {
                            EditorGUILayout.BeginHorizontal();
                            // ¹LÂo¹H³W¿é¤J
                            Sprite tempSprite = prefab.GetComponent<SpriteRenderer>().sprite;
                            if (x == 0 || x < 0) x = 0;
                            if (y == 0 || y < 0) y = 0;
                            int minWH = (int)(tempSprite.texture.width < tempSprite.texture.height ? tempSprite.texture.width : tempSprite.texture.height);
                            if (scale == 0 || scale < 0) scale = 0;
                            if (scale > minWH) scale = minWH;
                            if (x + scale > tempSprite.texture.width) x = (int)tempSprite.texture.width - scale;
                            if (y + scale + 1 > tempSprite.texture.height) y = (int)tempSprite.texture.height - scale;

                            iconFromPrefab = (Texture2D)EditorGUILayout.ObjectField("icon preview", iconFromPrefab, typeof(Texture2D), false);
                            try { iconFromPrefab = Sprite2Texture(tempSprite, x, y, scale); } catch { Debug.Log("There is no Prefab or Texture is not Readable !"); }

                            if (GUILayout.Button("Cancel"))
                            {
                                isLoadIconFromPrefab = false;
                            }
                            EditorGUILayout.EndHorizontal();
                            x = EditorGUILayout.IntField("x", x);
                            y = EditorGUILayout.IntField("y", y);
                            scale = EditorGUILayout.IntField("scale", scale);
                            iconFolder = (DefaultAsset)EditorGUILayout.ObjectField("Save locate", iconFolder, typeof(DefaultAsset), false);

                            if (GUILayout.Button("Save and load icon"))
                            {
                                string path = AssetDatabase.GetAssetPath(iconFolder) + "/" + itemName + "_icon.asset";
                                AssetDatabase.CreateAsset(iconFromPrefab, path);
                                icon = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
                                isLoadIconFromPrefab = false;
                            }
                        }

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Custom Infomation",GUILayout.Width(140));
                        dataListIndex = EditorGUILayout.Popup(dataListIndex, dataListLayout.ToArray());
                        EditorGUILayout.EndHorizontal();
                        infoLayout();


                        

                        if (!isLoadIconFromPrefab)
                        {
                            if (GUILayout.Button("Generate Item"))
                            {
                                item_itemType item = CreateInstance<item_itemType>();


                                AssetDatabase.CreateAsset(item, targetFolderPath +'/'+ itemName + ".asset");

                                if (!AssetDatabase.IsValidFolder(targetFolderPath + "/CustomItemInfo")) AssetDatabase.CreateFolder(targetFolderPath, "CustomItemInfo");
                                
                                AssetDatabase.SaveAssets();
                                serializedObject = new SerializedObject(item);
                                serializedObject.FindProperty("itemName").stringValue = itemName;
                                serializedObject.FindProperty("displayName").stringValue = displayName;
                                serializedObject.FindProperty("stackLimit").intValue = stackLimit;
                                serializedObject.FindProperty("prefab").objectReferenceValue = prefab;
                                serializedObject.FindProperty("icon").objectReferenceValue = icon;
                                string temp = "";
                                for(int i = 0; i < groupIndex.Count; i++)
                                {
                                    temp += groupList[groupIndex[i]];
                                    if (i < groupIndex.Count - 1)
                                        temp += ',';
                                }
                                serializedObject.FindProperty("group").stringValue = temp;
                                serializedObject.ApplyModifiedProperties();

                                if (dataListIndex != 0)
                                {
                                    AssetDatabase.CreateAsset(customInfoObject, targetFolderPath + "/CustomItemInfo/" + itemName + "_customInfo.asset");
                                    serializedObject.FindProperty("infomation").objectReferenceValue = customInfoObject;
                                    serializedObject.ApplyModifiedProperties();
                                    serializedObject = new SerializedObject(customInfoObject);
                                    //serializedObject.FindProperty("relatedOn").objectReferenceValue = item;
                                    serializedObject.ApplyModifiedProperties();

                                    customInfoObject = new UnityEngine.Object();
                                }

                                customInfoObject = null;
                                itemName = null;
                                displayName = null;
                                stackLimit = 1;
                                prefab = null;
                                icon = null;
                            }
                        }
                        break;
                    }
                    
                case itemEditorPage.editItem:
                    {
                        targetItem = (item_itemType)EditorGUILayout.ObjectField("Select Item", targetItem, typeof(item_itemType), false);

                        if (targetItem != null)
                        {
                            string[] targetItemGroup = targetItem.group.Split(',');
                            groupIndex.Clear();
                            for (int i = 0; i < targetItemGroup.Length; i++)
                            {
                                for (int j = 0; j < groupList.Count; j++)
                                {
                                    if (groupList[j] == targetItemGroup[i])
                                    {
                                        groupIndex.Add(j);
                                        break;
                                    }
                                }
                            }

                            serializedObject = new SerializedObject(targetItem);
                            serializedObject.FindProperty("itemName").stringValue = EditorGUILayout.TextField("Item Name", targetItem.itemName);
                            serializedObject.FindProperty("displayName").stringValue = EditorGUILayout.TextField("Display Name", targetItem.displayName);
                            serializedObject.FindProperty("stackLimit").intValue = EditorGUILayout.IntField("Stack limit", targetItem.stackLimit);
                            groupLayout();
                            string temp = "";
                            for (int i = 0; i < groupIndex.Count; i++)
                            {
                                temp += groupList[groupIndex[i]];
                                if (i < groupIndex.Count - 1)
                                    temp += ',';
                            }
                            serializedObject.FindProperty("group").stringValue = temp;
                            serializedObject.FindProperty("prefab").objectReferenceValue = (GameObject)EditorGUILayout.ObjectField("GameObject (prefab)", targetItem.prefab, typeof(GameObject), false);
                            serializedObject.FindProperty("icon").objectReferenceValue = (Texture2D)EditorGUILayout.ObjectField("Item icon ", targetItem.icon, typeof(Texture2D), false);

                            if (targetItem.infomation != null)
                            {
                                SerializedObject customSerializedObject = new SerializedObject(targetItem.infomation);
                                for (int i = 0; i < dataList.Count; i++)
                                {
                                    if (targetItem.infomation.GetType().ToString() == dataList[i].name) dataListIndex = i + 1;
                                }
                                foreach (itemCustomInfoData.valueData i in dataList[dataListIndex - 1].valueDatas)
                                {
                                    EditorGUILayout.PropertyField(customSerializedObject.FindProperty(i.name));
                                }
                                customSerializedObject.ApplyModifiedProperties();
                                GUILayout.Space(10);
                            }
                            

                            serializedObject.ApplyModifiedProperties();
                        }
                        break;
                    }

                case itemEditorPage.editGroup:
                    {
                        EditorGUILayout.LabelField("Item Group");
                        stringListLayout(ref groupList, ref itemGroupScrollPosition,210);

                        // Save/Cancel edit & back to lastPage
                        EditorGUILayout.BeginHorizontal();
                        if (GUILayout.Button("Save"))
                        {
                            pageSelected = lastPage;
                            saveData();
                        }
                        if (GUILayout.Button("Cancel"))
                        {
                            groupList = file_System.LoadDataAsJson<itemEditorData>("Assets/Samples/NaiveUnity Pack/config/item group list.json").list;
                            pageSelected = lastPage;
                        }
                        EditorGUILayout.EndHorizontal();
                        break;
                    }
            }

            EditorGUILayout.EndScrollView();
        }
        private void groupLayout()
        {
            GUILayout.Space(10);
            groupScrollPosition = EditorGUILayout.BeginScrollView(groupScrollPosition, GUILayout.Width(position.width), GUILayout.Height(75));
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Group",GUILayout.Width(100));
            if (GUILayout.Button("edit"))
            {
                pageSelected = itemEditorPage.editGroup;
            }
            if (GUILayout.Button("add"))
            {
                groupIndex.Add(0);
            }
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < groupIndex.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("", GUILayout.Width(70));
                GUILayout.Label((i+1).ToString(), GUILayout.Width(15));
                groupIndex[i] = EditorGUILayout.Popup(groupIndex[i], groupList.ToArray());
                if (GUILayout.Button("Delete"))
                {
                    groupIndex.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
            GUILayout.Space(10);
            EditorGUILayout.EndScrollView();
        }
        private void saveData()
        {
            itemEditorData saveList = new itemEditorData
            {
                page = pageSelected,
                itemFolder = targetFolder,
                prefabFolder = iconFolder,
                list = groupList
            };
            file_System.SaveDataAsJson(saveList, "Assets/Samples/NaiveUnity Pack/config/", "item group list.json");
        }
        private void loadData()
        {
            itemEditorData itemEditorData = new itemEditorData();
            file_System.LoadDataAsJson("Assets/Samples/NaiveUnity Pack/config/item group list.json", itemEditorData);
            pageSelected = itemEditorData.page;
            groupList = itemEditorData.list;
            targetFolder = itemEditorData.itemFolder;
            iconFolder = itemEditorData.prefabFolder;


            saveList<itemCustomInfoData> saveList = new saveList<itemCustomInfoData>();
            file_System.LoadDataAsJson(dataPath.customInfoList, saveList);
            dataList = saveList.list;
        }
        public void infoLayout()
        {
            if (dataListIndex != 0)
            {
                if (customInfoObject == null) customInfoObject = CreateInstance(dataListLayout[dataListIndex]);
                if (dataListLayout[dataListIndex] != customInfoObject.GetType().ToString())
                {
                    customInfoObject = CreateInstance(dataListLayout[dataListIndex]);
                }
            }

            if (dataListLayout[dataListIndex] != "null")
            {
                SerializedObject serializedObject = new SerializedObject(customInfoObject);
                foreach (itemCustomInfoData.valueData i in dataList[dataListIndex-1].valueDatas)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(i.name));
                }
                serializedObject.ApplyModifiedProperties();
                GUILayout.Space(10);
            }
        }
        // Sprite to Texture
        public static Texture2D Sprite2Texture(Sprite sprite,int x,int y,int scale)
        {
            if (scale == 0 || scale < 0) scale = (int)(sprite.textureRect.width < sprite.textureRect.height ? sprite.textureRect.width : sprite.textureRect.height);
            Texture2D newText = new Texture2D(scale, scale);
            Color[] newColors = sprite.texture.GetPixels(x, y, scale, scale);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }

        public void reflushDataListLayout()
        {
            dataListLayout.Clear();
            dataListLayout.Add("null");
            foreach (itemCustomInfoData i in dataList)
            {
                dataListLayout.Add(i.name);
            }
        }
    }
}
