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
    [SerializeField] private LayerMask bathroomLayerMask;

    private void OnEnable()
    {
        statusManager.OnHungerCallback += LookForFood;
        statusManager.OnBathroomCallback += LookForBathroom;
    }

    private void OnDisable()
    {
        statusManager.OnHungerCallback -= LookForFood;
        statusManager.OnBathroomCallback -= LookForBathroom;
    }

    //bathroom 
    #region Bathroom
    private void LookForBathroom()
    {
        //look for the closest bathroom
        Transform bathroom = ClosestBathroom().transform;
        //if is a cat look for a sandbox
        //if is a dog look for a paper

        //if found 
        if (bathroom)
        {
            movementation.GoInteract(bathroom, UseBathroom, bathroomLayerMask);
        }
    }

    private void UseBathroom(RaycastHit2D hit)
    {
        if (hit.collider.TryGetComponent<Bathroom>(out Bathroom bathroom))
        {
            //maximize bathroom status of the pet
            pet.GetStatus(out PetStatus petStatus, out PetStatus maxStatus);
            petStatus.Bathroom = maxStatus.Bathroom;
            pet.SetStatus(petStatus);
            //use bathroom
            bathroom.Use();
        }
    }

    private Bathroom ClosestBathroom()
    {
        List<Bathroom> bathroom_list = new List<Bathroom>(FindObjectsOfType<Bathroom>());

        //Transform closestpot = null;

        if (bathroom_list.Count > 0)
        {
            Bathroom bathroom = PetLarUtils.Complex<Bathroom>.ClosestObject(this.transform, bathroom_list.ToArray());
            return bathroom != null ? bathroom : null;
        }
        return null;
    }

    #endregion

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

    //if pot used increment food status and reduce bathroom status
    private void usePot(RaycastHit2D hit)
    {
        if (hit.collider.TryGetComponent<FoodPot>(out FoodPot foodPot))
        {
            pet.GetStatus(out PetStatus status, out PetStatus maxStatus);

            //increment bathroom
            float feed = foodPot.UsePot();
            status.Hunger += feed;
            status.Bathroom -= feed;
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