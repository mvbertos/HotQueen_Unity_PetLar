using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPot : MonoBehaviour
{
    public float food_amout { private set; get; }


    public void FillPot()
    {
        if (GameManager._ong.food >= 10)
        {
            food_amout = 10;
            GameManager._ong.food -= 10;
        }
        else if (GameManager._ong.food > 0 && GameManager._ong.food <= 10)
        {
            food_amout = GameManager._ong.food;
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

    public void UsePot()
    {
        food_amout -= 1;
    }
}
