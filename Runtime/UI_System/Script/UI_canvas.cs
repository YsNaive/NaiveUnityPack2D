using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace NaiveAPI
{
    public enum buttonType
    {
        canvasButton,
        customButton,
        image
    }

    public class UI_canvas : projectAPI2D
    {
        [HideInInspector]
        public buttonType buttonType = buttonType.canvasButton;
        [HideInInspector]
        public string objectName ;
        [HideInInspector]
        public bool isCloseClickOutside;

        // for cavanButton
        [HideInInspector]
        public GameObject targetObject;
        [HideInInspector]
        public bool isCloseSelfCanvas, isOpenOtherObject;

        // for image
        [HideInInspector]
        public Sprite image;
        public override void localAwake()
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
            if (isCloseClickOutside) i.AddComponent<UI_closeIfClickOutside>();
            switch (buttonType)
            {
                case buttonType.canvasButton:
                    i.GetComponent<UI_button>().ButtonType = buttonType.ToString();
                    UI_objectButton cb = i.GetComponent<UI_objectButton>();
                    cb.isCloseSelfCanvas = isCloseSelfCanvas;
                    if (isOpenOtherObject) cb.openObject = targetObject; else cb.openObject = null;
                    break;
                case buttonType.customButton:
                    break;
                case buttonType.image:
                    i.GetComponent<Image>().sprite = image;
                    break;
                default:
                    break;
            }
            image = null;
            objectName = null;
            targetObject = null;
            isCloseSelfCanvas = false;
            isOpenOtherObject = false;
            isCloseClickOutside = false;

            loadInfomation();

        
        }

        public void loadInfomation()
        {

        }


    }

}
