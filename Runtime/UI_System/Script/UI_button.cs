using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NaiveAPI
{
    public abstract class UI_button : MonoBehaviour
    {
        [HideInInspector]
        public UI_System UI_System;
        void Awake()
        {
            UI_System = GameObject.Find("UI_Manager").GetComponent<UI_System>();
            GetComponent<Button>().onClick.AddListener(onClick);
        }
        private void Update()
        {
        }
        public abstract void localAwake();
        public abstract void onClick();
    }

}
