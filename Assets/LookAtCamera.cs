using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Camera cam;

    void OnEnable()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        StartCoroutine("Look");
    }

    IEnumerator Look()
    {
        while (true)
        {
            transform.LookAt(cam.transform.position);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
