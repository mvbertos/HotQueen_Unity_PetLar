using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    [SerializeField] private float resistance;
    [SerializeField] private float funRegeneration;
    private float maxResistance;

    private void Awake()
    {
        maxResistance = resistance;
    }

    public float Use()
    {
        //add force to the ball
        return  funRegeneration;
    }
}
