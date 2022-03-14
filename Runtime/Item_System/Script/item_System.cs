using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    public class item_System : MonoBehaviour
    {
        public item_inventory inventory;

        // 清除背包所有物品
        public void clear()
        {
            for (int i = 0; i < inventory.slots.Count; i++)
            {
                inventory.slots[i] = null;
            }
        }

        // 清除指定欄位
        public void clearAt(int number)
        {
            inventory.slots[number] = null;
        }
        public int ifItemHolding(item_itemType searchItem)
        {
            int output = 0;
            for (int i = 0; i < inventory.slots.Count; i++)
            {
                if (searchItem == inventory.slots[i].item) output += inventory.slots[i].stack;
            }
            return output;
        }
        public bool push(item_itemType item)
        {
            item_slot emptySlot = null;
            for(int i = 0; i < inventory.slotLimit; i++)
            {
                item_slot slot = inventory.slots[i];
                if (slot.item == item && slot.stack < item.stackLimit)
                {
                    slot.stack++;
                    inventory.isUIupdate = true;
                    return true;
                }
                else if (emptySlot == null && slot.item == null)
                    emptySlot = slot;
            }
            if (emptySlot != null)
            {
                emptySlot.item = item;
                emptySlot.stack++;
                inventory.isUIupdate = true;
                return true;
            }
            else
                return false;
        }

        public bool pull(item_itemType item)
        {
            for (int i = 0; i < inventory.slotLimit; i++)
            {
                item_slot slot = inventory.slots[i];
                if(slot.item == item && slot.stack > 0)
                {
                    slot.stack--;
                    if (slot.stack <= 0)
                    {
                        slot.item = null;
                        slot.stack = 0;
                    }
                    inventory.isUIupdate = true;
                    return true;
                }
            }
            return false;
        }

        public bool pushAt(item_itemType item,int number)
        {
            item_slot slot = inventory.slots[number];
            if (slot.item == item && slot.stack < item.stackLimit)
            {
                slot.stack++;
                inventory.isUIupdate = true;
                return true;
            }
            else if (slot == null)
            {
                slot.item = item;
                slot.stack = 1;
                inventory.isUIupdate = true;
                return true;
            }
            else return false;
        }
        public item_itemType pullAt(int number)
        {
            item_itemType output = null;
            item_slot slot = inventory.slots[number];
            if (slot.item != null)
            {
                output = slot.item;
                slot.stack--;
                if (slot.stack <= 0)
                {
                    slot.item = null;
                    slot.stack = 0;
                }
                inventory.isUIupdate = true;
            }
            return output;
        }

        public List<item_slot> pullAll()
        {
            List<item_slot> output = new List<item_slot>();
            for(int i = 0; i < inventory.slots.Count; i++)
            {
                output.Add(inventory.slots[i]);
            }
            clear();
            return output;
        }
        public item_slot pullAllat(int number)
        {
            item_slot output;
            output=inventory.slots[number];
            clearAt(number);
            return output;
        }
    }

}
