namespace MonsterFactory
{
    using System.Collections.Generic;
    using UnityEngine;

    public class EconomyManager : Singleton<EconomyManager>
    {
        [Header("Economy System")]
        public List<Item> m_SellingList;
        public int totalMoney;
        public float totalExpense;

        public float totalMonsters;

        #region Save/Load
        public void SaveGame()
        {
            SaveSystem.SaveEconomy(this);
        }

        public void LoadGame()
        {
            EconomyData eco = SaveSystem.LoadEconomy();

            totalMoney = eco.money;
            totalExpense = eco.expense;
            totalMonsters = eco.totalMonsters;
            SetupEconomy();
        }
        #endregion

        private void Start()
        {
            SetupEconomy();
        }

        #region Setup Economy System
        void SetupEconomy()
        {
            MainUIManager.Instance.UpdateMoneyUI(Mathf.RoundToInt(totalMoney));
        }
        #endregion

        #region Economy

        public void AddToSellingList(Item _item)
        {
            m_SellingList.Add(_item);
        }

        // Check if there's enough money to build the machine
        public bool CheckMoney(int _cost)
        {
            if (totalMoney >= _cost)
                return true;
            else
                return false;
        }

        public void SpendMoney(int _expense)
        {
            totalMoney -= _expense;
            MainUIManager.Instance.UpdateMoneyUI(Mathf.RoundToInt(totalMoney));
        }

        /// <summary>
        /// Runs when a game day is finished (GameManager.dayCountdown == 0), then find out the total value of profit (Sales of Items - Costs of Electricity).
        /// </summary>
        public void CalculateRevenue()
        {
            int _monsterProduced = 0;
            int _sales = 0;
            float _expense = 0;
            int _reward = 0;
            int _goalCount = 0;

            // Sum of the value of the items in the selling area.
            foreach (Item _item in m_SellingList)
            {
                if (_item == TaskManager.Instance.currentTask.item)
                {
                    _goalCount++;
                }

                _sales += _item.value;
                _monsterProduced++;
            }
            
            totalMonsters += _monsterProduced;    

            MainUIManager.Instance.monsterProduced.text = _monsterProduced.ToString();
            MainUIManager.Instance.sales.text = _sales.ToString("0.00");

            // Sum of the electricity cost of the machines in the scene.
            foreach (Machine _machine in BuildManager.Instance.machinesInScene)
                _expense += _machine.accumulatedElectricCost;

            MainUIManager.Instance.costs.text = _expense.ToString("0.00");

            // Calculate & display the reward for completing a task

            if (TaskManager.Instance.currentTask.isCompleted)
                _reward = TaskManager.Instance.currentTask.reward;

            MainUIManager.Instance.reward.text = _reward.ToString("0.00");

            // Calculate the revenue at the end.
            totalMoney += (_sales + _reward - Mathf.RoundToInt(_expense));
            MainUIManager.Instance.sum.text = (_sales - Mathf.RoundToInt(_expense)).ToString("0.00");
            MainUIManager.Instance.closingBalance.text = totalMoney.ToString("0.00");
        }
        #endregion
    }
}
