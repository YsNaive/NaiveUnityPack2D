using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UI_state : MonoBehaviour
{
    public string thisUI_type;
    public bool isActive;
    public bool ignoreClear;
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
