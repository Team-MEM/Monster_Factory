namespace MonsterFactory
{
    using System.Collections.Generic;
    using UnityEngine;

    public class PoolManager : Singleton<PoolManager>
    {
        [SerializeField] PoolType[] m_ItemPools;
        [SerializeField] PoolType[] m_MonsterPools;
        [SerializeField] PoolType[] m_MachinePools;
        [SerializeField] PoolType[] m_ParticlePools;
                
        [System.Serializable]
        struct PoolType
        {
            public string poolTag;
            public GameObject prefab;
            public List<GameObject> pool;
        }
    
        void Start()
        {
            InitializePools(m_ItemPools, "ItemPools", 100);
            InitializePools(m_MonsterPools, "MonsterPools", 40);
            InitializePools(m_MachinePools, "MachinePools", 10);
            InitializePools(m_ParticlePools, "MachinePools", 5);
        }

        #region Resetting - MassDiable, CleanSceneItem
        /// <summary>
        /// Reset entire scene.
        /// </summary>
        public void MassDisable()
        {
            foreach (PoolType _itemPool in m_ItemPools)
            {
                for (int i = 0; i < _itemPool.pool.Count; i++)
                {
                    _itemPool.pool[i].SetActive(false);
                }
            }

            foreach (PoolType _monsterPool in m_MonsterPools)
            {
                for (int i = 0; i < _monsterPool.pool.Count; i++)
                {
                    _monsterPool.pool[i].SetActive(false);
                }
            }

            foreach (PoolType _machinePool in m_MachinePools)
            {
                for (int i = 0; i < _machinePool.pool.Count; i++)
                {
                    if (_machinePool.pool[i].GetComponent<Machine>().cell != null && _machinePool.pool[i].GetComponent<Machine>().name != "Assembler")
                    {
                        _machinePool.pool[i].GetComponent<Machine>().accumulatedElectricCost = 0;
                        _machinePool.pool[i].GetComponent<Machine>().cell.GetComponent<Cell>().machine = null;
                        _machinePool.pool[i].GetComponent<Machine>().cell = null;
                        _machinePool.pool[i].SetActive(false);
                    }
                    
                }
            }
        }

        /// <summary>
        /// Disable all items and Monsters in the scene
        /// </summary>
        public void ClearSceneItems()
        {
            for (int i = 0; i < m_ItemPools.Length; i++)
                for (int j = 0; j < m_ItemPools[i].pool.Count; j++)
                    m_ItemPools[i].pool[j].SetActive(false);
            
            for (int i = 0; i < m_MonsterPools.Length; i++)
                for (int j = 0; j < m_MonsterPools[i].pool.Count; j++)
                    m_MonsterPools[i].pool[j].SetActive(false);
        }
        #endregion

        /// <summary>
        /// Preload the pool with objects.
        /// </summary>
        private void InitializePools(PoolType[] _masterPool, string _masterPoolName, int _objectsPerPool)
        {
            for (int i = 0; i < _masterPool.Length; i++)
            {
                if (_masterPool[i].prefab != null)
                    _masterPool[i].poolTag = _masterPool[i].prefab.name;
                else
                    _masterPool[i].poolTag = "This prefab is not assigned!";
            }

            GameObject _emptyMasterPool = new GameObject();
            _emptyMasterPool.name = _masterPoolName;
            _emptyMasterPool.transform.parent = transform;


            for (int i = 0; i < _masterPool.Length; i++)
            {
                // TODO: Make the _poolType.poolTag = _poolType.prefab.name;
                GameObject _emptyGameObject = new GameObject();
                _emptyGameObject.transform.parent = _emptyMasterPool.transform;
                _emptyGameObject.name = _masterPool[i].prefab.name;

                for (int j = 0; j < _objectsPerPool + 1; j++)
                {
                    if (_masterPool[i].prefab != null)
                    {
                        GameObject _clone = Instantiate(_masterPool[i].prefab, _emptyGameObject.transform);
                        _clone.SetActive(false);
                        _masterPool[i].pool.Add(_clone);
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


        /// <summary>
        /// Request a object from the matched pool (_poolTag) in master pool (_masterPoolTag).
        /// </summary>
        /// <param name="_poolTag">To locate the pool to get object from</param>
        /// <param name="_masterPoolTag">To locate the master pool to access the object pool</param>
        /// <returns></returns>
        public GameObject RequestAvailableObject(string _poolTag, string _masterPoolTag)
        {
            PoolType[] _masterPool;

            switch (_masterPoolTag)
            {
                case "ItemPools":
                    _masterPool = m_ItemPools;
                    break;

                case "MonsterPools":
                    _masterPool = m_MonsterPools;
                    break;

                case "MachinePools":
                    _masterPool = m_MachinePools;
                    break;

                default:
                    Debug.LogWarning("The master pool does not exist! (" + _masterPoolTag + ")");
                    _masterPool = null;
                    break;
            }

            if (_masterPool != null)
            {
                foreach (PoolType _poolType in _masterPool)
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
            }
        
            Debug.LogError("This pool you trying to access doesn't exist! Check the tag you using! (" + _poolTag + ").");
            return null;
        }
    }
}