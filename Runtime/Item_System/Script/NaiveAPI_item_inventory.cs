using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NaiveAPI/Item System/Inventory")]
[System.Serializable]
public class NaiveAPI_item_inventory : ScriptableObject
{
    public int slotLimit = 0;
    public bool isUIupdate = false;
    public List<slot> slots = new List<slot>();
}
[System.Serializable]
public class slot
{
    public slot()
    {
        item = null;
        stack = 0;
    }
    public NaiveAPI_item_itemType item;
    public int stack = 1;
}
