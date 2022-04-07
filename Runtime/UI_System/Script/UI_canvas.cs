using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace NaiveAPI
{
    public enum UI_objectType
    {
        objectButton,
        customButton,
        gridSlot,
        image
    }

    public class UI_canvas : NaiveAPI2D
    {
        [HideInInspector]
        public UI_objectType buttonType = UI_objectType.objectButton;
        [HideInInspector]
        public string objectName ;
        [HideInInspector]
        public bool isCloseClickOutside;

        public override void LocalAwake()
        {
            throw new System.NotImplementedException();
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void addObject(GameObject InsObject) 
        { 
            GameObject i = Instantiate(InsObject, transform);
            i.name = objectName;
            i.GetComponent<UI_state>().ignoreClear = true;
            if (isCloseClickOutside) i.AddComponent<UI_closeIfClickOutside>();

            objectName = null;
        }
    }

}
