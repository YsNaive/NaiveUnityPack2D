using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NaiveAPI/Item System/ItemList")]
[System.Serializable]
public class NaiveAPI_item_itemList : ScriptableObject
{
    [SerializeField]
    public List<NaiveAPI_item_itemType> itemList = new List<NaiveAPI_item_itemType>();
}
