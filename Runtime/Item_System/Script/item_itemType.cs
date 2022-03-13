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
        public GameObject prefab;
    }
}


