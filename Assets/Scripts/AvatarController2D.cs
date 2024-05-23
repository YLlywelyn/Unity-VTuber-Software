using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarController2D: MonoBehaviour
{
    public Sprite normal;
    public Sprite blinking;
    public Sprite talking;
    public Sprite blinking_talking;

    private Image sprite;
    private bool isBlinking;
    private bool isTalking;

    void Start()
    {
        sprite = GetComponent<Image>();
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (isBlinking)
            sprite.sprite = isTalking ? blinking_talking : blinking;
        else
            sprite.sprite = isTalking ? talking : normal;
    }
}
