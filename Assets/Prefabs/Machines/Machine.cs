

namespace MonsterFactory
{
    using System.Collections;
    using UnityEngine;

    public class Machine : MonoBehaviour
    {
        [Header("Machine Setting", order = 1)]
        [Tooltip("On/Off state of the machine.")]
        [SerializeField] protected bool m_State;
        public string machineName;
        public string description;
        public int buildCost;
        public float electriCostPerSecond;
        public float accumulatedElectricCost;

        private Transform m_SpawnPoint;
        private Animator[] m_Animators;
        private QuickOutline.Outline m_Outline;

        // TODO: NEED TO FIX THIS - CELL_MACHINE REFERENCE
        public GameObject cell;

        protected virtual void Awake()
        {
            m_Animators = GetComponentsInChildren<Animator>();
            m_SpawnPoint = transform.Find("SpawnPoint");
            m_Outline = GetComponent<QuickOutline.Outline>();
        }

        protected virtual void Start()
        {
            StartCoroutine(CR_AccumulateElectricityCost());
        }

        /// <summary>
        /// Increase accumulatedElectricCost per 1 second based on electriCostPerSecond;
        /// </summary>
        /// <returns></returns>
        IEnumerator CR_AccumulateElectricityCost()
        {
            while (true)
            {
                if (m_State)
                {
                    accumulatedElectricCost += electriCostPerSecond;
                    yield return new WaitForSeconds(1);
                }
                yield return null;
            }
        }

        /// <summary>
        /// Switch on/off machine.
        /// </summary>
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

        public bool GetState()
        {
            return m_State;
        }

        /// <summary>
        /// Find object from PoolManager, then "spawn" it at m_SpawnPoint;
        /// </summary>
        /// <param name="_poolTag">Used to find corresponding pool in Master Pool.</param>
        /// <param name="_masterPoolTag">Used to differentiate Master Pool.</param>
        protected void PoolObject(string _poolTag, string _masterPoolTag = "ItemPools")
        {
            GameObject _object = PoolManager.Instance.RequestAvailableObject(_poolTag, _masterPoolTag);
            _object.transform.position = m_SpawnPoint.position;
            _object.transform.rotation = m_SpawnPoint.rotation;
            _object.gameObject.SetActive(true);
        }

        /// <summary>
        /// Rotate Machine with _degree
        /// </summary>
        /// <param name="_degree">Passed down from Machine UI.</param>
        public void RotateMachine(int _degree)
        {
            transform.Rotate(new Vector3(0, _degree, 0));
        }
    }
}