namespace MonsterFactory
{
    using System.Collections.Generic;
    using UnityEngine;

    public class PoolManager : MonoBehaviour
    {
        public static PoolManager instance;

        [SerializeField] int m_ObjectsPerPool;
        [SerializeField] List<PoolType> m_Pools;
        
        [System.Serializable]
        struct PoolType
        {
            public string poolTag;
            public GameObject prefab;
            public List<GameObject> pool;
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        void Start()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            foreach (PoolType _poolType in m_Pools)
            {
                GameObject _emptyGameObject = new GameObject();
                _emptyGameObject.transform.parent = transform;
                _emptyGameObject.name = _poolType.poolTag;

                for (int i = 0; i < m_ObjectsPerPool; i++)
                {
                    if (_poolType.prefab != null)
                    {
                        GameObject _clone = Instantiate(_poolType.prefab, _emptyGameObject.transform);
                        _clone.SetActive(false);
                        _poolType.pool.Add(_clone);
                    }
                    else
                    {
                        // TODO: REENABLE THIS AFTER I'M DONE WITH OTHER STUFFS
                        //Debug.LogError("Pool " + _poolType.poolTag + " doesn't have a prefab in it!!!");
                        break;
                    }
                }
            }
        }

        public GameObject RequestAvailableObject(string _poolTag)
        {
            foreach (PoolType _poolType in m_Pools)
            {
                if (_poolType.poolTag == _poolTag)
                {
                    for (int i = 0; i < _poolType.pool.Count; i++)
                    {
                        if (!_poolType.pool[i].gameObject.activeSelf)
                            return _poolType.pool[i];
                    }

                    GameObject _clone = Instantiate(_poolType.prefab, transform);
                    _clone.SetActive(false);
                    _poolType.pool.Add(_clone);
                    return _poolType.pool[_poolType.pool.Count - 1];
                }
            }
        
            Debug.LogError("This pool you trying to access doesn't exist! Check the tag you using! (" + _poolTag + ").");
            return null;
        }
    }
}