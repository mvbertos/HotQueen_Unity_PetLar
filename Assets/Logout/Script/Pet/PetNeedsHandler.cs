using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PetNeedsHandler : MonoBehaviour
{
    [SerializeField] private PetStatusManager _pet_status_manager;
    [SerializeField] private PetMovementation _pet_movementation;
    [SerializeField] private LayerMask food_layerMask;

    private void Start()
    {
        _pet_status_manager.OnHungerCallback += LookForFood;
    }

    private void OnDestroy()
    {
        _pet_status_manager.OnHungerCallback -= LookForFood;
    }

    private void LookForFood()
    {
        Transform target = ClosestFoodPot();

        //return if is null
        if (target)
        {
            _pet_movementation.SetNewTargetPosition(target.position);
            _pet_movementation.OnCloseToTargetCallback += usePot;
        }
    }

    private void usePot()
    {
        RaycastHit2D hit = Physics2D.CircleCast(this.transform.position, 2, this.transform.right, 1, food_layerMask);
        if (hit && hit.collider.TryGetComponent<FoodPot>(out FoodPot foodPot))
        {
            _pet_status_manager.status.Hunger += foodPot.UsePot();
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