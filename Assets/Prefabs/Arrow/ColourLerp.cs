using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourLerp : MonoBehaviour
{
    Image image;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    void OnEnable()
    {
        StartCoroutine("ColourChange");
    }

    IEnumerator ColourChange()
    {
        float t = 0;
        while (true)
        {
            while (t < 1)
            {
                image.color = Color.Lerp(Color.red, Color.grey, t);
                t += Time.deltaTime;
                yield return null;
            }

            t = 0;

            while (t < 1)
            {
                image.color = Color.Lerp(Color.grey, Color.red, t);
                t += Time.deltaTime;
                yield return null;
            }

            t = 0;

            yield return null;
        }
    }
}
