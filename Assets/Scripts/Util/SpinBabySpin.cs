using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBabySpin : MonoBehaviour
{
    [SerializeField]
    float speed;
    void OnEnable()
    {
        StartCoroutine("Spin");
    }

    IEnumerator Spin()
    {
        while (true)
        {
            transform.Rotate(0, 0, speed);
            yield return null;
        } 
    }
}
