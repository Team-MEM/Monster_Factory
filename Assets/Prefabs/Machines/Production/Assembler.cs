namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Assembler : Machine
    {
        [SerializeField] List<IngredientRecord> m_Inventory;
        [SerializeField] Recipe m_SelectedRecipes;

        private int m_RequiredIngredientCount = 0;

        void ProduceMonster()
        {
            if (m_State)
            {
                if (m_SelectedRecipes != null)
                {
                    // Check if there is a monster in queue, if not, fetch how many type of ingredients it need to produce a monster.
                    if (m_RequiredIngredientCount == 0)
                    {
                        m_RequiredIngredientCount = m_SelectedRecipes.ingredientRecords.Length;
                        Debug.Log("This recipe needs " + m_RequiredIngredientCount + " ingredients!");
                    }

                    // Check if m_Inventory has required number of ingredient
                    for (int i = 0; i < m_Inventory.Count; i++)
                    {
                        foreach (IngredientRecord _recipe in m_SelectedRecipes.ingredientRecords)
                        {
                            if (m_Inventory[i].name == _recipe.name)
                            {
                                if (m_Inventory[i].amount >= _recipe.amount)
                                {
                                    m_Inventory[i].amount -= _recipe.amount;
                                    m_RequiredIngredientCount--;
                                    Debug.Log("That's " + m_RequiredIngredientCount + " ingredients left!");
                                }
                            }
                        }
                    }

                    // Produce monster when all the ingredient is there.
                    if (m_RequiredIngredientCount == 0)
                    {
                        Debug.Log("You got a monster");
                        PoolObject(m_SelectedRecipes.name);
                    }
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Item>() != null)
            {
                Item _ingredient = other.GetComponent<Item>();
                Debug.Log("I just got a " + _ingredient.name);

                // Check if this ingredient exist in the inventory
                if (!HasRecord(_ingredient.name))
                {
                    IngredientRecord _newRecord = new IngredientRecord(_ingredient.name, _ingredient.gameObject, 1);
                    m_Inventory.Add(_newRecord);
                }
                else
                {
                    for (int i = 0; i < m_Inventory.Count; i++)
                        if (_ingredient.name == m_Inventory[i].name)
                            m_Inventory[i].amount++;
                }

                ProduceMonster();
                other.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("What is this " + other.gameObject.name + "?");
                other.gameObject.SetActive(false);
            }
        }


        // Check if this ingredient already have its own record in m_Inventory.
        private bool HasRecord(string _name)
        {
            foreach (IngredientRecord _record in m_Inventory)
                if (_record.name == _name)
                    return true;
            return false;
        }
        
        #region Getter Functions
        public List<IngredientRecord> GetInventory() { return m_Inventory; }
        public void SetRecipe(Recipe recipe) { m_SelectedRecipes = recipe; }
        #endregion
    }
}