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
        _pet_status_manager.status.OnHungerCallback += LookForFood;
    }
    private void LookForFood()
    {
        Transform target = ClosestFoodPot();

        //return if is null
        if (!target) return;

        if (_pet_status_manager.status.Hunger < 50)
        {
            if (_pet_movementation)
            {
                _pet_movementation.targetPosition = target.position;
            }
        }

        _pet_movementation.OnCloseToTargetCallback += usePot;
    }

    private void usePot()
    {
        RaycastHit2D hit = Physics2D.CircleCast(this.transform.position, 2, this.transform.right, 1, food_layerMask);
        if (hit && hit.rigidbody.TryGetComponent<FoodPot>(out FoodPot foodPot))
        {
            foodPot.UsePot();
            _pet_status_manager.status.Hunger += 10;
        }
        _pet_movementation.OnCloseToTargetCallback -= usePot;
    }

    private Transform ClosestFoodPot()
    {
        FoodPot[] foodPot = FindObjectsOfType<FoodPot>();
        Transform closestpot = null;

        if (foodPot.Length > 0)
        {
            foreach (var item in foodPot)
            {
                if (item.food_amout > 0)
                {
                    if (closestpot == null)
                    {
                        closestpot = item.transform;
                    }
                    else if (Vector2.Distance(this.transform.position, item.transform.position) < Vector2.Distance(this.transform.position, closestpot.position))
                    {
                        closestpot = item.transform;
                    }
                }
            }
        }
        return closestpot;
    }
}
