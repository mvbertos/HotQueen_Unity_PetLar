using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private float sleepRegeneration = 10;
    public float Use()
    {
        return sleepRegeneration;
    }
}
