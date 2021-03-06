using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPot : MonoBehaviour
{
    private float current_food_amount = 0;
    [SerializeField] private float maxFood = 10;
    [SerializeField] private float hungerRegen = 10;
    [SerializeField] private Sprite emptyPot;
    [SerializeField] private Sprite fullPot;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    public void Fill()
    {
        ONG ong = GameObject.FindObjectOfType<ONG>();
        if (ong.Food >= maxFood)
        {
            current_food_amount = 10;
            ong.Food -= maxFood;
        }
        else if (ong.Food > 0 && ong.Food <= maxFood)
        {
            current_food_amount = ong.Food;
            ong.Food = 0;
        }
        else
        {
            Debug.LogWarning("Not enought food, pot failed to be filled");
            return;
        }

        UpdateSprite();
    }

    public bool IsEmpty()
    {
        return current_food_amount == 0;
    }
    public bool IsFull()
    {
        return current_food_amount == maxFood;
    }
    public float UsePot()
    {
        float regen = 0;

        if (!IsEmpty())
        {
            current_food_amount -= 1;
            regen = hungerRegen;
        }

        UpdateSprite();
        return regen;
    }

    private void UpdateSprite()
    {
        if (IsEmpty())
        {
            spriteRenderer.sprite = emptyPot;
        }
        else if (IsFull())
        {
            spriteRenderer.sprite = fullPot;
        }
        else
        {
            //_spriteRenderer.color = Color.yellow;
        }
    }
}
