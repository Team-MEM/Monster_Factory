namespace MonsterFactory
{
    using UnityEngine;

    /// <summary>
    /// Storage facility that helds all and only the ingredients player transported to it.
    /// </summary>
    public class Storage : Machine
    {
        private void OnTriggerEnter(Collider other)
        {
            // Check if it's an ingredient
            if (other.GetComponent<Item>().id == 0)
            {
                // Tell Resource manager to add ingredient to storage list
                ResourceManager.Instance.AddIngredientToStorage(other.GetComponent<Item>().name);
                other.gameObject.SetActive(false);
            }
            else
            {
                //Disable whatever else player throw into storage.
                other.gameObject.SetActive(false);
            }
        }
    }
}