using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NaiveAPI_window_itemEditor : EditorWindow
{
    private DefaultAsset targetFolder = null,iconFolder=null;
    private string targetFolderPath;
    private bool isEditItem = false,isLoadIconFromPrefab = false;
    private SerializedObject serializedObject;
    private NaiveAPI_item_itemType targetItem;
    private int stackLimit = 1;
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
        GetWindow<NaiveAPI_window_itemEditor>(true, "Item Editor", true);

    }

    private void OnGUI()
    {

        if (!isEditItem)
        {
            if (GUILayout.Button("Create Mode"))
            {
                isEditItem = true;
            }
        }

        else
        {
            if (GUILayout.Button("Edit Mode"))
            {
                isEditItem = false;
            }
        }


        if ( ! isEditItem)
        {
            targetItem = null;
            targetFolder = (DefaultAsset)EditorGUILayout.ObjectField("Generate locate", targetFolder, typeof(DefaultAsset), false);
            targetFolderPath = AssetDatabase.GetAssetPath(targetFolder) + "/";

            
            itemName = EditorGUILayout.TextField("Item Name",itemName);
            displayName = EditorGUILayout.TextField("Display Name", displayName);
            stackLimit = EditorGUILayout.IntField("Stack limit", stackLimit);
            prefab = (GameObject)EditorGUILayout.ObjectField("GameObject (prefab)", prefab, typeof(GameObject), false);

            

            if(!isLoadIconFromPrefab)
            {
                EditorGUILayout.BeginHorizontal();
                icon = (Texture2D)EditorGUILayout.ObjectField("Item icon ", icon, typeof(Texture2D), false);
                if (GUILayout.Button("Load form\nPrefab"))
                {
                    isLoadIconFromPrefab = ! isLoadIconFromPrefab;
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
                int minWH = (int)(tempSprite.textureRect.width > tempSprite.textureRect.height ? tempSprite.textureRect.width : tempSprite.textureRect.height);
                if (scale == 0 || scale < 0) scale = 0;
                if (x > tempSprite.textureRect.width*2) x = (int)tempSprite.textureRect.width*2;
                if (y > tempSprite.textureRect.height) y = (int)tempSprite.textureRect.height;
                if (scale > minWH) scale = minWH;
                iconFromPrefab = (Texture2D)EditorGUILayout.ObjectField("icon preview", iconFromPrefab, typeof(Texture2D), false);
                try { iconFromPrefab = Sprite2Texture(tempSprite, x, y, scale); } catch { Debug.Log("There is no Prefab or Texture is not Readable !"); }
                
                if (GUILayout.Button("Cancel"))
                {
                    isLoadIconFromPrefab = false;
                }
                EditorGUILayout.EndHorizontal();
                x = EditorGUILayout.IntField("x",x);
                y = EditorGUILayout.IntField("y", y);
                scale = EditorGUILayout.IntField("scale", scale);
                iconFolder = (DefaultAsset)EditorGUILayout.ObjectField("Save locate", iconFolder, typeof(DefaultAsset), false);
                
                if (GUILayout.Button("Save and load icon"))
                {
                    string path = AssetDatabase.GetAssetPath(iconFolder) + "/" + itemName + "_icon.asset";
                    AssetDatabase.CreateAsset(iconFromPrefab,path);
                    icon = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
                    isLoadIconFromPrefab = false;
                }
            }

            EditorGUILayout.Space(20);
            if(!isLoadIconFromPrefab)
            {
                if (GUILayout.Button("Generate Item"))
                {
                    NaiveAPI_item_itemType item = CreateInstance<NaiveAPI_item_itemType>();

                    AssetDatabase.CreateAsset(item, targetFolderPath + itemName + ".asset");
                    AssetDatabase.SaveAssets();
                    serializedObject = new SerializedObject(item);
                    serializedObject.FindProperty("itemName").stringValue = itemName;
                    serializedObject.FindProperty("displayName").stringValue = displayName;
                    serializedObject.FindProperty("stackLimit").intValue = stackLimit;
                    serializedObject.FindProperty("prefab").objectReferenceValue = prefab;
                    serializedObject.FindProperty("icon").objectReferenceValue = icon;
                    serializedObject.ApplyModifiedProperties();

                    itemName = null;
                    displayName = null;
                    stackLimit = 1;
                    prefab = null;
                    icon = null;
                    
                }
            }

        }
        else
        {
            targetItem = (NaiveAPI_item_itemType)EditorGUILayout.ObjectField("Select Item", targetItem, typeof(NaiveAPI_item_itemType), false);
            if (targetItem != null)
            {
                serializedObject = new SerializedObject(targetItem);
                serializedObject.FindProperty("itemName").stringValue = EditorGUILayout.TextField("Item Name", targetItem.itemName);
                serializedObject.FindProperty("displayName").stringValue = EditorGUILayout.TextField("Display Name", targetItem.displayName);
                serializedObject.FindProperty("stackLimit").intValue = EditorGUILayout.IntField("Stack limit", targetItem.stackLimit);
                serializedObject.FindProperty("prefab").objectReferenceValue = (GameObject)EditorGUILayout.ObjectField("GameObject (prefab)", targetItem.prefab, typeof(GameObject), false);
                serializedObject.FindProperty("icon").objectReferenceValue = (Texture2D)EditorGUILayout.ObjectField("Item icon ", targetItem.icon, typeof(Texture2D), false);
                serializedObject.ApplyModifiedProperties();
            }
            
        }
    }


    // Sprite to Texture
    public static Texture2D Sprite2Texture(Sprite sprite,int x,int y,int scale)
    {
        if (scale == 0 || scale < 0) scale = (int)(sprite.textureRect.width > sprite.textureRect.height ? sprite.textureRect.width : sprite.textureRect.height);
        Texture2D newText = new Texture2D(scale, scale);
        Color[] newColors = sprite.texture.GetPixels(x, y, scale, scale);
        newText.SetPixels(newColors);
        newText.Apply();
        return newText;
    }
}
