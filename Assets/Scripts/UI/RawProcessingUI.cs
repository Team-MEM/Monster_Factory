namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class RawProcessingUI : MonoBehaviour
    {
        #region Basic Buttons
        public Text powerText;

        public void Rotate(int degree)
        {
            currentMachine.GetComponent<Machine>().RotateMachine(degree);
        }

        public void Scrap()
        {

            // TODO NEED TO FIX - CELL_MACHINE REFERENCE
            currentMachine.GetComponent<Machine>().cell.GetComponent<Cell>().machine = null;
            currentMachine.GetComponent<Machine>().cell = null;
            currentMachine.SetActive(false);
            MainUIManager.Instance.MachineUIOff();

        }


        public void PowerToggle()
        {
            currentMachine.GetComponent<Machine>().ToggleState();
        }
        #endregion

        [SerializeField] GameObject inventory;
        [SerializeField] GameObject recipeList;

        public void InventoryRecipeToggle()
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
                recipeList.SetActive(true);
            }
            else
            {
                recipeList.SetActive(false);
                inventory.SetActive(true);
            }
        }

        //ui components
        [Header("ooph")]
        //need to rework slots into class
        [SerializeField] Text queue;
        [SerializeField]
        Image currentRecipe;

        GameObject currentMachine;
        int component;

        void UISetup(GameObject machine)
        {
            currentMachine = machine;

            currentRecipe.sprite = null;
            //currentRecipe.sprite = currentMachine.GetComponent<Processor>().GetSelectedOutput().sprite;

            currentMachine.GetComponent<QuickOutline.Outline>().enabled = true;

            StartCoroutine("UIUpdate", currentMachine);
            powerText.text = currentMachine.GetComponent<Machine>().electriCostPerSecond.ToString("0.0") + "/s";

        }

        //need skin,limb and sensory scripts implemented for this to work


        IEnumerator UIUpdate(GameObject selected)
        {

            while (true)
            {
                //queue.text = "" + selected.GetComponent<Processor>().GetCurrentQueueSize();

                yield return new WaitForSeconds(0.5f);
            }
        }

        void OnDisable()
        {
            currentMachine.GetComponent<QuickOutline.Outline>().enabled = false;
        }

        

        public void SetOutput(Item output)
        {
            //currentMachine.GetComponent<Processor>().SetSelectedOutput(output);
            currentRecipe.sprite = output.sprite;
            InventoryRecipeToggle();
        }

    }
}


