using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace MonsterFactory
{
    public class MainUIManager : Singleton<MainUIManager>
    {
        [Header("Menu UI")]
        public GameObject TransportMenu;
        public GameObject ProductionMenu;
        public GameObject ProcessingMenu;
        public GameObject RecipeMenu;
        public GameObject TechMenu;
        public GameObject EscMenu;
        public GameObject MenuBar;
        public GameObject MachineTooltipBox;
        public Text UIM_Name, UIM_Cost;
        public TextMeshProUGUI UIM_Description;

        private int page;
        
        void Start()
        {
            page = 0;
            StartCoroutine("CheckForMachineSelect");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
                MachineUIOff();
        }

        void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                CheckForMachineSelect();
        }

        //Turns UI off
        public void UIOff()
        {
            AudioManager.Instance.click.Play();
            RecipeMenu.SetActive(false);
            TechMenu.SetActive(false);
            TransportMenu.SetActive(false);
            ProductionMenu.SetActive(false);
            ProcessingMenu.SetActive(false);
            EscMenu.SetActive(false);
            MachineTooltipBox.SetActive(false);
            MachineUIOff();
        }

        public void GUIToggle()
        {

            MenuBar.SetActive(!MenuBar.activeSelf);
            TechTreeUpdate();
        }


        public void ToggleMenu(GameObject _gameObject)
        {
            if (_gameObject.activeSelf)
            {
                AudioManager.Instance.click.Play();
                _gameObject.SetActive(false);
            }              
            else
            {
                UIOff();
                _gameObject.SetActive(true);
            }
        }

        public GameObject resourceView, upButton, downButton;
        bool isDown;

        public void ToggleResource()
        {
            Vector3 pos = resourceView.transform.localPosition;
            if (!isDown)
            {
                pos = new Vector3(pos.x, pos.y - 40, pos.z);
                upButton.SetActive(true);
                downButton.SetActive(false);
            }
            else
            {
                pos = new Vector3(pos.x, pos.y + 40, pos.z);
                upButton.SetActive(false);
                downButton.SetActive(true);
            }

            isDown = !isDown;
            resourceView.transform.localPosition = pos;
        }

        [SerializeField]
        GameObject ErrorContainer, ErrorBox;
        [SerializeField]
        Text errorMessage;


        public void ErrorMessage(string message)
        {
            AudioManager.Instance.error.Play();
            errorMessage.text = message;
            ErrorContainer.SetActive(true);
            StopCoroutine("ErrorAnim");
            ErrorBox.transform.localScale = Vector3.zero;
            StartCoroutine("ErrorAnim");
        }

        IEnumerator ErrorAnim()
        {
            while (ErrorBox.transform.localScale.y < 1)
            {
                ErrorBox.transform.localScale += new Vector3(0, 0.25f, 0);
                yield return null;
            }

            while (ErrorBox.transform.localScale.x < 1)
            {
                ErrorBox.transform.localScale += new Vector3(0.2f, 0, 0);
                yield return null;
            }

            yield return new WaitForSeconds(2f);

            while (ErrorBox.transform.localScale.x > 0)
            {
                ErrorBox.transform.localScale -= new Vector3(0.2f, 0, 0);
                yield return null;
            }

            while (ErrorBox.transform.localScale.y > 0)
            {
                ErrorBox.transform.localScale -= new Vector3(0, 0.25f, 0);
                yield return null;
            }

           
            ErrorContainer.SetActive(false);
        }

        #region Util Funcions - IsPointerOverUIObject
        // Checks if mouse is over ui object and returns a bool
        public bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        #endregion

        #region Tech Tree
        [SerializeField]
        Image bar1, bar2, bar3, bar4, bar5, unlock1, unlock2, unlock3, unlock4, unlock5;

        public void TechTreeUpdate()
        {
            if (UnlockManager.unlocks.level < 1)
            {
                bar1.fillAmount = EconomyManager.Instance.totalMonsters / UnlockManager.unlocks.exp1;
            }
            else if (UnlockManager.unlocks.level < 2)
            {
                bar1.fillAmount = 1;
                unlock1.color = Color.yellow;
                bar2.fillAmount = (EconomyManager.Instance.totalMonsters - UnlockManager.unlocks.exp1) / (UnlockManager.unlocks.exp2 - UnlockManager.unlocks.exp1);
            }
            else if (UnlockManager.unlocks.level < 3)
            {
                bar1.fillAmount = bar2.fillAmount = 1;
                unlock1.color = unlock2.color = Color.yellow;
                bar3.fillAmount = (EconomyManager.Instance.totalMonsters - UnlockManager.unlocks.exp2) / (UnlockManager.unlocks.exp3 - UnlockManager.unlocks.exp2);
            }
            else if (UnlockManager.unlocks.level < 4)
            {
                bar1.fillAmount = bar2.fillAmount = bar3.fillAmount = 1;
                unlock1.color = unlock2.color = unlock3.color = Color.yellow;
                bar4.fillAmount = (EconomyManager.Instance.totalMonsters - UnlockManager.unlocks.exp3) / (UnlockManager.unlocks.exp4 - UnlockManager.unlocks.exp3);
            }
            else if (UnlockManager.unlocks.level < 5)
            {
                bar1.fillAmount = bar2.fillAmount = bar3.fillAmount = bar4.fillAmount = 1;
                unlock1.color = unlock2.color = unlock3.color = unlock4.color = Color.yellow;
                bar5.fillAmount = (EconomyManager.Instance.totalMonsters - UnlockManager.unlocks.exp4) / (UnlockManager.unlocks.exp5 - UnlockManager.unlocks.exp5);
            }
            else
            {
                bar1.fillAmount = bar2.fillAmount = bar3.fillAmount = bar4.fillAmount = bar5.fillAmount = 1;
                unlock1.color = unlock2.color = unlock3.color = unlock4.color = unlock5.color = Color.yellow;
            }
        }
        #endregion

        #region Recipe Book - UpdatePage, ResetPage, NextPage, PreviousPage ,JumptoPage
        [Header("Recipe Book")]
        //Recipe Book
        [SerializeField] GameObject[] pages;

        //goes through pages, deactivates them and then activates desired page
        void UpdatePage()
        {
            foreach (GameObject i in pages)
                i.SetActive(false);
            pages[page].SetActive(true);
        }

        public void ResetPage()
        {
            page = 1;
            UpdatePage();
        }

        public void NextPage()
        {
            page++;
            if (page >= pages.Length)
                page = 0;
            UpdatePage();
        }

        public void PreviousPage()
        {
            page--;
            if (page < 0)
                page = 11;
            UpdatePage();
        }

        public void JumpToPage(int pageNo)
        {
            page = pageNo;
            UpdatePage();
        }
        #endregion

        #region Machine UI - MachineUIOff, CheckForMachineSelect
        //Machine UI and raycast
        [Header("Machine UI")]

        [SerializeField] LayerMask machineLayer;

        [SerializeField] Transform MachineUIHolder;
        //machine specific UIs
        [SerializeField] GameObject assemblerUI, skinUI, limbUI, sensoryUI, sorterUI, splitterUI, genericUI;

        Ray castPoint;
        RaycastHit hit;

       public void MachineUIOff()
        {           
            foreach (Transform child in MachineUIHolder)
                child.gameObject.SetActive(false);
        }

        //coroutine to control machine UIs.
        void CheckForMachineSelect()
        {
            UIOff();
            MachineUIOff();

            castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, machineLayer))
            {
                AudioManager.Instance.click.Play();
                if (hit.transform.gameObject.tag == "Assembler")
                {
                    assemblerUI.SetActive(true);
                    assemblerUI.SendMessage("UISetup", hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.tag == "GenericMachine")
                {
                    genericUI.SetActive(true);
                    genericUI.SendMessage("UISetup", hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.tag == "Sorter")
                {
                    sorterUI.SetActive(true);
                    sorterUI.SendMessage("UISetup", hit.transform.gameObject);
                }
                else
                    print("ui WIP");

                //else if (hit.transform.gameObject.tag == "Skin")
                //{
                //    skinUI.SetActive(true);
                //    skinUI.SendMessage("UISetup", hit.transform.gameObject);
                //}
                //else if (hit.transform.gameObject.tag == "Limb")
                //{
                //    limbUI.SetActive(true);
                //    limbUI.SendMessage("UISetup", hit.transform.gameObject);
                //}
                //else if (hit.transform.gameObject.tag == "Sensory")
                //{
                //    sensoryUI.SetActive(true);
                //    sensoryUI.SendMessage("UISetup", hit.transform.gameObject);
                //}
                //else if (hit.transform.gameObject.tag == "Splitter")
                //{
                //    splitterUI.SetActive(true);
                //    splitterUI.SendMessage("UISetup", hit.transform.gameObject);
                //}
            }
        }
        #endregion

        #region Economy UI - UpdateMoneyUI, UpdateExpenseUI
        [Header("Economy UI")]
        [SerializeField] Text[] m_Money;
        [SerializeField] Text m_Expense;

        // input parameter must be integer 
        public void UpdateMoneyUI(int _value)
        {
            string _valueString = _value.ToString();
            int i = 0;

            foreach (Text number in m_Money)
            {
                if (i < _valueString.Length)
                    number.text = "" + _valueString[i];
                else
                    number.text = "";

                i++;
            }

            //too many 0000 looks ugly...
            //m_Money.SetText(_value.ToString("000000000"));
        }

        
        // input parameter must be integer
        public void UpdateExpenseUI(int _value)
        {
            m_Expense.text = "$" + _value.ToString("0000") + "/day";
        }
        #endregion

        #region Resource UI - Reference of Resource List
        [Header("Resource UI")]
        public ResourceUIItem[] resourceUIItemList;
        #endregion

        #region Task UI - TaskUIUpdate
        [Header("Task UI")]
        [SerializeField] Image m_TaskFiller;
        [SerializeField] Image m_TaskMonster;
        [SerializeField] Text m_TaskText;
        
        public void UpdateTaskMonsterSprite(Sprite _monsterSprite)
        {
            m_TaskMonster.sprite = _monsterSprite;
        }

        public void UpdateTaskUI(float _current, float _goal)
        {
            m_TaskText.text = _current + "/" + _goal;
            m_TaskFiller.fillAmount = _current / _goal;
        }

        #endregion

        #region Time Control UI - GameSpeedAnim
        [Header("Time Control UI")]
        public Image dayClock;
        [SerializeField] GameObject speedEffect0;
        [SerializeField] GameObject speedEffect1;
        [SerializeField] GameObject speedEffect2;

        public void GameSpeedAnim(int speed)
        {
            if (speed == 0)
            {
                speedEffect0.SetActive(true);
                speedEffect1.SetActive(false);
                speedEffect2.SetActive(false);
            }
            else if (speed == 1)
            {
                speedEffect0.SetActive(false);
                speedEffect1.SetActive(true);
                speedEffect2.SetActive(false);
            }
            else
            {
                speedEffect0.SetActive(false);
                speedEffect1.SetActive(false);
                speedEffect2.SetActive(true);
            }
        }

        #endregion

        #region Summary UI
        [Header("Sumamry UI Elements")]
        public Text summaryUITitle;
        public Animator fadeMaskAnimController;
        public Animator summaryUIAnimController;
        public Text monsterProduced;
        public Text sales;
        public Text costs;
        public Text reward;
        public Text sum;
        public Text closingBalance;
        #endregion

        #region Help Screen UI
        [Header("Help Screen UI")]
        [SerializeField] GameObject m_HelpScreen;

        public void ToggleHelpScreen()
        {
            if (m_HelpScreen.activeSelf)
                m_HelpScreen.SetActive(false);
            else
                m_HelpScreen.SetActive(true);
        }
        #endregion

    }
}

