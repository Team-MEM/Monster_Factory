namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class UnlockData
    {
        //base machine unlock bools
        public bool u_sorter, u_clayProducer, u_eyeMaker, u_mouthMaker, u_legMaker, u_armMaker;
        
        public int level;

        public UnlockData(UnlockManager unlock)
        {
            u_sorter = unlock.u_sorter;
            u_clayProducer = unlock.u_clayProducer;
            u_eyeMaker = unlock.u_eyeMaker;
            u_mouthMaker = unlock.u_mouthMaker;
            u_legMaker = unlock.u_legMaker;
            u_armMaker = unlock.u_armMaker;

            level = unlock.level;
        }
    }
}


