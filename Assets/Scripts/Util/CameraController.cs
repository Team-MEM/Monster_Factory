using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(1f, 10f)]
    [SerializeField] float m_Sensitivity;

    private float m_HorizontalInput, m_VerticalInput;

    public float mouseSensitivity = 1.0f;
    private Vector3 lastPosition;


    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            transform.Translate(-delta.x * mouseSensitivity, 0, -delta.y * mouseSensitivity);
            lastPosition = Input.mousePosition;
        }

        m_HorizontalInput = Input.GetAxis("Horizontal");
        m_VerticalInput = Input.GetAxis("Vertical");

        
        if ((transform.position.x > 7 && m_VerticalInput < 0) || (transform.position.x < -2 && m_VerticalInput > 0))
        {
            m_VerticalInput = 0;
        }

        if ((transform.position.z > 7 && m_HorizontalInput > 0) || (transform.position.z < -7 && m_HorizontalInput < 0))
        {
            m_HorizontalInput = 0;
        }

        transform.position += new Vector3(-m_VerticalInput, 0, m_HorizontalInput);
    }
}
