using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(1f, 10f)]
    [SerializeField] float m_Sensitivity;

    private float m_HorizontalInput, m_VerticalInput;

    void Update()
    {
        m_HorizontalInput = Input.GetAxis("Horizontal");
        m_VerticalInput = Input.GetAxis("Vertical");
        
        transform.position += new Vector3(-m_VerticalInput, 0, m_HorizontalInput);
    }
}
