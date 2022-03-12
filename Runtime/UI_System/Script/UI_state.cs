using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NaiveAPI
{
    [System.Serializable]
    public class UI_state : MonoBehaviour
    {
        [HideInInspector]
        public string thisUI_type;
        public bool isActive;
        public bool ignoreClear;
        [HideInInspector]
        public bool closeIfMouseClickOutside;
        public void setActive(bool isActive)
        {
            this.isActive = isActive;
        }
        public void setIgnoreClear(bool ignoreClear)
        {
            this.ignoreClear = ignoreClear;
        }
    }
}

