namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UnlockManager : MonoBehaviour
    {
        #region Singleton
        public static UnlockManager unlocks;

        private void Awake()
        {
            if (unlocks == null)
            {
                unlocks = this;
                
                FirstTimeSetup();
            }         
            else
                Destroy(this.gameObject);


        }
        #endregion

        #region Save/Load
        public void SaveGame()
        {
            SaveSystem.SaveUnlocks(this);
        }

        public void LoadGame()
        {
            UnlockData unlock = SaveSystem.LoadUnlocks();

            u_sorter = unlock.u_sorter;
            u_clayProducer = unlock.u_clayProducer;
            u_eyeMaker = unlock.u_eyeMaker;
            u_mouthMaker = unlock.u_mouthMaker;
            u_legMaker = unlock.u_legMaker;
            u_armMaker = unlock.u_armMaker;

            level = unlock.level;
        }
        #endregion

        #region Tech Tree
        //base machine unlock bools
        public bool u_sorter, u_clayProducer, u_eyeMaker, u_mouthMaker, u_legMaker, u_armMaker;

        public int level;

        public float exp1, exp2, exp3, exp4, exp5;
        #endregion

        void FirstTimeSetup()
        {
            u_sorter = u_clayProducer = u_eyeMaker = u_mouthMaker = u_legMaker = u_armMaker = false;

            level = 0;
        }


    }
}


