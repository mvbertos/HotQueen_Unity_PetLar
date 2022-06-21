using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private float sleepRegeneration = 10;
    [SerializeField] private Transform point;
    [SerializeField] private float useTime = 5;

    public void Use(Pet pet)
    {
        //pet stays still until petstatus sleep is full
        //pet is teleported to the point and AI movement is disabled
        pet.transform.position = point.position;
        pet.SpriteRenderer.sortingOrder += 1;
        pet.DisableAI();
        pet.DisableColliders();

        //create a timer to enable AI movement again after useTime
        TimerEvent.Create(() => { pet.EnableColliders(); pet.EnableAI(); pet.SpriteRenderer.sortingOrder -= 1; }, useTime);
    }

    public void SetTrigger(bool value)
    {
        //for each collider child of this gameobject
        //set isTrigger to true
        foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
        {
            collider.isTrigger = value;
        }
    }
}
