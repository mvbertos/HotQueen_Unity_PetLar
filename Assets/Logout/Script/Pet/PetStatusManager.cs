using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetStatusManager : MonoBehaviour
{
    [Header("Mood")]
    [SerializeField] private float radius;
    [SerializeField] private Slider m_slider;


    private bool hungerIsBeingUpdated = false;

    //mood will be reduced, when pet is nerby bad things like empty food pot, dirty corner and else.
    public PetStatus status;
    private PetStatus maxStatus;


    private void Start()
    {
        //initialize maxstatus
        maxStatus = new PetStatus();
        maxStatus.Mood = status.Mood;
        maxStatus.Hunger = status.Hunger;
    }

    private void Update()
    {
        //Handle mood status
        status.Mood = GetMood(this.transform.position, radius, maxStatus.Mood);
        UpdateMoodSlider();

        //Handle Hunger status based in 
        if (!hungerIsBeingUpdated)
        {
            hungerIsBeingUpdated = true;
            StartCoroutine(UpdateHunger());
        }
    }

    private IEnumerator UpdateHunger()
    {
        yield return new WaitForSeconds(1f);
        
        status.Hunger -= 1;
        if (status.Hunger < 50)
        {
            status.OnHungerCallback?.Invoke();
        }

        hungerIsBeingUpdated = false;
    }

    private void UpdateMoodSlider()
    {
        float newMood = status.Mood / maxStatus.Mood;
        if (m_slider)
        {
            if (newMood <= 0)
            {
                m_slider.value = 0;
            }
            else
            {
                m_slider.value = newMood;
            }
        }
    }

    //Handle status
    public float GetMood(Vector3 origin, float radius, float maxValue)
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

[System.Serializable]
public class PetStatus
{
    public float Mood;
    public float Hunger;
    public Action OnHungerCallback;
}