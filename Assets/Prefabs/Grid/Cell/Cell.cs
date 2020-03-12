namespace MonsterFactory
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.EventSystems;

    /*
     * How to make a Tower Defense Game (E06 BUILDING) - Unity Tutorial
     * By Brackeys
     * https://www.youtube.com/watch?v=t7GuWvP_IEQ&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=6
     * 
     */

    public class Cell : MonoBehaviour
    {
        [SerializeField] float m_DestroyTimer;
        [SerializeField] Color m_HighlighColor;

        [Space(10)]
        [Header("Gizmos Setting")]
        [SerializeField] bool m_ToggleGizmo;
        [SerializeField] Color m_ColorWhenFree;
        [SerializeField] Color m_ColorWhenOccupied;
        [SerializeField] float m_GizmosHeight;
        [SerializeField] float m_GizmosSize;

        private Color m_OriginalColor;
        private MeshRenderer m_MeshRenderer;

        // TODO: RIIIIP I NEED TO FIX THIS AS WELL - CELL_MACHINE REFERENCE
        public GameObject machine;

        private void Start()
        {
            m_MeshRenderer = GetComponentInChildren<MeshRenderer>();
            m_OriginalColor = m_MeshRenderer.material.color;
            Init();
        }

        public LayerMask layerMask;

        // Check if Machine already exist on top of cell
        private void Init()
        {
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, transform.up, out _hit, 2f, layerMask))
                machine = _hit.collider.gameObject;
        }
        
        private IEnumerator OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Machine>() != null)
                machine = other.gameObject;
            else
            {
                other.GetComponent<Item>().isOnFloor = true;
                yield return new WaitForSeconds(0);
                other.gameObject.SetActive(false);
            }
        }


        // TODO: OMG THIS MONSTROSITY CODE
        private void OnMouseOver()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && BuildManager.Instance.machineToBuild.machine != null)
            {
                BuildManager.Instance.ShowHologram(transform.position);
                m_MeshRenderer.material.color = m_HighlighColor;
            }
            else
            {
                m_MeshRenderer.material.color = m_OriginalColor;
                BuildManager.Instance.ShowHologram(Vector3.zero);
            }

        }

        private void OnMouseDown()
        {
            if (!MainUIManager.Instance.IsPointerOverUIObject() && BuildManager.Instance.machineToBuild.machine != null)
            {
                if (machine != null)
                {
                    Debug.Log("This tile has been taken!");
                    return;
                }

                // Build Machine
                BuildManager.Instance.BuildMachineAt(transform);
                BuildManager.Instance.ShowHologram(Vector3.zero);
            }
        }

        private void OnMouseExit()
        {
            m_MeshRenderer.material.color = m_OriginalColor;
            BuildManager.Instance.ShowHologram(Vector3.zero);
        }

        private void OnDrawGizmos()
        {
            if (m_ToggleGizmo)
            {
                if (machine == null)
                    Gizmos.color = m_ColorWhenFree;
                else
                    Gizmos.color = m_ColorWhenOccupied;
                Gizmos.DrawWireSphere(transform.position + new Vector3(0, m_GizmosHeight, 0), m_GizmosSize);
            }
        }
    }
}