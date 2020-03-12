namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class EconomyData
    {
        public int money;
        public float expense, totalMonsters;

        public EconomyData(EconomyManager eco)
        {
            money = eco.totalMoney;
            expense = eco.totalExpense;
            totalMonsters = eco.totalMonsters;
        }
    }

}

