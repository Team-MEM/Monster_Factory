namespace MonsterFactory
{
    using UnityEngine;

    [System.Serializable]
    public class IngredientRecord
    {
        public string name;
        public GameObject ingredient;
        public int amount;

        public IngredientRecord(string _name, GameObject _ingredient, int _amount)
        {
            name = _name;
            ingredient = _ingredient;
            amount = _amount;
        }
    }
}
