using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterFactory
{
    public class SellingArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Item>() != null)
            {
                EconomyManager.Instance.AddToSellingList(other.GetComponent<Item>());
                if (TaskManager.Instance.currentTask.item.name == other.GetComponent<Item>().name)
                    TaskManager.Instance.ProgressCurrentTask();
            }
        } 
    }
}
