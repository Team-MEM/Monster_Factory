namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Resource manager oversee all the resource in a round.
    /// <para> - Add ingredients to storage</para>
    /// <para> - Return storage info to UI</para>
    /// <para> - </para>
    /// </summary>
    public class ResourceManager : Singleton<ResourceManager>
    {
        [System.Serializable]
        public class StorageRecord
        {
            public Item item;
            public int quantity;
        }
        
        public List<StorageRecord> m_Storage;
        public List<StorageRecord> _tempStorage;

        #region Save/Load
        public void SaveGame()
        {
            SaveSystem.SaveResource(Instance);
        }

        public void LoadGame()
        {
            ResourceData rec = SaveSystem.LoadResource();
            
            for (int i = 0; i < m_Storage.Count; i++)
                m_Storage[i].quantity = rec.newStorageAmount[i];

            UpdateResourceBar();
        }
        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                for (int i = 0; i < m_Storage.Count; i++)
                {
                    m_Storage[i].quantity += 100;
                }
            }
                
        }

        /// <summary>
        /// Add Ingredient to the storage list of currant game day.
        /// <para>!!! quantity of item will be limited to 999</para>
        /// </summary>
        public void AddIngredientToStorage(string _itemName)
        {
            for (int i = 0; i < m_Storage.Count; i++)
                if (_itemName == m_Storage[i].item.name)
                {
                    m_Storage[i].quantity++;
                    Mathf.Clamp(m_Storage[i].quantity, 0, 999);
                }

            UpdateResourceBar();
        }

        #region Resource Check
        /// <summary>
        /// Receive recipe passed down by Assembler. Check if there's enough resources to produce the monster.
        /// <para>Returns a bool to tell Assembler if it can produce the monster</para>
        /// </summary>
        /// <param name="_recipe">Passed down from Assembler.ProduceMonster(Recipe)</param>
        public bool CheckIfEnoughResource(Recipe _recipe)
        {
            int _typeOfIngredientNeeded = _recipe.itemRecords.Length;

            // Save a copy of storages.
            _tempStorage = m_Storage;
            
            foreach (ItemRecord _itemRecord in _recipe.itemRecords)
            {
                if (CheckInStorage(_itemRecord.name, _itemRecord.amount) == 0)
                    _typeOfIngredientNeeded--;
                else
                    break;
            }

            if (_typeOfIngredientNeeded == 0)
            {
                UpdateResourceBar();
                return true;
            }
            else
            {
                m_Storage = _tempStorage;
                return false;
            }
        }
        
        /// <summary>
        /// Check passed item with each item in the storage, when matched, check if has the right amount.
        /// </summary>
        /// <param name="_requiredItem"></param>
        /// <param name="_requiredAmount"></param>
        /// <returns></returns>
        private int CheckInStorage(string _requiredItem, int _requiredAmount)
        {
            for (int i = 0; i < m_Storage.Count; i++)
            {
                if (_requiredItem == m_Storage[i].item.name)
                {
                    if (m_Storage[i].quantity >= _requiredAmount)
                    {
                        m_Storage[i].quantity -= _requiredAmount;
                        return 0;
                    }
                }
            }

            return 1;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_item"> sadas</param>
        /// <returns></returns>
        public void UpdateResourceBar()
        {
            int _totalQuantity = 0;

            foreach (ResourceUIItem _resourceUIItem in MainUIManager.Instance.resourceUIItemList)
            {
                foreach (StorageRecord _record in m_Storage)
                    if (_resourceUIItem.item == _record.item)
                        _totalQuantity += _record.quantity;
                
                _resourceUIItem.quantityText.text = ":" + _totalQuantity.ToString();
                _totalQuantity = 0;
            }
        }
    }
}
