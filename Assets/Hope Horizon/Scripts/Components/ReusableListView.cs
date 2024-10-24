using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HopeHorizon.Scripts.Components
{
    public class ReusableListView : MonoBehaviour
    {
        [SerializeField]
        protected GameObject prefab;

        [SerializeField]
        protected GameObject parentGO;

        protected List<GameObject> itemGOList = new();
        public List<GameObject> ItemGOList => itemGOList;

        public UnityEvent<GameObject> ItemSpawned;

        public virtual void UpdateItemList(int count)
        {
            for (var i = 0; i < count; i++)
            {
                GameObject itemGO;
                if (i < itemGOList.Count)
                {
                    itemGO = itemGOList[i];
                }
                else
                {
                    itemGO = SpawnItem();
                }

                Activate(itemGO, i);
            }

            for (var i = count; i < itemGOList.Count; i++)
            {
                var itemGO = itemGOList[i];
                Deactivate(itemGO, i);
            }
        }

        protected virtual void Activate(GameObject itemGO, int index)
        {
            itemGO.SetActive(true);
        }

        protected virtual void Deactivate(GameObject itemGO, int index)
        {
            itemGO.SetActive(false);
        }

        public virtual void Clear()
        {
            for (var i = 0; i < itemGOList.Count; i++)
            {
                var itemGO = itemGOList[i];
                if (itemGO.activeInHierarchy)
                {
                    Deactivate(itemGO, i);
                }
            }
        }

        protected virtual GameObject SpawnItem()
        {
            var item = Instantiate(prefab, parentGO.transform);
            itemGOList.Add(item);
            ItemSpawned.Invoke(item);
            return item;
        }
    }
}
