 namespace MonsterFactory
{
    using UnityEngine;

    /*
     * How to make a Tower Defense Game (E06 BUILDING) - Unity Tutorial
     * By Brackeys
     * https://www.youtube.com/watch?v=t7GuWvP_IEQ&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=6
     * 
     */

    public class BuildManager : MonoBehaviour
    {
        public static BuildManager instance;
        
        // TODO: Need a better name for this
        [System.Serializable]
        struct Machine_Button
        {
            public string machineName;
            public GameObject machinePrefab;
        }

        [SerializeField] GameObject m_SelectedMachines;
        [SerializeField] Machine_Button[] m_Machines;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
    
        public GameObject GetMachineToBuild()
        {
            return m_SelectedMachines;
        }

        public void SwitchMachine(string _machineName)
        {
            m_SelectedMachines = null;
            foreach (Machine_Button _machine in m_Machines)
            {
                if (_machineName == _machine.machineName)
                {
                    m_SelectedMachines = _machine.machinePrefab;
                    break;
                }
            }

            if (m_SelectedMachines == null)
                Debug.LogError("This machine cannot be found! Check your machine names! (" + _machineName + ")");
        }
        
        public MachineInfo FetchMachineInfo(string _machineName)
        {
            MachineInfo _machineInfo = new MachineInfo();
            foreach (Machine_Button _machine in m_Machines)
            {
                if (_machine.machineName == _machineName)
                {
                    Machine _machineScript = _machine.machinePrefab.GetComponent<Machine>();
                    _machineInfo.MT_name = _machineScript.machineName;
                    _machineInfo.MT_cost = _machineScript.costToBuild;
                    _machineInfo.MT_description = _machineScript.description;
                    Debug.Log(_machineInfo.MT_description);
                }
            }

            return _machineInfo;
        }

        void OnGUI()
        {
            GUI.TextArea(new Rect(10, 10, 150, 20), (1f / Time.deltaTime).ToString("000"));
        }

    }
}