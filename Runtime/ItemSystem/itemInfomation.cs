using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NaiveAPI
{
    public class itemInfomation : ScriptableObject
    {
        [HeaderAttribute("Fill in if value exist. (SerachAPI require prefab or ID)")]
        public string ID;
        public Sprite Image;
        public GameObject prefab;
        [TextArea]
        public string description;
    }
}

