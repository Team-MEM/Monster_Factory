namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class MainUIManager : MonoBehaviour
    {

        [SerializeField] GameObject TransportMenu, ProductionMenu, ProcessingMenu, RecipeMenu, TechMenu;

        [SerializeField] protected GameObject MachineTooltipBox;

        [SerializeField] protected Text UIM_Name, UIM_Cost, UIM_Description;

        [SerializeField] Animator MenuAnim;

        bool menuUp;


        int page;

        void Start()
        {
            page = 0;
            menuUp = false;
            StartCoroutine("CheckForMachineSelect");
        }
        
        //Turns UI off
        void UIOff()
        {
            RecipeMenu.SetActive(false);
            TechMenu.SetActive(false);
            TransportMenu.SetActive(false);
            ProductionMenu.SetActive(false);
            ProcessingMenu.SetActive(false);
            MachineUIOff();

            //if escape menu is active deactivate
            if (menuUp)
                EscMenuToggle();
        }


        public void ToggleMenu(GameObject _gameObject)
        {
            if (_gameObject.activeSelf)
                _gameObject.SetActive(false);
            else
            {
                UIOff();
                _gameObject.SetActive(true);
            }
        }

        public void EscMenuToggle()
        {
            if (!menuUp)
                UIOff();
            menuUp = !menuUp;

            MenuAnim.SetBool("isUp", menuUp);
        }

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
            page = 0;

            UpdatePage();

        }

        public void NextPage()
        {
            page++;

            if (page > 11)
            {
                page = 0;
            }

            UpdatePage();
        }

        public void PreviousPage()
        {
            page--;

            if (page < 0)
            {
                page = 11;
            }

            UpdatePage();
        }

        public void JumpToPage(int pageNo)
        {
            page = pageNo;

            UpdatePage();
        }

        //Machine UI and raycast

        [SerializeField] LayerMask machineLayer;

        [SerializeField] Transform MachineUIHolder;
        //machine specific UIs
        [SerializeField] GameObject AssemblerUI, GenericUI;

        Ray castPoint;
        RaycastHit hit;

        void MachineUIOff()
        {
            foreach (Transform child in MachineUIHolder)
                child.gameObject.SetActive(false);
                
        }

        //checks if mouse is over ui object and returns a bool
        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        

        //coroutine to control machine UIs.
        IEnumerator CheckForMachineSelect()
        {
            while (true)
            {

                if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
                {
                    MachineUIOff();

                    castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, machineLayer))
                    {
                        if (hit.transform.gameObject.tag == "Assembler")
                        {
                            print("assembler Clicked");
                            AssemblerUI.SetActive(true);
                            AssemblerUI.SendMessage("AssemblerUISetup", hit.transform.gameObject);
                        }
                        else if (hit.transform.gameObject.tag == "GenericMachine")
                        {
                            print(hit.transform.gameObject.name + " Clicked");
                            GenericUI.SetActive(true);
                        }
                        else
                        {
                            print("ui WIP");
                        }
                    }
                            
                }

                yield return null;
            }
            
        }

    }
}

