using System.Collections;
using UnityEngine;

namespace MonsterFactory
{

    public class Producer : Machine
    {
        [SerializeField] float m_ProductionRate = 5f;
        [SerializeField] Item m_SelectedOutput;

        private void OnEnable()
        {
            StartCoroutine(CR_Spawning());
        }


        IEnumerator CR_Spawning()
        {
            while (true)
            {
                while (m_State)
                {
                    yield return new WaitForSeconds(m_ProductionRate);
                    PoolObject(m_SelectedOutput.name);
                }
                yield return null;
            }
        }
    }
}