namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Map : MonoBehaviour
    {
        public GameObject[] cells;

        #region Singleton
        public static Map instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                cells = new GameObject[transform.childCount];

                for (int i = 0; i < transform.childCount; i++)
                {
                    cells[i] = transform.GetChild(i).gameObject;
                }
            }        
            else
                Destroy(this.gameObject);
        }
        #endregion

        #region Save/Load
        public void SaveGame()
        {
            SaveSystem.SaveMap(this);
        }

        public void LoadGame()
        {
            MapData data = SaveSystem.LoadMap();
            int i = 0;

            foreach (GameObject cell in cells)
            {
                if (data.hasMachine[i] && data.machineName[i] != "Assembler")
                {
                    BuildManager.Instance.SwitchMachine(data.machineName[i]);
                    BuildManager.Instance.BuildMachineAt(cell.transform, true);
                    Transform machine = cell.GetComponent<Cell>().machine.transform;
                    machine.localEulerAngles = new Vector3(machine.localEulerAngles.x, data.machineRotation[i], machine.localEulerAngles.z);
                }

                i++;
            }
        }
        #endregion

    }
}


