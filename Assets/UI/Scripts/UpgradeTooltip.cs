namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class UpgradeTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject panel;
        
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            panel.SetActive(true);
            transform.SetAsLastSibling();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            panel.SetActive(false);
        }
    }
}