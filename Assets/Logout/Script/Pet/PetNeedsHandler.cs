using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PetNeedsHandler : MonoBehaviour
{
    [SerializeField] private PetStatusManager petStatusManager;
    [SerializeField] private AIMovimentation petMovementation;
    [SerializeField] private LayerMask foodLayerMask;

    private void Start()
    {
        petStatusManager.OnHungerCallback += LookForFood;
    }

    private void OnDestroy()
    {
        petStatusManager.OnHungerCallback -= LookForFood;
    }

    private void LookForFood()
    {
        Transform target = ClosestFoodPot();

        //return if is null
        if (target)
        {
            petMovementation.GoInteract(target, usePot, foodLayerMask);
        }
    }

    private void usePot(RaycastHit2D hit)
    {
         if (hit.collider.TryGetComponent<FoodPot>(out FoodPot foodPot))
        {
            petStatusManager.status.Hunger += foodPot.UsePot();
        }
    }

    private void usePot()
    {
        RaycastHit2D hit = Physics2D.CircleCast(this.transform.position, 2, this.transform.right, 1, foodLayerMask);
        if (hit && hit.collider.TryGetComponent<FoodPot>(out FoodPot foodPot))
        {
            petStatusManager.status.Hunger += foodPot.UsePot();
        }
    }

    private Transform ClosestFoodPot()
    {
        List<FoodPot> foodPot_list = new List<FoodPot>(FindObjectsOfType<FoodPot>());

        //Transform closestpot = null;

        if (foodPot_list.Count > 0)
        {
            FoodPot pot = PetLarUtils.Complex<FoodPot>.ClosestObject(this.transform, foodPot_list.ToArray(), verification);
            return pot != null ? pot.transform : null;
        }
        return null;
    }

    private bool verification(FoodPot reference)
    {
        if (reference)
        {
            return !reference.IsEmpty();
        }
        return false;
    }
}