namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class SorterUI : MonoBehaviour
    {
        [SerializeField]
        GameObject filterSelect;
        public bool currentSide;
        [SerializeField] Image spriteLeft, spriteRight, powerButton;
        public Text powerText;
        public Sprite empty;

        GameObject currentMachine;

        #region Basic Buttons
        bool scrap;

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
        #endregion

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

            if (machine.GetComponent<Sorter>().m_Filter_Left != null && machine.GetComponent<Sorter>().m_Filter_Left.sprite != null)
            {
                spriteLeft.sprite = machine.GetComponent<Sorter>().m_Filter_Left.sprite;
            }
            else
            {
                spriteLeft.sprite = empty;
            }

            if (machine.GetComponent<Sorter>().m_Filter_Right != null && machine.GetComponent<Sorter>().m_Filter_Right.sprite != null)
            {
                spriteRight.sprite = machine.GetComponent<Sorter>().m_Filter_Right.sprite;
            }
            else
            {
                spriteRight.sprite = empty;
            }
            

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

        public void SetSide(bool side)
        {
            AudioManager.Instance.click.Play();
            currentSide = side;
        }

        public void SetFilter(Item _itemToSort)
        {
            AudioManager.Instance.click.Play();
            if (currentSide)
                currentMachine.GetComponent<Sorter>().SetRightFilter(_itemToSort);
            else
                currentMachine.GetComponent<Sorter>().SetLeftFilter(_itemToSort);
        }

        public void SetSprite(Sprite sprite)
        {
            if (currentSide)
                spriteRight.sprite = sprite;
            else
                spriteLeft.sprite = sprite;
        }


    }
}

