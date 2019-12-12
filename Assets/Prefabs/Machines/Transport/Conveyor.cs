namespace MonsterFactory
{
    using UnityEngine;

    public class Conveyor : Machine
    {
        [SerializeField] Transform m_Target;
        [Range(1f,3f)]
        [SerializeField] float m_Speed;
    
        private void OnTriggerStay(Collider other)
        {
            if (m_State)
                other.transform.position = Vector3.MoveTowards(other.transform.position, m_Target.transform.position, m_Speed / 100);
        }
    }
}
