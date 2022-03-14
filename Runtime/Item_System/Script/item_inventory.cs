using System.Collections.Generic;
using UnityEngine;


namespace NaiveAPI
{
    [CreateAssetMenu(menuName = "NaiveAPI/Item System/Inventory")]
    [System.Serializable]
    public class item_inventory : ScriptableObject
    {
        public int slotLimit = 0;
        public bool isUIupdate = false;
        public List<item_slot> slots = new List<item_slot>();
    }
    [System.Serializable]
    public class item_slot
    {
        public item_slot()
        {
            item = null;
            stack = 0;
        }
        public item_itemType item;
        public int stack = 1;
}

}
