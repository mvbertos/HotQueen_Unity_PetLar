using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Pet : MonoBehaviour
{
    //pet information
    [SerializeField] private PetData data;
    [SerializeField] private InformationDisplayer informationDisplayer;

    //pet status
    //mood will be reduced, when pet is nearby bad things like empty food pot, dirty corner and else.
    private PetStatus status;
    private PetStatus maxStatus;


    private void Start()
    {
        informationDisplayer.UpdateInformation(data.Name, 100);
        InitStatus();
    }

    //pet 
    public void ChangeName(String newName)
    {
        data.Name = newName;
    }

    public PetData GetData()
    {
        return data;
    }

    public void SetStatus(PetStatus status)
    {
        this.status = new PetStatus(status.Mood, status.Hunger, status.Sleep, status.Bathroom);
    }

    public void GetStatus(out PetStatus status, out PetStatus maxStatus)
    {
        status = this.status;
        maxStatus = this.maxStatus;
    }

    //status    
    private void InitStatus()
    {
        //Health and Stuff
        status = new PetStatus(100, 100, 100, 100);
        maxStatus = new PetStatus(status.Mood, status.Hunger, status.Sleep, status.Bathroom);
    }
}

[System.Serializable]
public class PetStatus
{
    public float Mood; //mood will be reduced if in the pet range, there is unlikable stuff like dirty sandbox or no food in pot
    public float Hunger; //hunger will reduce over time
    public float Sleep; //sleep will reduce over time
    public float Bathroom; // Bathroom will reduce every time pet eat something

    public PetStatus(float mood, float hunger, float sleep, float bathroom)
    {
        Mood = mood;
        Hunger = hunger;
        Sleep = sleep;
        Bathroom = bathroom;
    }
}

public enum Personality
{
    Playfull,
    Angry,
}