using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gridSlotCanvas : MonoBehaviour
{
    [HideInInspector]
    public GameObject slotPrefab;
    public Sprite icon, backGround;
    public float itemH=0.5f, itemW=0.5f;

    [HideInInspector]
    public bool isShowByItemList = false,isGenerateByIcon = false;
    [HideInInspector]
    public NaiveAPI_item_itemList displayList = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void addSlot(string name,Sprite icon,Sprite backGround ,float x ,float y ,bool showNull)
    {
        GameObject slot = Instantiate(slotPrefab, transform);
        slot.name = name;
        slot.GetComponent<RectTransform>().localScale = new Vector2(x, y);
        slot.transform.GetChild(0).GetComponent<Image>().sprite = backGround;
        slot.transform.GetChild(1).GetComponent<Image>().sprite = icon;

        if (!showNull)
        {
            if (slot.transform.GetChild(0).GetComponent<Image>().sprite == null)
                slot.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
            if (slot.transform.GetChild(1).GetComponent<Image>().sprite == null)
                slot.transform.GetChild(1).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }

    }
    public void clearAll()
    {
        for (int i = 0; i < transform.childCount;)
            DestroyImmediate(transform.GetChild(i).gameObject);
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
    public void reflushByItemList()
    {
        clearAll();
        for (int i = 0; i < displayList.itemList.Count; i++)
        {
            NaiveAPI_item_itemType item = displayList.itemList[i];
            if(isGenerateByIcon)
                addSlot(item.itemName, Sprite.Create(item.icon, new Rect(0, 0, item.icon.width, item.icon.height), Vector2.zero), backGround, 0.5f, 0.5f, false);
            else
                addSlot(item.itemName, item.prefab.GetComponent<SpriteRenderer>().sprite, backGround, 0.5f, 0.5f,false);
        }
    }

}
