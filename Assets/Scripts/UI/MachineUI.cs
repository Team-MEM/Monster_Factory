namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class MachineUI : MonoBehaviour
    {
        GameObject currentMachine;
        bool scrap;
        [SerializeField] Text m_Name;
        [SerializeField] Text m_ElectricCost;
        [SerializeField] Image powerButton;

        void UISetup(GameObject machine)
        {
            currentMachine = machine;

            if (!currentMachine.GetComponent<Machine>().GetState())
            {
                powerButton.color = Color.red;
            }
            else
            {
                powerButton.color = new Color(255, 200, 0);
            }

            currentMachine.GetComponent<QuickOutline.Outline>().enabled = true;

            scrap = false;

            m_Name.text = currentMachine.GetComponent<Machine>().machineName;
            m_ElectricCost.text = currentMachine.GetComponent<Machine>().electriCostPerSecond.ToString() + "/s";
        }

        void OnDisable()
        {
            currentMachine.GetComponent<QuickOutline.Outline>().enabled = false;
        }

        public void Rotate(int degree)
        {
            AudioManager.Instance.rotate.Play();
            currentMachine.GetComponent<Machine>().RotateMachine(degree);
        }

        public void Scrap()
        {
            AudioManager.Instance.demolish.Play();
            // TODO NEED TO FIX - CELL_MACHINE REFERENCE
            currentMachine.GetComponent<Machine>().cell.GetComponent<Cell>().machine = null;
            currentMachine.GetComponent<Machine>().cell = null;
            currentMachine.SetActive(false);
            MainUIManager.Instance.MachineUIOff();

        }

        public void PowerToggle()
        {
            if (currentMachine.GetComponent<Machine>().GetState())
            {
                AudioManager.Instance.powerOff.Play();
                powerButton.color = Color.red;
            }
            else
            {
                AudioManager.Instance.powerOn.Play();
                powerButton.color = new Color(255, 200, 0);
            }

            currentMachine.GetComponent<Machine>().ToggleState();
        }
    }
}


