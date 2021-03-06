using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace NaiveAPI
{
    public class UI_closeIfClickOutside : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        bool isPointerInside;
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
            UI_System = GameObject.Find("UI_Manager").GetComponent<UI_System>();
        }

        // Update is called once per frame
        void Update()
        {
            //?P?_?O?_?I??UI
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                UI_System.setActive(gameObject, isPointerInside);
            }
        }
    }
}


