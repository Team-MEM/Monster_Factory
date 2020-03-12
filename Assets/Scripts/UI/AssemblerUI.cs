namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class AssemblerUI : MonoBehaviour
    {
        [SerializeField]
        GameObject costView;//recipeList
        [SerializeField]
        GameObject[] tiers;

        //assuming there is only one assembler in the scene
        #region Singleton
        public static AssemblerUI instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }
        #endregion


        void TierReset()
        {
            foreach (GameObject t in tiers)
            {
                t.SetActive(false);
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
        [SerializeField] GameObject[] costSlots;
        [SerializeField] Text sellPrice;
        [SerializeField] Image monsterSprite;
        //Image currentRecipe;
        GameObject currentMachine;
        //Recipe startrecipe;

        void UISetup(GameObject machine)
        {
            currentMachine = machine;

            currentMachine.GetComponent<QuickOutline.Outline>().enabled = true;
            
        }

        void OnDisable()
        {
            currentMachine.GetComponent<QuickOutline.Outline>().enabled = false;
        }

        // Send a order to AssemblyManager.cs
        public void SentOrder(Recipe _recipe)
        {
            if (currentMachine.GetComponent<Assembler>().ProduceMonster(_recipe))
            {
                AudioManager.Instance.monsterCreated.Play();
            }
            else
            {
                MainUIManager.Instance.ErrorMessage("Not Enough Resources");
            }
            //InventoryRecipeToggle();
        }

        void ResetCostView()
        {
            foreach (GameObject slot in costSlots)
            {
                slot.SetActive(false);
            }
        }

        //checks the ingridents against the slot names to determine what needs to be displayed in the cost window
        void CostViewSetup(ItemRecord[] _ingredients)
        {
            ResetCostView();

            foreach (ItemRecord _record in _ingredients)
            {
                foreach (GameObject slot in costSlots)
                {
                    if (slot.name == _record.name)
                    {
                        slot.SetActive(true);
                        slot.transform.GetChild(0).GetComponent<Text>().text = _record.amount + "";
                        break;
                    }
                }
            }
        }

        public void CostViewOn(Recipe _recipe, int _value)
        {
            CostViewSetup(_recipe.itemRecords);
            sellPrice.text = "$" + _value;
            monsterSprite.sprite = _recipe.recipeSprite;
            costView.SetActive(true);
        }

        public void CostViewOff()
        {
            costView.SetActive(false);
        }


    }
}


