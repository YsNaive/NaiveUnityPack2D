using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NaiveAPI_window_itemEditor : EditorWindow
{
    private DefaultAsset targetFolder = null,iconFolder=null;
    private string targetFolderPath;
    private bool isEditItem = false,isLoadIconFromPrefab = false;
    private NaiveAPI_item_itemType targetItem;
    // for image setting
    private int x = 0, y = 0, scale = 0;
    // itemType
    private Texture2D icon;
    private string itemName;
    private string displayName;
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
            prefab = (GameObject)EditorGUILayout.ObjectField("GameObject (prefab)", prefab, typeof(GameObject), false);

            EditorGUILayout.BeginHorizontal();
            icon = (Texture2D)EditorGUILayout.ObjectField("Item icon ", icon, typeof(Texture2D), false);
            if (GUILayout.Button("Load form\nPrefab"))
            {
                isLoadIconFromPrefab = ! isLoadIconFromPrefab;
                x = 0;
                y = 0;
                scale = 0;
            }
            if (isLoadIconFromPrefab)
            {
                EditorGUILayout.EndHorizontal();
                x = EditorGUILayout.IntField("x",x);
                y = EditorGUILayout.IntField("y", y);
                scale = EditorGUILayout.IntField("scale", scale);

                // ¹LÂo¹H³W¿é¤J
                Sprite tempSprite = prefab.GetComponent<SpriteRenderer>().sprite;
                if (x == 0 || x < 0) x = 0;
                if (y == 0 || y < 0) y = 0;
                int minWH = (int)(tempSprite.textureRect.width > tempSprite.textureRect.height ? tempSprite.textureRect.width : tempSprite.textureRect.height);
                if (scale == 0 || scale < 0) scale = 0;
                if (x > tempSprite.textureRect.width) x = (int)tempSprite.textureRect.width;
                if (y > tempSprite.textureRect.height) y = (int)tempSprite.textureRect.height;
                if (scale > minWH) scale = minWH;

                try { icon = Sprite2Texture(tempSprite, x, y, scale); } catch { Debug.Log("There is no Prefab or Texture is not Readable !"); }
                iconFolder = (DefaultAsset)EditorGUILayout.ObjectField("Save locate", iconFolder, typeof(DefaultAsset), false);
                if (GUILayout.Button("Save and load icon"))
                {
                    string path = AssetDatabase.GetAssetPath(iconFolder) + "/" + itemName + "_icon.asset";
                    AssetDatabase.CreateAsset(icon,path);
                    icon = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
                    isLoadIconFromPrefab = false;
                }
                EditorGUILayout.BeginHorizontal();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20);
            if (GUILayout.Button("Generate Item"))
            {
                NaiveAPI_item_itemType item = CreateInstance<NaiveAPI_item_itemType>();
                AssetDatabase.CreateAsset(item, targetFolderPath + itemName + ".asset");
                item.itemName = itemName;
                item.displayName = displayName;
                item.prefab = prefab;
                item.icon = icon;

                AssetDatabase.SaveAssets();

                itemName = null;
                displayName = null;
                prefab = null;
                icon = null;
            }
        }
        else
        {
            targetItem = (NaiveAPI_item_itemType)EditorGUILayout.ObjectField("Select Item", targetItem, typeof(NaiveAPI_item_itemType), false);
            if (targetItem != null)
            {
                targetItem.itemName = EditorGUILayout.TextField("Item Name", targetItem.itemName);
                targetItem.displayName = EditorGUILayout.TextField("Display Name", targetItem.displayName);
                targetItem.prefab = (GameObject)EditorGUILayout.ObjectField("GameObject (prefab)", targetItem.prefab, typeof(GameObject), false);
                targetItem.icon = (Texture2D)EditorGUILayout.ObjectField("Item icon ", targetItem.icon, typeof(Texture2D), false);
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
