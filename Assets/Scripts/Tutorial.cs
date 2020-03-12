namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class Tutorial : MonoBehaviour
    {
        [SerializeField]
        GameObject[] steps;
        int counter;

        void Start()
        {
            counter = 0;
            UpdateTutorial();
        }

        public void NextStep()
        {
            counter++;
            DisableSteps();
            UpdateTutorial();
        }

        public void End()
        {
            SceneManager.LoadScene(5);
        }

        void DisableSteps()
        {
            foreach (GameObject step in steps)
            {
                step.SetActive(false);
            }
        }

        void UpdateTutorial()
        {
            steps[counter].SetActive(true);

            switch (counter)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    Step2();
                    break;
                case 3:
                    Step3();
                    break;
                case 4:
                    break;
                case 5:
                    Step5();
                    break;
                case 6:
                    Step6();
                    break;
                case 7:
                    Step7();
                    break;
                case 8:
                    Step8();
                    break;
                case 9:
                    Step9();
                    break;
                case 10:
                    Step10();
                    break;
                case 11:
                    Step11();
                    break;
                case 12:
                    Step12();
                    break;
                case 13:
                    Step13();
                    break;
                case 14:
                    break;
                case 15:
                    Step15();
                    break;
                case 16:
                    break;
                case 17:
                    break;
                case 18:
                    Step18();
                    break;
                case 19:
                    Step19();
                    break;
                case 20:
                    Step20();
                    break;
                case 21:
                    break;
                case 22:
                    break;
                case 23:
                    break;
                case 24:
                    break;
                case 25:
                    break;
                case 26:
                    break;
                default:
                    break;

            }
        }

        public GameObject assemblerUI;

        void Step2()
        {
            assemblerUI.SetActive(true);
        }

        public GameObject monster;

        void Step3()
        {
            assemblerUI.SetActive(false);
            monster.SetActive(true);
            AudioManager.Instance.monsterCreated.Play();
        }

        public GameObject TransportTab;

        void Step5()
        {
            TransportTab.SetActive(true);
        }

        void Step6()
        {
            TransportTab.SetActive(false);
        }

        public GameObject storage;

        void Step7()
        {
            storage.SetActive(true);
            AudioManager.Instance.machinePlacement.Play();
        }

        void Step8()
        {
            TransportTab.SetActive(true);
        }

        void Step9()
        {
            TransportTab.SetActive(false);
        }

        public GameObject conveyor;

        void Step10()
        {
            conveyor.SetActive(true);
            AudioManager.Instance.machinePlacement.Play();
        }

        public GameObject ProducerTab;

        void Step11()
        {
            ProducerTab.SetActive(true);
        }

        void Step12()
        {
            ProducerTab.SetActive(false);
        }

        public GameObject rawProducer;

        void Step13()
        {
            rawProducer.SetActive(true);
            AudioManager.Instance.machinePlacement.Play();
        }

        public GameObject machineUI;

        void Step15()
        {
            machineUI.SetActive(true);
        }

        void Step18()
        {
            machineUI.SetActive(false);
        }

        public GameObject costView;

        void Step19()
        {
            assemblerUI.SetActive(true);
            costView.SetActive(true);
        }

        void Step20()
        {
            assemblerUI.SetActive(false);
            costView.SetActive(false);
        }

    }

}

