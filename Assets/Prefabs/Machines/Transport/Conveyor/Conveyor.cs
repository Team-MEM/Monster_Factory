namespace MonsterFactory
{
    using UnityEngine;

    public class Conveyor : Machine
    {
        [Range(1f,3f)]
        [SerializeField] float m_Speed;

        private Transform m_ExitPoint;
    
        protected override void Awake()
        {
            base.Awake();
            m_ExitPoint = transform.Find("ExitPoint");
        }

        private void OnTriggerStay(Collider other)
        {
            if (m_State)
                other.transform.position = Vector3.MoveTowards(other.transform.position, m_ExitPoint.position, m_Speed / 100);
        }
    }
}
