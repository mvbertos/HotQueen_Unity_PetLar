using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPot : MonoBehaviour
{
    private float current_food_amount = 0;
    [SerializeField] private float maxFood = 10;
    [SerializeField] private float hungerRegen = 10;

    public void FillPot()
    {
        if (GameManager._ong.food >= maxFood)
        {
            current_food_amount = 10;
            GameManager._ong.food -= maxFood;
        }
        else if (GameManager._ong.food > 0 && GameManager._ong.food <= maxFood)
        {
            current_food_amount = GameManager._ong.food;
            GameManager._ong.food = 0;
        }
        else
        {
            Debug.LogWarning("Not enought food, pot failed to be filled");
            return;
        }

        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
        renderer.color = Color.green;
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
        SpriteRenderer _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if (IsEmpty())
        {
            _spriteRenderer.color = Color.red;
        }
        else if (IsFull())
        {
            _spriteRenderer.color = Color.green;
        }
        else
        {
            _spriteRenderer.color = Color.yellow;
        }
    }
}
