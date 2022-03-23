using System.Collections.Generic;
using UnityEngine;


namespace NaiveAPI
{ 
    [System.Serializable]
    public class item_inventory : MonoBehaviour
    {
        public int slotLimit = 0;
        public bool isUIupdate = false;
        public List<item_slot> slots = new List<item_slot>();


        /*----------------------------- 背包操作函式 -----------------------------*/
        // 清除背包所有物品
        public void clear()
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i] = new item_slot();
            }
        }

        // 清除指定欄位
        public void clearAt(int number)
        {
            slots[number] = new item_slot();
        }
        public int ifItemHolding(item_itemType searchItem)
        {
            int output = 0;
            for (int i = 0; i < slots.Count; i++)
            {
                if (searchItem == slots[i].item) output += slots[i].stack;
            }
            return output;
        }
        public bool push(item_itemType item)
        {
            item_slot emptySlot = null;
            for (int i = 0; i < slotLimit; i++)
            {
                item_slot slot = slots[i];
                if (slot.item == item && slot.stack < item.stackLimit)
                {
                    slot.stack++;
                    isUIupdate = true;
                    return true;
                }
                else if (emptySlot == null && slot.item == null)
                    emptySlot = slot;
            }
            if (emptySlot != null)
            {
                emptySlot.item = item;
                emptySlot.stack++;
                isUIupdate = true;
                return true;
            }
            else
                return false;
        }

        public bool pull(item_itemType item)
        {
            for (int i = 0; i < slotLimit; i++)
            {
                item_slot slot = slots[i];
                if (slot.item == item && slot.stack > 0)
                {
                    slot.stack--;
                    if (slot.stack <= 0)
                    {
                        slot = new item_slot();
                    }
                    isUIupdate = true;
                    return true;
                }
            }
            return false;
        }

        public bool pushAt(item_itemType item, int number)
        {
            item_slot slot = slots[number];
            if (slot.item == item && slot.stack < item.stackLimit)
            {
                slot.stack++;
                isUIupdate = true;
                return true;
            }
            else if (slot == null)
            {
                slot.item = item;
                slot.stack = 1;
                isUIupdate = true;
                return true;
            }
            else return false;
        }
        public item_itemType pullAt(int number)
        {
            item_itemType output = null;
            item_slot slot = slots[number];
            if (slot.item != null)
            {
                output = slot.item;
                slot.stack--;
                if (slot.stack <= 0)
                {
                    slot = new item_slot();
                }
                isUIupdate = true;
            }
            return output;
        }

        public List<item_slot> pullAll()
        {
            List<item_slot> output = new List<item_slot>();
            for (int i = 0; i < slots.Count; i++)
            {
                output.Add(slots[i]);
            }
            clear();
            return output;
        }
        public item_slot pullAllat(int number)
        {
            item_slot output;
            output = slots[number];
            clearAt(number);
            return output;
        }
        /*------------------------------------------------------------------------*/
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


