namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class AssemblerUI : MonoBehaviour
    {
        [SerializeField]
        GameObject inventory, recipeList;
        [SerializeField]
        GameObject[] tiers;

        void TierReset()
        {
            foreach (GameObject t in tiers)
            {
                t.SetActive(false);
            }
        }

        public void InventoryRecipeToggle()
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
                recipeList.SetActive(true);
                TierSelect(0);
            }
            else
            {
                recipeList.SetActive(false);
                inventory.SetActive(true);
            }
        }

        public void TierSelect(int t)
        {
            TierReset();

            tiers[t].SetActive(true);
        }

        //assembler ui components
        [Header("oft")]
        //need to rework slots into class
        [SerializeField] GameObject[] inventorySlots;

        GameObject currentMachine;

        void AssemblerUISetup(GameObject machine)
        {
            currentMachine = machine;

            foreach (GameObject slot in inventorySlots)
            {
                slot.SetActive(false);
            }

            StartCoroutine("AssemblerUIUpdate", machine);
        }

        //sets up the inventory. it works.... dont judge me   - Okay, no judge
        IEnumerator AssemblerUIUpdate(GameObject machine)
        {
            while (true)
            {
                List<IngredientRecord> record = machine.GetComponent<Assembler>().GetInventory();

                foreach (IngredientRecord rec in record)
                {
                    foreach (GameObject slot in inventorySlots)
                    {
                        if (slot.name == rec.name)
                        {
                            slot.SetActive(true);
                            Transform text = slot.transform.GetChild(0);
                            text.gameObject.GetComponent<Text>().text = "" + rec.amount;
                            break;
                        }
                        
                    }
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        [SerializeField] Image currentRecipe;
        
        public void SetRecipe(Recipe recipe)
        {
            currentMachine.GetComponent<Assembler>().SetRecipe(recipe);
            InventoryRecipeToggle();
        }

        public void SetSprite(Sprite sprite)
        {
            currentRecipe.sprite = sprite;
        }

    }
}


