using NaiveAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NaiveAPI
{
    public class UI_System : projectAPI2D
    {
        [HideInInspector]
        public string canvasName;
        [HideInInspector]
        public bool isCloseClickOutside;

        public List<switchByKeyCode> switchByKeyCode = new List<switchByKeyCode>();
        public List<holdByKeyCode> holdByKeyCode = new List<holdByKeyCode>();
        public override void localAwake()
        {
            throw new System.NotImplementedException();
        }

        public List<UI_infomation> UIS = new List<UI_infomation>();
    
        private void Start()
        {
            loadUIstructure();
        }

        private void Update()
        {
            for(int i = 0; i < switchByKeyCode.Count; i++)
            {
                if (Input.GetKeyDown(switchByKeyCode[i].keyCode))
                {
                    setActive(switchByKeyCode[i].targetObject, UIS[searchStructure(switchByKeyCode[i].targetObject)].isActive);
                }
            }
            for (int i = 0; i < holdByKeyCode.Count; i++)
            {
                setActive(holdByKeyCode[i].targetObject , Input.GetKey(holdByKeyCode[i].keyCode));
            }

            reflush();
        }

        public void reflush()
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
            Transform[] allChild = GetComponentsInChildren<Transform>(true);
            int j = 0;
            for (int i=0; i < allChild.Length; i++)
            {
                if(allChild[i].GetComponent<UI_state>() != null)
                {
                    UIS.Add(new UI_infomation());
                    UIS[j].thisUI_state = allChild[i].GetComponent<UI_state>();
                    UIS[j].displayName = allChild[i].name;
                    UIS[j].isActive = UIS[j].thisUI_state.isActive;
                    UIS[j].ignoreClear = UIS[j].thisUI_state.ignoreClear;
                    UIS[j].thisUI = allChild[i].gameObject;
                    j++;
                }
            }
        }
        public int searchStructure(GameObject searchObject)
        {
            for(int i = 0; i < UIS.Count; i++)
            {
                if (searchObject == UIS[i].thisUI)
                    return i;
            }
            return -1;
        }

        public void addCanvas(GameObject canvas)
        {
            GameObject i = Instantiate(canvas, transform);
            if (isCloseClickOutside) i.AddComponent<UI_closeIfClickOutside>();
            i.name = canvasName;
            canvasName = null;
            isCloseClickOutside = false;
        }

        public void setActive(GameObject targetObject,bool isActive)
        {
            try { UIS[searchStructure(targetObject)].isActive = isActive;}
            catch { print("Target : " + targetObject + " Not Found"); }
        }
    }

    [System.Serializable]
    public class UI_infomation
    {
        [HideInInspector]
        public string displayName;
        public GameObject thisUI;
        [HideInInspector]
        public UI_state thisUI_state;
       
        public bool isActive, ignoreClear;
    }
    [System.Serializable]
    public class switchByKeyCode
    {
        public string keyCode;
        public GameObject targetObject; 
    }
    [System.Serializable]
    public class holdByKeyCode
    {
        public string keyCode;
        public GameObject targetObject;
    }
}
