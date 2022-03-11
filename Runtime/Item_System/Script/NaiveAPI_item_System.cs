using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaiveAPI_item_System : MonoBehaviour
{
    public NaiveAPI_item_inventory inventory;

    public bool push(NaiveAPI_item_itemType item)
    {
        slot emptySlot = null;
        for(int i = 0; i < inventory.slotLimit; i++)
        {
            slot slot = inventory.slots[i];
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

    public bool pull(NaiveAPI_item_itemType item)
    {
        for (int i = 0; i < inventory.slotLimit; i++)
        {
            slot slot = inventory.slots[i];
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

    public bool putAt(NaiveAPI_item_itemType item,int number)
    {
        slot slot = inventory.slots[number];
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
    public NaiveAPI_item_itemType takeAt(int number)
    {
        NaiveAPI_item_itemType output = null;
        slot slot = inventory.slots[number];
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
}
