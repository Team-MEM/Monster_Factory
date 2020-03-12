namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class ResourceData
    {
        public int[] newStorageAmount = new int[6];

        public ResourceData(ResourceManager res)
        {
            for (int i = 0; i < res.m_Storage.Count; i++)
                newStorageAmount[i] = res.m_Storage[i].quantity;
  
        }
    }

}

