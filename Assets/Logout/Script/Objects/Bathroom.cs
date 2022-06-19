using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathroom : MonoBehaviour
{
    [SerializeField] private Sprite dirty;
    [SerializeField] private Sprite clean;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject point;
    [SerializeField] private float useTime = 2;
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

    public void Use(Pet pet)
    {
        uses--;

        //pet is teleported to the point and AI movement is disabled
        pet.transform.position = point.transform.position;
        pet.SpriteRenderer.sortingOrder += 1;
        pet.DisableAI();
        //create a timer to enable AI movement again after useTime
        TimerEvent.Create(() => { pet.EnableAI(); pet.SpriteRenderer.sortingOrder -= 1; }, useTime);

        UpdateSprite();
    }

    public void Clean()
    {
        uses = maxUses;
        UpdateSprite();
    }
}
