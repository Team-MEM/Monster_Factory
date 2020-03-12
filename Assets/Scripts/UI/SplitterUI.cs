namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class SplitterUI : MonoBehaviour
    {
        [SerializeField]
        GameObject filterSelect;
        bool currentSide;
        GameObject filter;
        [SerializeField]
        Image filterSprite;
        public Text powerText;

        GameObject currentMachine;

        #region Basic Buttons
        bool scrap;

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

        void UISetup(GameObject machine)
        {
            currentMachine = machine;

            currentMachine.GetComponent<QuickOutline.Outline>().enabled = true;

            powerText.text = currentMachine.GetComponent<Machine>().electriCostPerSecond.ToString("0.0") + "/s";
        }

        void OnDisable()
        {
            currentMachine.GetComponent<QuickOutline.Outline>().enabled = false;
        }

        public void ToggleFilter()
        {
            filterSelect.SetActive(!filterSelect.activeSelf);
        }

        public void SetFilter(GameObject filterObject)
        {
            filter = filterObject;
        }

        public void SetSprite(Sprite sprite)
        {
            filterSprite.sprite = sprite;
        }



    }
}