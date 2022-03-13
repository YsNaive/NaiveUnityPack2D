using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    [CreateAssetMenu(menuName = "NaiveAPI/Item System/ItemList")]
    [System.Serializable]
    public class item_itemList : ScriptableObject
    {
        [SerializeField]
        public List<item_itemType> itemList = new List<item_itemType>();
    }
}
