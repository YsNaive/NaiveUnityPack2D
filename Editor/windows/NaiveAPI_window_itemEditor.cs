using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NaiveAPI_window_itemEditor : EditorWindow
{
    private DefaultAsset targetFolder = null;
    private string targetFolderPath;
    private bool isEditItem = false,isLoadIconFromPrefab = false;
    private NaiveAPI_item_itemType targetItem;
    // for image setting
    private int x = 0, y = 0, h = 0, w = 0;
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
                h = 0;
                w = 0;
            }
            if (isLoadIconFromPrefab)
            {
                EditorGUILayout.EndHorizontal();
                x = EditorGUILayout.IntField("x",x);
                y = EditorGUILayout.IntField("y", y);
                h = EditorGUILayout.IntField("h", h);
                w = EditorGUILayout.IntField("w", w);
                try { icon = Sprite2Texture(prefab.GetComponent<SpriteRenderer>().sprite, x, y, h, w); } catch { Debug.Log("There is no Prefab or Texture is not Readable !"); }
                EditorGUILayout.BeginHorizontal();
            }
            EditorGUILayout.EndHorizontal();
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
        }
            
    }


    // Sprite to Texture
    public static Texture2D Sprite2Texture(Sprite sprite,int x,int y,int h,int w)
    {
        if (x == 0) x = (int)sprite.textureRect.x;
        if (y == 0) x = (int)sprite.textureRect.y;
        if (w == 0) w = (int)sprite.textureRect.width;
        if (h == 0) h = (int)sprite.textureRect.height;
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D(w, h);
            Color[] newColors = sprite.texture.GetPixels(x, y, w, h);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }
}
