using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONG : MonoBehaviour
{
    [SerializeField] private float money = 1000;
    [SerializeField] private float food = 0;

    public float Food
    {
        get { return food; }
        set { food = value; }
    }

    public float Money
    {
        get { return money; }
        set { money = value; }
    }

}
