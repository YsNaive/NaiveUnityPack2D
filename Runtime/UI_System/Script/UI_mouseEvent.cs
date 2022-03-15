using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace NaiveAPI
{
    public class UI_mouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool isPointerInside;
        [HideInInspector]
        public UI_System UI_System;
        public bool isCloseWhenClickOutside = false;
        public void OnPointerEnter(PointerEventData eventData)
        {
            isPointerInside = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPointerInside = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            UI_System = GameObject.Find("UI_Manager").GetComponent<UI_System>();
        }

        // Update is called once per frame
        void Update()
        {
            //�P�_�O�_�I��UI
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                if(isCloseWhenClickOutside)
                    UI_System.setActive(gameObject, isPointerInside);
            }
        }
    }
}


