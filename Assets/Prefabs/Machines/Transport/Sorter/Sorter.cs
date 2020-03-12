using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterFactory
{
    public class Sorter : Machine
    {
        public Item m_Filter_Left;
        public Item m_Filter_Right;

        private bool m_Upgraded;
        private Transform m_ExitPoint;
        private Transform m_ExitPoint_Left;
        private Transform m_ExitPoint_Right;
        
        protected override void Awake()
        {
            base.Awake();
            m_ExitPoint = transform.Find("ExitPoint");
            m_ExitPoint_Left = transform.Find("ExitPoint_Left");
            m_ExitPoint_Right = transform.Find("ExitPoint_Right");
        }

        /// <summary>
        /// Set item to be sorted to the left.
        /// </summary>
        /// <param name="_item">Passed down from SorterUI.cs</param>
        public void SetLeftFilter(Item _item)
        {
                m_Filter_Left = _item;
        }

        /// <summary>
        /// Set item to be sorted to the right.
        /// </summary>
        /// <param name="_item">Passed down from SorterUI.cs</param>
        public void SetRightFilter(Item _item)
        {
                m_Filter_Right = _item;
        }

        private void OnTriggerStay(Collider other)
        {
            if (m_State)
            {
                if (m_Filter_Left != null && other.GetComponent<Item>().name == m_Filter_Left.name)
                    other.transform.position = Vector3.MoveTowards(other.transform.position, m_ExitPoint_Left.position, 0.02f);
                else if (m_Filter_Right != null && other.GetComponent<Item>().name == m_Filter_Right.name)
                    other.transform.position = Vector3.MoveTowards(other.transform.position, m_ExitPoint_Right.position, 0.02f);
                else
                    other.transform.position = Vector3.MoveTowards(other.transform.position, m_ExitPoint.transform.position, 0.02f);
            }
        }
    }
}
