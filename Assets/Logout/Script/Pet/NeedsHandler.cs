using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NeedsHandler : MonoBehaviour
{

    [SerializeField] private Pet pet;
    [SerializeField] private StatusManager statusManager;
    [SerializeField] private AIMovimentation movementation;
    [SerializeField] private LayerMask foodLayerMask;

    private void OnEnable()
    {
        statusManager.OnHungerCallback += LookForFood;
    }

    private void OnDisable()
    {
        statusManager.OnHungerCallback -= LookForFood;
    }

    //Hunger
    #region Hunger
    private void LookForFood()
    {
        Transform target = ClosestFoodPot();

        //return if is null
        if (target)
        {
            movementation.GoInteract(target, usePot, foodLayerMask);
        }
    }

    private void usePot(RaycastHit2D hit)
    {
        if (hit.collider.TryGetComponent<FoodPot>(out FoodPot foodPot))
        {
            pet.GetStatus(out PetStatus status, out PetStatus maxStatus);
            status.Hunger += foodPot.UsePot();
            pet.SetStatus(status);
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
    #endregion
}