namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class MapData
    {
        public bool[] hasMachine;
        public string[] machineName;
        public float[] machineRotation;

        public MapData(Map map)
        {
            hasMachine = new bool[map.cells.Length];
            machineName = new string[map.cells.Length];
            machineRotation = new float[map.cells.Length];
            int i = 0;

            foreach (GameObject cell in map.cells)
            {
                if (cell.GetComponent<Cell>().machine != null)
                {
                    hasMachine[i] = true;
                }
                else
                {
                    hasMachine[i] = false;
                }

                if (hasMachine[i])
                {
                    machineName[i] = cell.GetComponent<Cell>().machine.GetComponent<Machine>().machineName;
                    machineRotation[i] = cell.GetComponent<Cell>().machine.GetComponent<Machine>().transform.localEulerAngles.y;
                }
                else
                {
                    machineName[i] = null;
                    machineRotation[i] = 0;
                }

                i++;
            }

            
        }
    }

}

