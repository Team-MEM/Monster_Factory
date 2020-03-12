using System.Collections.Generic;
using UnityEngine;

namespace MonsterFactory
{

    /*
     * How to make a Tower Defense Game (E06 BUILDING) - Unity Tutorial
     * By Brackeys
     * https://www.youtube.com/watch?v=t7GuWvP_IEQ&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=6
     * 
     */

    public class BuildManager : Singleton<BuildManager>
    {
        // TODO: Need a better name for this
        [System.Serializable]
        public struct Machine_Button
        {
            public string machineName;
            public Machine machine;
            public GameObject hologram;
        }
        
        [SerializeField] Vector3 m_RotationOffset;
        public Machine_Button machineToBuild;
        [SerializeField] Machine_Button[] m_Machines;
        public List<Machine> machinesInScene;

        private void Start()
        {
            for (int i = 0; i < m_Machines.Length; i++)
            {
                if (m_Machines[i].machine != null)
                    m_Machines[i].machineName = m_Machines[i].machine.name;
                else
                    m_Machines[i].machineName = "This machine is not assigned!";
            }
        }

        private void Update()
        {
            // Rotate Anti-Clockwise
            if (Input.GetKeyDown(KeyCode.Q) || Input.mouseScrollDelta.y > 0)
                m_RotationOffset -= new Vector3(0, 90, 0);

            // Rotate Clockewise
            if (Input.GetKeyDown(KeyCode.E) || Input.mouseScrollDelta.y < 0)
                m_RotationOffset += new Vector3(0, 90, 0);

            // Cancel Machine at hand
            if (Input.GetMouseButtonDown(1))
            {
                // Reset Hologram to origin (localPosition = Vector3.Zero)
                ShowHologram(Vector3.zero);
                machineToBuild.machine = null;
            }
        }

        /// <summary>
        /// Display corresponding hologram at _cell position.
        /// </summary>
        /// <param name="_cell"></param>
        public void ShowHologram(Vector3 _cell)
        {
            if (machineToBuild.machine != null)
            {
                if (_cell == Vector3.zero)
                    machineToBuild.hologram.transform.localPosition = _cell;
                else
                    machineToBuild.hologram.transform.position = _cell;
                machineToBuild.hologram.transform.localEulerAngles = m_RotationOffset;
            }
        }

        /// <summary>
        /// Placed corresponding machine at _cell, and create reference.
        /// </summary>
        public void BuildMachineAt(Transform _cell)
        {
            if (EconomyManager.Instance.CheckMoney(machineToBuild.machine.buildCost))
            {
                // Pool selected machine and store a reference of it.
                GameObject _selectedMachine = PoolManager.Instance.RequestAvailableObject(machineToBuild.machine.name, "MachinePools");
                machinesInScene.Add(_selectedMachine.GetComponent<Machine>());

                // TODO NEED TO FIX - CELL_MACHINE REFERENCE
                _selectedMachine.GetComponent<Machine>().cell = _cell.gameObject;
                _cell.GetComponent<Cell>().machine = _selectedMachine;

                // Moved pooled machine to selected position
                _selectedMachine.transform.position = _cell.position;
                _selectedMachine.transform.localEulerAngles = m_RotationOffset;
                _selectedMachine.gameObject.SetActive(true);
                AudioManager.Instance.machinePlacement.Play();

                // Play SFX when machine is built
                AudioManager.Instance.machinePlacement.Play();

                // Deduce the money spent from total money
                EconomyManager.Instance.SpendMoney(machineToBuild.machine.buildCost);

            }
            else
            {
                MainUIManager.Instance.ErrorMessage("Not Enough Money");
                Debug.LogWarning("Heeeeeeeeeeey, You don't have enough money!");
                // TODO: Display not enough money message!
            }
        }

        /// <summary>
        /// This function is used to build machine on load.
        /// </summary>
        /// <param name="_cell"></param>
        /// <param name="load"></param>
        public void BuildMachineAt(Transform _cell, bool load)
        {
            // Pool selected machine and store a reference of it.
            GameObject _selectedMachine = PoolManager.Instance.RequestAvailableObject(machineToBuild.machine.name, "MachinePools");

            // TODO NEED TO FIX - CELL_MACHINE REFERENCE
            _selectedMachine.GetComponent<Machine>().cell = _cell.gameObject;
            _cell.GetComponent<Cell>().machine = _selectedMachine;

            // Moved pooled machine to selected position
            _selectedMachine.transform.position = _cell.position;
            _selectedMachine.transform.localEulerAngles = m_RotationOffset;
            _selectedMachine.gameObject.SetActive(true);

            // Reset Hologram to origin (localPosition = Vector3.Zero)
            ShowHologram(Vector3.zero);

            machineToBuild.machine = null;
        }

        /// <summary>
        /// Switch machine to build based on the machine name passed in. 
        /// </summary>
        /// <param name="_machineName"></param>
        public void SwitchMachine(string _machineName)
        {
            MainUIManager.Instance.UIOff();
            machineToBuild.machine = null;
            foreach (Machine_Button _machine in m_Machines)
            {
                if (_machineName == _machine.machineName)
                {
                    machineToBuild = _machine;
                    break;
                }
            }

            if (machineToBuild.machine == null)
                Debug.LogError("This machine cannot be found! Check your machine names! (" + _machineName + ")");
        }
        
        public MachineInfo FetchMachineInfo(string _machineName)
        {
            MachineInfo _machineInfo = new MachineInfo();
            foreach (Machine_Button _machine in m_Machines)
            {
                if (_machine.machineName == _machineName)
                {
                    if (_machine.machine != null)
                    {
                        _machineInfo.MT_name = _machine.machine.machineName;
                        _machineInfo.MT_cost = _machine.machine.buildCost;
                        _machineInfo.MT_description = _machine.machine.description;
                    }
                    else
                        Debug.LogError(_machineName + " script does not exist!!");
                }
            }

            return _machineInfo;
        }

        /// <summary>
        /// Clear accumulated electricity cost of all machine in scene.
        /// </summary>
        public void ClearMachineAccumulatedElectricCost()
        {
            for (int i = 0; i < machinesInScene.Count; i++)
                machinesInScene[i].accumulatedElectricCost = 0;
        }
    }
}