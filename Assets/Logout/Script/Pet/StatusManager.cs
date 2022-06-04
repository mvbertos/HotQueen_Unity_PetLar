using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this class handle the behavior of each status caried by the pet
/// that includes:
/// - hunger
/// - sleepness
/// - bathroom
/// - mood
/// </summary>
public class StatusManager : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Pet pet;

    //callbacks
    public Action OnHungerCallback;
    public Action OnBathroomCallback;
    public Action OnFunCallback;
    public Action OnSleepCallback;

    private void Start()
    {
        //Handle status based in 
        PetLarUtils.LoopTimerEvent(UpdateHunger, 1, "UpdateHunger");
        PetLarUtils.LoopTimerEvent(UpdateBathroom, 1, "UpdateBathroom");
        PetLarUtils.LoopTimerEvent(UpdateSleep, 1, "UpdateSleep");
        PetLarUtils.LoopTimerEvent(UpdateFun, 1, "UpdateFun");
    }

    private void UpdateFun()
    {
        pet.GetStatus(out PetStatus _status, out PetStatus _maxStatus);
        _status.Fun -= 1;
        pet.SetStatus(_status);

        if (!IsHungry() && !NeedBathroom() && !IsSleepy())
        {
            OnFunCallback?.Invoke();
        }
    }

    private void UpdateMood()
    {

        pet.GetStatus(out PetStatus _status, out PetStatus _maxStatus);

        _status.Mood = UpdateMood(this.transform.position, radius, _maxStatus.Mood);
    }

    //Sleep
    private void UpdateSleep()
    {
        pet.AddStatus(0, 0, 0, -1, 0);
        if (IsSleepy())
        {
            OnSleepCallback?.Invoke();
        }
    }

    private bool IsSleepy()
    {
        pet.GetStatus(out PetStatus _status, out PetStatus _maxStatus);
        if (_status.Sleep < _maxStatus.Sleep / 3) //hunger is lesser than half 
        {
            return true;
        }
        return false;
    }

    //HUNGER
    private void UpdateHunger()
    {
        pet.AddStatus(0, 0, -1, 0, 0);
        if (IsHungry())
        {
            OnHungerCallback?.Invoke(); //pet is hunger
        }
    }

    public bool IsHungry()
    {
        pet.GetStatus(out PetStatus _status, out PetStatus _maxStatus);
        if (_status.Hunger < _maxStatus.Hunger / 2) //hunger is lesser than half 
        {
            return true;
        }
        return false;
    }

    //BATHROOM
    private void UpdateBathroom() //Bathroom will be updated every after pet use pot food
    {
        pet.AddStatus(0, 0, 0, 0, -1);
        if (NeedBathroom())
        {
            OnBathroomCallback?.Invoke();
        }
    }

    public bool NeedBathroom()
    {
        pet.GetStatus(out PetStatus _status, out PetStatus _maxStatus);
        if (_status.Bathroom < _maxStatus.Bathroom / 2) //is lesser than half 
        {
            return true;
        }
        return false;
    }

    /// Calculates the mood by verifying if there is dislikeble object around 
    /// or the pets hunger is too low
    private float UpdateMood(Vector3 origin, float radius, float maxValue)
    {
        float mood = maxValue;

        Collider2D[] objectsDetected = Physics2D.OverlapCircleAll(origin, radius);
        foreach (Collider2D item in objectsDetected)
        {
            if (item.TryGetComponent<ObjectMoodEffect>(out ObjectMoodEffect _object))
            {
                mood -= _object.MoodEffect;
            }
        }

        return mood;
    }

}
