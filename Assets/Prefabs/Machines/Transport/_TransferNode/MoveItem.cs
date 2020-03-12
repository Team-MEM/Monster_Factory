namespace MonsterFactory
{
    using UnityEngine;

    public class MoveItem : MonoBehaviour
    {
        [Range(0.01f, 0.03f)]
        [SerializeField] float m_Speed = 0.02f;
    
        private void OnTriggerStay(Collider other)
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, m_Speed);
        }
    }
}