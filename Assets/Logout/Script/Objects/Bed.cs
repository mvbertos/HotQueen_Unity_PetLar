using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private float sleepRegeneration = 10;
    private Pet pet;
    public float Use()
    {
        //pet stays still until petstatus sleep is full

        return sleepRegeneration;
    }
}
