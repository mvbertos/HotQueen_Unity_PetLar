using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//food it´s can be only bought by ONG
//when bought it adds to the ONG´s food
//when ONG´s food is full it can´t be bought anymore
//food is consumed by ONG every time it´s added to the foodpot
public class Food : MonoBehaviour
{
    public enum Size
    {
        Small,
        Medium,
        Large,        
    }

    [SerializeField] private Size size = 0;
    private float foodAmount = 0;

    private void Start() {
        InitFoodAmount();
    }

    private void InitFoodAmount()
    {
        switch (size)
        {
            case Size.Small:
                foodAmount = 30;
                break;
            case Size.Medium:
                foodAmount = 60;
                break;
            case Size.Large:
                foodAmount = 100;
                break;
        }
    }
}
