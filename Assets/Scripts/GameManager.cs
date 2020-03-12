namespace MonsterFactory
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameManager : Singleton<GameManager>
    {
        public int dayCounter;
        [SerializeField] float m_DayLength;
        [Range(0f, 300f)]
        [SerializeField] float m_TimeLeft;

        void Start()
        {
            StartCoroutine(CR_CountdownDay());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
                MainUIManager.Instance.ToggleHelpScreen();
        }

        #region Game Round Control
        /// <summary>
        /// When a day ended, it will run EconomyManager.CalculateRevenue();
        /// </summary>
        /// <returns></returns>
        IEnumerator CR_CountdownDay()
        {
            m_TimeLeft = m_DayLength;

            while (m_TimeLeft >= 0)
            {
                m_TimeLeft -= Time.deltaTime;
                MainUIManager.Instance.dayClock.fillAmount = (m_DayLength - m_TimeLeft) / m_DayLength;
                yield return null;
            }

            // Start FadeMask & SummaryUI fade in sequence.
            MainUIManager.Instance.fadeMaskAnimController.SetBool("dayEnded", true);
            MainUIManager.Instance.summaryUIAnimController.SetBool("dayEnded", true);

            // Change title so it matches the day, and ask EconomyManager to do the math.
            MainUIManager.Instance.summaryUITitle.text = "Summary - Day " + (dayCounter + 1);
            EconomyManager.Instance.CalculateRevenue();

            // Auto save game
            SaveGame();

            yield return new WaitForSeconds(2);
            Time.timeScale = 0;
        }
        
        /// <summary>
        /// Reset items & monsters in the scene, clear Machine accumulatedElectricCost;
        /// </summary>
        public void NextDay()
        {
            dayCounter++;

            // Disable all items in the scene
            PoolManager.Instance.ClearSceneItems();

            // Clear Machine accumulatedElectricityCost;
            BuildManager.Instance.ClearMachineAccumulatedElectricCost();
            
            // Start FadeMask & SummaryUI fade out sequence.
            MainUIManager.Instance.fadeMaskAnimController.SetBool("dayEnded", false);
            MainUIManager.Instance.summaryUIAnimController.SetBool("dayEnded", false);

            // Get new task
            TaskManager.Instance.SetupTask();

            // Set game speed back to normal
            MainUIManager.Instance.UpdateMoneyUI(EconomyManager.Instance.totalMoney);
            Time.timeScale = 1;
            StartCoroutine(CR_CountdownDay());
        }
        #endregion

        #region Game Speed Control
        public void SetGameSpeed(float _multiplier)
        {
            Time.timeScale = 1 * _multiplier;
        }
        #endregion
        
        #region Game Menu Control
        public void SaveGame()
        {
            Map.instance.SaveGame();
            UnlockManager.unlocks.SaveGame();
            EconomyManager.Instance.SaveGame();
            ResourceManager.Instance.SaveGame();
        }

        public void LoadGame()
        {
            PoolManager.Instance.MassDisable();
            Map.instance.LoadGame();

            //TODO:check unlockManager Save works
            
            UnlockManager.unlocks.LoadGame();
            EconomyManager.Instance.LoadGame();
            ResourceManager.Instance.LoadGame();
        }

        public void ToMenu()
        {
            SceneManager.LoadScene(5);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
        #endregion
    }
}