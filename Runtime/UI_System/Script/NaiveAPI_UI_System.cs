using NaiveAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NaiveAPI_UI_System : projectAPI2D
{
    [HideInInspector]
    public string canvasName;
    [HideInInspector]
    public bool isCloseClickOutside;


    public override void localAwake()
    {
        throw new System.NotImplementedException();
    }

    public List<UI_infomation> UIS = new List<UI_infomation>();
    
    private void Start()
    {
        loadUIstructure();
        displayReflush();
    }

    private void Update()
    {
        
    }

    public void displayReflush()
    {
        for(int i = 0; i < UIS.Count; i++)
        {
            UIS[i].thisUI.SetActive(UIS[i].isActive);
        }
    }
    public void clearAll()
    {
        for (int i = 0; i < UIS.Count; i++)
        {
            if(! UIS[i].ignoreClear)
                UIS[i].isActive = false;
        }
    }

    public void loadUIstructure()
    {
        UIS.Clear();
        Transform[] allChild = this.GetComponentsInChildren<Transform>(true);
        int j = 0;
        for (int i=0; i < allChild.Length; i++)
        {
            if(allChild[i].GetComponent<UI_state>() != null)
            {
                UIS.Add(new UI_infomation());
                UIS[j].thisUI_state = allChild[i].GetComponent<UI_state>();
                UIS[j].isActive = UIS[j].thisUI_state.isActive;
                UIS[j].ignoreClear = UIS[j].thisUI_state.ignoreClear;
                UIS[j].thisUI = allChild[i].gameObject;
                j++;
            }
        }
    }
    public int searchStructure(string searchName)
    {
        for(int i = 0; i < UIS.Count; i++)
        {
            if (searchName == UIS[i].thisUI.name)
                return i;
        }
        return -1;
    }

    public void addCanvas(GameObject canvas)
    {
        GameObject i = Instantiate(canvas, transform);
        if (isCloseClickOutside) i.AddComponent<NaiveAPI.closeIfClickOutside>();
        i.name = canvasName;
        canvasName = null;
    }

    public void setActive(string targetName,bool isActive)
    {
        try { UIS[searchStructure(targetName)].isActive = isActive;}
        catch {  }
        
    }
}

namespace NaiveAPI
{
    [System.Serializable]
    public class UI_infomation
    {
        public GameObject thisUI;
        [HideInInspector]
        public UI_state thisUI_state;
        public bool isActive, ignoreClear;
    }
}