using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ONGData
{
    private float money;
    private float maxFood;
    private Pet[] pets; //pets rescued by the ONG

    public ONGData(float money, float maxFood, float food, Pet[] pets)
    {
        this.money = money;
        this.maxFood = maxFood;
        this.pets = pets;
    }
    public ONGData(ONG ong)
    {
        // this.money = ong.Money;
        // this.maxFood = ong.maxFoodValue;
        // this.pets = ong.PetDatas.ToArray();
    }
}
