namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;

    public class ResourceUIItem : MonoBehaviour /*, IPointerEnterHandler, IPointerExitHandler*/
    {
        public Image icon;
        public Text quantityText;
        public Text expireText;
        public Item item;

        private void Start()
        {
            if (item != null)
            {
                gameObject.name = item.name;

                if (item.sprite != null)
                    icon.sprite = item.sprite;
                else
                    Debug.LogWarning(item.name + " does not have a spirte!");
            }
            else
                gameObject.name = "No item assigned!!";
        }

        //void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        //{
        //    //TODO: get expiringQuantity
        //    expireText.gameObject.SetActive(true);
        //    quantityText.gameObject.SetActive(false);
        //}

        //void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        //{
        //    expireText.gameObject.SetActive(false);
        //    quantityText.gameObject.SetActive(true);
        //}

    }

}
