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
            UI_System = transform.Find("UI_Manager").GetComponent<UI_System>();
        }

        // Update is called once per frame
        void Update()
        {
            //§PÂ_¬O§_ÂI¿ïUI
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {

            }
        }
        void OnMouseDown()
        {
            
        }

    }
}


