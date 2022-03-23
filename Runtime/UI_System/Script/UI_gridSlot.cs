using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NaiveAPI
{
    public class UI_gridSlot : MonoBehaviour
    {
        [HideInInspector]
        public GameObject slotBased;
        public Sprite icon;

        [HideInInspector]
        public bool isShowByInventory = false, isGenerateByItemList = false, isGenerateByIcon = false;
        [HideInInspector]
        public GameObject displayByItemSystem;
        [HideInInspector]
        public item_inventory displayInventory;
        [HideInInspector]
        public item_itemList displayItemList;

        private void Awake()
        {
            displayInventory = displayByItemSystem.gameObject.GetComponent<item_inventory>();
        }
        // Start is called before the first frame update
        void Start()
        {
            
            slotBased.SetActive(false);
            if (isShowByInventory)
                reflushByInventory();
            if (isGenerateByItemList)
                reflushByItemList();
            
            if(displayInventory == null) print("Can not find item_inventory on [ " + displayByItemSystem + " ]\nPlease check your setting !"); 
        }

        // Update is called once per frame
        void Update()
        {
            if (isShowByInventory)
            {
                if (displayInventory.isUIupdate)
                {
                    reflushByInventory();
                }
            }

        }

        public void addSlot(string name,Sprite icon,string text,bool showNull)
        {
            GameObject slot = Instantiate(slotBased, transform);
            slot.name = name;
            slot.transform.GetChild(1).GetComponent<Image>().sprite = icon;
            slot.transform.GetChild(2).GetComponent<Text>().text = text;

            if (!showNull)
            {
                if (slot.transform.GetChild(0).GetComponent<Image>().sprite == null)
                    slot.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
                if (slot.transform.GetChild(1).GetComponent<Image>().sprite == null)
                    slot.transform.GetChild(1).GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }

            slot.SetActive(gameObject.activeSelf);
        }
        public void clearAll()
        {
            for (int i = 0; i < transform.childCount;)
                if (transform.GetChild(i).name != "slotBased") DestroyImmediate(transform.GetChild(i).gameObject);
                else i++;
        }
        public void clearPreview()
        {
            for (int i = 0; i < transform.childCount;)
            {
                if (transform.GetChild(i).name == "previewSlot")
                    DestroyImmediate(transform.GetChild(i).gameObject);
                else
                    i++;
            }
        }
        public void reflushByInventory()
        {
            clearAll();
            for (int i = 0; i < displayInventory.slots.Count; i++)
            {
                if (displayInventory.slots[i].item != null)
                {
                    item_itemType item = displayInventory.slots[i].item;
                    if (isGenerateByIcon)
                        addSlot(item.itemName, Sprite.Create(item.icon, new Rect(0, 0, item.icon.width, item.icon.height), Vector2.zero),  item.displayName,  false);
                    else
                        addSlot(item.itemName, item.prefab.GetComponent<SpriteRenderer>().sprite, item.displayName,  false);
                }
            }
            displayInventory.isUIupdate = false;
        }
        public void reflushByItemList()
        {
            clearAll();
            for (int i = 0; i < displayItemList.itemList.Count; i++)
            {
                item_itemType item = displayItemList.itemList[i];
                if (isGenerateByIcon)
                    addSlot(item.itemName, Sprite.Create(item.icon, new Rect(0, 0, item.icon.width, item.icon.height), Vector2.zero),  item.displayName,  false);
                else
                    addSlot(item.itemName, item.prefab.GetComponent<SpriteRenderer>().sprite,  item.displayName, false);
            }
        }
    }

}
