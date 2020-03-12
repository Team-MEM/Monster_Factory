using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MonsterFactory
{

    public class CostView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Recipe m_Recipe;
        [SerializeField] Image m_Sprite;
        [SerializeField] Item m_Monster;

        private Animator m_Animator;
        
        void Start()
        {
            gameObject.name = m_Recipe.name;
            m_Sprite.sprite = m_Recipe.recipeSprite;
            m_Animator = GetComponent<Animator>();
        }
        
        /// <summary>
        /// Start the delay animation for Recipe Buttons
        /// </summary>
        public void StartDelay()
        {
            m_Animator.SetTrigger("Delay");
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            AssemblerUI.instance.CostViewOn(m_Recipe, m_Monster.value);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            AssemblerUI.instance.CostViewOff();
        }
    }
}


