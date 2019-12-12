namespace MonsterFactory
{
    using UnityEngine;

    public class MoveItem : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, 0.02f);
        }
    }
}