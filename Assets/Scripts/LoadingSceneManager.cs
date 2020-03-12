using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] string[] hints;
    public int sceneToLoad;
    
    void Start()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
        text.text = hints[Random.Range(0, hints.Length)];
    }

}
