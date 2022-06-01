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

    private void Start()
    {
        //Handle status based in 
        PetLarUtils.LoopTimerEvent(UpdateHunger, 1, "UpdateHunger");
        PetLarUtils.LoopTimerEvent(UpdateMood, 1, "UpdateMood");
    }

    private void UpdateMood()
    {
        PetStatus _status = null;
        PetStatus _maxStatus = null;
        pet.GetStatus(out _status, out _maxStatus);

        _status.Mood = UpdateMood(this.transform.position, radius, _maxStatus.Mood);
    }

    private void UpdateHunger()
    {
        PetStatus _status = null;
        PetStatus _maxStatus = null;
        pet.GetStatus(out _status, out _maxStatus);

        _status.Hunger -= 1;
        if (_status.Hunger < _maxStatus.Hunger / 2) //hunger is lesser than half 
        {
            print("Hunger");
            OnHungerCallback?.Invoke(); //pet is hunger
        }
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
