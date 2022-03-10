using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "NaiveAPI/Item System/Item")]
public class NaiveAPI_item_itemType : ScriptableObject
{
    public Texture2D icon;
    public string itemName;
    public string displayName;
    public Sprite image;
    public GameObject prefab;
}

