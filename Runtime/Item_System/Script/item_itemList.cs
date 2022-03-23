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

        public GameObject GetPrefab(string name)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (name == itemList[i].prefab.name)
                    return itemList[i].prefab;
            }
            return null;
        }
        public object GetComponentOnPrefab<T>(string name) where T : new()
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (name == itemList[i].prefab.name)
                    return itemList[i].prefab.GetComponent<T>();
            }
            return null;
        }
    }
}
