namespace MonsterFactory
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Machine : MonoBehaviour
    {
        [SerializeField] Transform m_SpawnPoint;
        private Animator[] m_Animators;

        [Header("Machine Setting", order = 1)]
        [Tooltip("On/Off state of the machine.")]
        [SerializeField] protected bool m_State;
        public string machineName;
        public int costToBuild;
        public string description;
        [SerializeField] protected float m_RuntimeCost;
        [Tooltip("x: Current queue size / y: Queue limit")]
        [SerializeField] protected Vector2 m_Queue;

        protected void Start()
        {
            m_Animators = GetComponentsInChildren<Animator>();
            m_SpawnPoint = transform.GetChild(1);
        }
        
        public void ToggleState()
        {
            m_State = !m_State;

            if (m_Animators != null)
            {
                foreach (Animator _anim in m_Animators)
                {
                    if (m_State)
                        _anim.SetFloat("speedMultiplier", 1);
                    else
                        _anim.SetFloat("speedMultiplier", 0);
                }
            }
        }
        
        protected void PoolObject(string _poolTag)
        {
            GameObject _object = PoolManager.instance.RequestAvailableObject(_poolTag);
            _object.transform.position = m_SpawnPoint.position;
            _object.transform.rotation = m_SpawnPoint.rotation;
            _object.gameObject.SetActive(true);
        }

        private void OnMouseOver()
        {
            Debug.Log("You just hover over " + gameObject.name);
        }


    }
}