using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace NaiveAPI
{
    public class window_itemEditor : EditorWindow
    {
        private enum page
        {
            create,
            editItem,
            editGroup
        }
        private class itemEditorData
        {
            public page page;
            public List<string> list;
        }

        private page pageSelected = page.create;
        private page lastPage = page.create;
        private DefaultAsset targetFolder = null,iconFolder=null;
        private string targetFolderPath;
        private bool isLoadIconFromPrefab = false;
        private SerializedObject serializedObject;
        private item_itemType targetItem;
        private int stackLimit = 1;
        private List<int> groupIndex = new List<int>();
        private List<string> groupList = new List<string>();
        private Vector2 itemGroupScrollPosition = new Vector2(0,0);
        
        // for image setting
        private int x = 0, y = 0, scale = 0;
        // itemType
        [HideInInspector]
        private Texture2D icon,iconFromPrefab;
        [HideInInspector]
        private string itemName,displayName;
        [HideInInspector]
        private GameObject prefab;

        [MenuItem("Window/NaiveAPI/Item Editor")]
        public static void ShowObjectWindow()
        {
            GetWindow<window_itemEditor>(true, "Item Editor", true);
        }

        private void OnEnable()
        {
            if (AssetDatabase.FindAssets("Assets/Samples/NaiveUnity Pack/config/item group list.json").Length == 0)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Samples/NaiveUnity Pack/config"))
                {
                    if (!AssetDatabase.IsValidFolder("Assets/Samples/NaiveUnity Pack"))
                    {
                        if (!AssetDatabase.IsValidFolder("Assets/Samples"))
                            AssetDatabase.CreateFolder("Assets", "Samples");

                        AssetDatabase.CreateFolder("Assets/Samples", "NaiveUnity Pack");
                    }
                    AssetDatabase.CreateFolder("Assets/Samples/NaiveUnity Pack", "config");
                }
                groupList.Add("none");
                saveData();
            }
            else
                loadData();
            groupIndex.Add(0);
        }

        private void OnGUI()
        {
            if (pageSelected == page.create)
            {
                if (GUILayout.Button("Create Mode"))
                {
                    pageSelected = page.editItem;
                    lastPage = page.editItem;
                    saveData();
                }
            }
            else if(pageSelected == page.editItem)
            {
                if (GUILayout.Button("Edit Mode"))
                {
                    pageSelected = page.create;
                    lastPage = page.create;
                    groupIndex.Clear();
                    groupIndex.Add(0);
                    saveData();
                }
            }

            switch (pageSelected)
            {
                case page.create:
                    {
                        targetItem = null;
                        targetFolder = (DefaultAsset)EditorGUILayout.ObjectField("Generate locate", targetFolder, typeof(DefaultAsset), false);
                        targetFolderPath = AssetDatabase.GetAssetPath(targetFolder) + "/";


                        itemName = EditorGUILayout.TextField("Item Name", itemName);
                        displayName = EditorGUILayout.TextField("Display Name", displayName);
                        stackLimit = EditorGUILayout.IntField("Stack limit", stackLimit);
                        prefab = (GameObject)EditorGUILayout.ObjectField("GameObject (prefab)", prefab, typeof(GameObject), false);

                        groupLayout();

                        if (!isLoadIconFromPrefab)
                        {
                            EditorGUILayout.BeginHorizontal();
                            icon = (Texture2D)EditorGUILayout.ObjectField("Item icon ", icon, typeof(Texture2D), false);
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

                        EditorGUILayout.Space(20);
                        if (!isLoadIconFromPrefab)
                        {
                            if (GUILayout.Button("Generate Item"))
                            {
                                item_itemType item = CreateInstance<item_itemType>();

                                AssetDatabase.CreateAsset(item, targetFolderPath + itemName + ".asset");
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

                                itemName = null;
                                displayName = null;
                                stackLimit = 1;
                                prefab = null;
                                icon = null;

                            }
                        }
                        break;
                    }
                    
                case page.editItem:
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
                            serializedObject.ApplyModifiedProperties();
                        }
                        break;
                    }

                case page.editGroup:
                    {
                        EditorGUILayout.LabelField("Item Group");
                        itemGroupScrollPosition = EditorGUILayout.BeginScrollView(itemGroupScrollPosition, GUILayout.Width(320), GUILayout.Height(300));
                        for (int i = 0; i < groupList.Count; i++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField(' '+(i + 1).ToString(), GUILayout.Width(20));
                            groupList[i] = EditorGUILayout.TextField(groupList[i]);
                            if (GUILayout.Button("Delete"))
                            {
                                groupList.RemoveAt(i);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndScrollView();
                        GUILayout.Space(10);
                        if (GUILayout.Button("Add new Group"))
                        {
                            groupList.Add("");
                        }

                        GUILayout.Space(20);

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
        }
        private void groupLayout()
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Group",GUILayout.Width(100));
            if (GUILayout.Button("edit"))
            {
                pageSelected = page.editGroup;
            }
            if (GUILayout.Button("add"))
            {
                groupIndex.Add(0);
            }
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < groupIndex.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("", GUILayout.Width(100));
                groupIndex[i] = EditorGUILayout.Popup(groupIndex[i], groupList.ToArray());
                if (GUILayout.Button("Delete"))
                {
                    groupIndex.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
            GUILayout.Space(10);
        }

        private void saveData()
        {
            itemEditorData saveList = new itemEditorData
            {
                page = pageSelected,
                list = groupList
            };
            file_System.SaveDataAsJson(saveList, "Assets/Samples/NaiveUnity Pack/config/", "item group list.json");
        }
        private void loadData()
        {
            itemEditorData itemEditorData = new itemEditorData();
            file_System.LoadDataAsJson<itemEditorData>("Assets/Samples/NaiveUnity Pack/config/item group list.json", itemEditorData);
            pageSelected = itemEditorData.page;
            groupList = itemEditorData.list;
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
    }

}
