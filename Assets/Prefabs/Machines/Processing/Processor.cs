namespace MonsterFactory
{
    using System.Collections;
    using UnityEngine;

    public class Processor : Machine
    {
        [SerializeField] Item m_RequiredInput;
        [SerializeField] Item m_ProcessedOutput;
        
        private void OnTriggerEnter(Collider _other)
        {
            if (m_State)
            {
                if (_other.GetComponent<Item>().name == m_RequiredInput.name)
                {
                    _other.gameObject.SetActive(false);
                    PoolObject(m_ProcessedOutput.name);
                }
            }
        }
    }
}