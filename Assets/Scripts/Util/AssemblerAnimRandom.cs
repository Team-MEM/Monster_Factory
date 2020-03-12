using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblerAnimRandom : MonoBehaviour
{
    [SerializeField]
    Sprite[] randSelection;
    [SerializeField]
    SpriteRenderer sprite;

    public void RandomiseSprite()
    {
        sprite.sprite = randSelection[Random.Range(0, randSelection.Length)];
    }
}
