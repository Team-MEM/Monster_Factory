namespace MonsterFactory
{
    using UnityEngine;

    [System.Serializable]
    public class ItemRecord
    {
        public string name;
        public int amount;

        public ItemRecord(string _name, int _amount)
        {
            name = _name;
            amount = _amount;
        }
    }
}
