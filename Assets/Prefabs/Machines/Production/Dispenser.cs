namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Dispenser : Machine
    {
        [Range(5f, 10f)]
        [SerializeField] float m_ProductionRate;
        [SerializeField] string m_SelectedOutput;

        private new void Start()
        {
            base.Start();
            StartCoroutine(Spawning());
        }
    
        IEnumerator Spawning()
        {
            while (true)
            {
                while (m_State)
                {
                    PoolObject(m_SelectedOutput);

                    yield return new WaitForSeconds(m_ProductionRate);
                }

                yield return null;
            }
        }
    }
}