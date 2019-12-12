namespace MonsterFactory
{
    using System.Collections;
    using UnityEngine;

    public class Producer : Machine
    {
        [SerializeField] Sprite m_Icon;
        [Range(5f, 10f)]
        [SerializeField] float m_ProductionRate;
        [SerializeField] string m_SelectedOutput;
        [SerializeField] string[] m_OutputOptions;

        IEnumerator ProcessMaterial()
        {
            yield return new WaitForSeconds(m_ProductionRate);
            m_Queue.x--;
            PoolObject(m_SelectedOutput);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Item>().id == 0)
            {
                other.gameObject.SetActive(false);

                if (m_Queue.x < m_Queue.y)
                    m_Queue.x++;

                if (m_Queue.x != 0)
                    StartCoroutine(ProcessMaterial());
            }
        }
    }
}