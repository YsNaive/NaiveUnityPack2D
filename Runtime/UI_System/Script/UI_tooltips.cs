using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NaiveAPI
{
    public abstract class UI_tooltips :MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject toolTips;
        public bool isFollowPointer = false;
        public Vector2 followPointerOffset = new Vector2(0, 0);
        public tooltipsMode mode = tooltipsMode.Hold;

        private bool isPointerInside;
        private RectTransform rectTransform;
        private bool isInUIS = false;
        private UI_System UI_System;
        public enum tooltipsMode
        {
            Switch,
            Hold
        }

        public UI_tooltips(GameObject toolTips)
        {
            this.toolTips = toolTips;
        }

        private void Start()
        {
            rectTransform = toolTips.GetComponent<RectTransform>();
            UI_System = GameObject.Find("UI_Manager").GetComponent<UI_System>();
            if (toolTips.GetComponent<UI_state>() != null) isInUIS = true;
            setActive(false);
        }
        private void Update()
        {
            
            if(mode == tooltipsMode.Switch)
            {
                if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    setActive(isPointerInside);
                }
            }
            
            if (isFollowPointer) rectTransform.position = Input.mousePosition + (Vector3)followPointerOffset;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            isPointerInside = true;
            if(mode == tooltipsMode.Hold) setActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPointerInside = false;
            if (mode == tooltipsMode.Hold) setActive(false);
        }

        public void FollowPointer(float offsetX, float offsetY)
        {
            isFollowPointer = true;
            followPointerOffset = new Vector2(offsetX, offsetY);
        }
        private void setActive(bool active)
        {
            if (isInUIS) UI_System.setActive(toolTips, active);
            else toolTips.SetActive(active);
        }
        public void SetMode(tooltipsMode setMode) { mode = setMode; }
    }
}
