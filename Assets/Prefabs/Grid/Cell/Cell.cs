namespace MonsterFactory
{
    using System.Collections;
    using UnityEngine;

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
        [SerializeField] Transform m_Arrow;

        [Space(10)]
        [Header("Gizmos Setting")]
        [SerializeField] bool m_ToggleGizmo;
        [SerializeField] Color m_ColorWhenFree;
        [SerializeField] Color m_ColorWhenOccupied;
        [SerializeField] float m_GizmosHeight;
        [SerializeField] float m_GizmosSize;

        private GameObject m_Machine;
        private Color m_OriginalColor;
        private MeshRenderer m_MeshRenderer;
        private Vector3 m_RotationOffset;

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
                m_Machine = _hit.collider.gameObject;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                m_RotationOffset += new Vector3(0, -90, 0);
                m_Arrow.Rotate(new Vector3(0,0,90));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                m_RotationOffset += new Vector3(0, 90, 0);
                m_Arrow.Rotate(new Vector3(0, 0, -90));
            }
        }
        
        private IEnumerator OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Machine>() != null)
                m_Machine = other.gameObject;
            else
            {
                yield return new WaitForSeconds(m_DestroyTimer);
                other.gameObject.SetActive(false);
            }
        }
        
        private void OnMouseEnter()
        {
            m_MeshRenderer.material.color = m_HighlighColor;
            m_Arrow.gameObject.SetActive(true);
        }

        private void OnMouseDown()
        {
            if (m_Machine != null)
            {
                Debug.Log("This tile has been taken!");
                return;
            }

            // Build Machine
            // TODO: Builder Error
            GameObject _machineToBuild = BuildManager.instance.GetMachineToBuild();

            if (_machineToBuild != null)
            {
                m_Machine = Instantiate(_machineToBuild, transform.position, transform.rotation * Quaternion.Euler(m_RotationOffset));
            }
        }

        private void OnMouseExit()
        {
            m_MeshRenderer.material.color = m_OriginalColor;
            m_Arrow.gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (m_ToggleGizmo)
            {
                if (m_Machine == null)
                    Gizmos.color = m_ColorWhenFree;
                else
                    Gizmos.color = m_ColorWhenOccupied;
                Gizmos.DrawWireSphere(transform.position + new Vector3(0, m_GizmosHeight, 0), m_GizmosSize);
            }
        }
    }
}