using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathroom : MonoBehaviour
{
    [SerializeField] private Sprite dirty;
    [SerializeField] private Sprite clean;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int uses; //how many times must be used to get dirty
    private int maxUses;

    private void Awake()
    {
        UpdateSprite();
        maxUses = uses;
    }

    private void UpdateSprite()
    {
        if (uses <= 0)
        {
            spriteRenderer.sprite = dirty;
        }
        else
        {
            spriteRenderer.sprite = clean;
        }
    }

    public void Use()
    {
        //bathroom
        uses -= 1;
        UpdateSprite();
    }
    
    public void Clean()
    {
        uses = maxUses;
        UpdateSprite();
    }
}
