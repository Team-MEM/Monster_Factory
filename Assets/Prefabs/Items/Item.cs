namespace MonsterFactory
{
    using UnityEngine;

    public class Item : MonoBehaviour
    {
        public int id;
        public new string name;
        public Sprite sprite;
        public int value;

        public bool isOnFloor = false;

        void OnMouseDown()
        {
            if (!MainUIManager.Instance.IsPointerOverUIObject() &&  isOnFloor)
                gameObject.SetActive(false);
        }
    }

}
