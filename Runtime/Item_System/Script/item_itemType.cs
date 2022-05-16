using System;
using UnityEditor;
using UnityEngine;

namespace NaiveAPI
{
    [CreateAssetMenu(menuName = "NaiveAPI/Item System/Item")]
    [System.Serializable]
    public class item_itemType : ScriptableObject
    {
        public Texture2D icon;
        public string itemName;
        public string displayName;
        public int stackLimit;
        public string group; 
        public GameObject prefab;

        public UnityEngine.Object infomation;

        public T getInfomation<T>()
        {
            T output = (T)(object)infomation;
            return output;
        }
    }
}