using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetStatusManager : MonoBehaviour
{
    [Header("Mood")]
    [SerializeField] private float radius;

    //mood will be reduced, when pet is nerby bad things like empty food pot, dirty corner and else.
    public PetStatus status { private set; get; }
    private PetStatus maxStatus;

    //personality
    public Perfil.Data perfil = new Perfil.Data();
    public Action OnHungerCallback;

    private void Awake()
    {
        status = new PetStatus(100, 60);
        maxStatus = new PetStatus(status.Hunger, status.Mood);
    }
    private void Start()
    {
        //Handle Hunger status based in 
        PetLarUtils.LoopTimerEvent(UpdateHunger, 1, "UpdateHunger");
    }

    private void Update()
    {
        //Handle mood status
        status.Mood = UpdateMood(this.transform.position, radius, maxStatus.Mood);

    }

    private void UpdateHunger()
    {
        status.Hunger -= 1;
        if (status.Hunger < 50)
        {
            OnHungerCallback?.Invoke();
        }

        Debug.Log(status.Hunger);
    }

    //Handle status
    /// <summary>
    /// Calculates the mood by verifying if there is dislikeble object around 
    /// or the pets hunger is too low
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="radius"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
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

        //todo: lower the mood if hunger is below 50%
        return mood;
    }

}

[System.Serializable]
public class PetStatus
{
    public float Mood;
    public float Hunger;

    public PetStatus(float mood, float hunger)
    {
        Mood = mood;
        Hunger = hunger;
    }
}

public enum Personality
{
    Playfull,
    Angry,
}