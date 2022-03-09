using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


    public class closeIfClickOutside : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        bool isPointerInside;
        public NaiveAPI_UI_System UI_System;
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
            UI_System = GameObject.Find("EventSystem").GetComponent<NaiveAPI_UI_System>();
        }

        // Update is called once per frame
        void Update()
        {
            //§PÂ_¬O§_ÂI¿ïUI
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                UI_System.setActive(gameObject.name, isPointerInside);
                UI_System.displayReflush();
            }
        }
    }

