namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Assembler : Machine
    {
        public ParticleSystem m_ProduceVFX;

        /// <summary>
        /// Receive a recipe from AssemblerUI, then send the recipe to ResourceManager to check if player have required resource to build monster.
        /// <para>Returns a bool to AssemblerUI.SendOrder to load either success/failed message.</para>
        /// </summary>
        /// <param name="_recipe">Passed down from AssemblerUI.SendOrder(Recipe)</param>
        public bool ProduceMonster(Recipe _recipe)
        {
            if (ResourceManager.Instance.CheckIfEnoughResource(_recipe))
            {
                PoolObject(_recipe.name, "MonsterPools");
                m_ProduceVFX.Play();
                return true;
            }
            else
                return false;
            
        }
      
    }
}