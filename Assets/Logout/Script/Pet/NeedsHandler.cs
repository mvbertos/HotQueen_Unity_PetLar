using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class NeedsHandler : MonoBehaviour
{

    [SerializeField] private Pet pet;
    [SerializeField] private StatusManager statusManager;
    [SerializeField] private AIMovimentation movementation;
    [SerializeField] private LayerMask interactLayer;
    private Dictionary<String, Coroutine> currentCoroutines = new Dictionary<string, Coroutine>();

    private void OnEnable()
    {
        statusManager.OnHungerCallback += LookForFood;
        statusManager.OnBathroomCallback += LookForBathroom;
        statusManager.OnSleepCallback += LookForBed;
        statusManager.OnFunCallback += LookForToy;
    }

    private void OnDisable()
    {
        statusManager.OnHungerCallback -= LookForFood;
        statusManager.OnBathroomCallback -= LookForBathroom;
        statusManager.OnSleepCallback -= LookForBed;
        statusManager.OnFunCallback -= LookForToy;
    }

    //Sleep
    #region Sleep
    private void LookForBed()
    {
        Bed target = ClosestObject<Bed>();

        if (target && !currentCoroutines.ContainsKey("Rest"))
        {
            currentCoroutines.Add("Rest", StartCoroutine(Rest(target)));
        }
    }

    //loop while target is not null and pet sleep is not full
    private IEnumerator Rest(Bed target)
    {
        while (target)
        {
            pet.GetStatus(out PetStatus status, out PetStatus maxStatus);
            if (status.Sleep < maxStatus.Sleep)
            {
                pet.state = PetState.Sleeping;
                yield return new WaitForSeconds(1);
                //return if is null
                if (target)
                {
                    movementation.GoInteract(target.transform, UseBed, interactLayer);
                }
            }
            else
            {
                pet.state = PetState.Idle;
                break;
            }
        }
    }

    private void UseBed(RaycastHit2D hit)
    {
        GameObject target = hit.collider.attachedRigidbody.gameObject;

        if (target.TryGetComponent<Bed>(out Bed bed))
        {
            bed.Use(pet);
            //maximize status
            pet.GetStatus(out PetStatus status, out PetStatus maxStatus);
            pet.SetStatus(new PetStatus(status.Mood, status.Fun, status.Hunger, maxStatus.Sleep, status.Bathroom));
        }
    }
    #endregion

    //Fun
    #region Fun
    private void LookForToy()
    {
        //look for the closest bathroom
        Toy toy = ClosestObject<Toy>();

        //if found 
        if (toy)
        {
            movementation.GoInteract(toy.transform, UseToy, interactLayer);
        }
    }

    private void UseToy(RaycastHit2D hit)
    {
        GameObject target = hit.collider.attachedRigidbody.gameObject;
        if (target.TryGetComponent<Toy>(out Toy toy))
        {
            pet.AddStatus(0, 0, 0, 0, toy.Use());
            hit.collider.attachedRigidbody.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * 25, ForceMode2D.Impulse);
        }
    }

    #endregion

    //bathroom 
    #region Bathroom
    private void LookForBathroom()
    {
        //look for the closest bathroom
        Bathroom bathroom = ClosestObject<Bathroom>();
        //if is a cat look for a sandbox
        //if is a dog look for a paper

        //if found 
        if (bathroom)
        {
            movementation.GoInteract(bathroom.transform, UseBathroom, interactLayer);
        }
    }

    private void UseBathroom(RaycastHit2D hit)
    {
        GameObject target = hit.collider.attachedRigidbody.gameObject;
        if (target.TryGetComponent<Bathroom>(out Bathroom bathroom))
        {
            //maximize bathroom status of the pet
            pet.GetStatus(out PetStatus petStatus, out PetStatus maxStatus);
            pet.SetStatus(new PetStatus(petStatus.Mood, petStatus.Fun, petStatus.Hunger, maxStatus.Sleep, maxStatus.Bathroom));

            //use bathroom
            bathroom.Use(this.pet);
        }
    }

    #endregion

    //Hunger
    #region Hunger
    private void LookForFood()
    {
        FoodPot target = ClosestObject<FoodPot>(verification);

        //return if is null
        if (target)
        {
            movementation.GoInteract(target.transform, usePot, interactLayer);
        }
    }

    //if pot used increment food status and reduce bathroom status
    private void usePot(RaycastHit2D hit)
    {
        GameObject target = hit.collider.attachedRigidbody.gameObject;
        if (target.TryGetComponent<FoodPot>(out FoodPot foodPot))
        {
            pet.GetStatus(out PetStatus status, out PetStatus maxStatus);

            //increment bathroom
            float feed = foodPot.UsePot();
            status.Hunger += feed;
            status.Bathroom -= feed;
            pet.SetStatus(status);
        }
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

    private T ClosestObject<T>(PetLarUtils.Complex<T>.ComplexDelegate verification = null) where T : MonoBehaviour
    {
        List<T> obj_list = new List<T>(FindObjectsOfType<T>());

        //Transform closestpot = null;

        if (obj_list.Count > 0)
        {
            T obj = PetLarUtils.Complex<T>.ClosestObject(this.transform, obj_list.ToArray(), verification);
            return obj != default(T) ? obj : default(T);
        }
        return default(T);
    }
}