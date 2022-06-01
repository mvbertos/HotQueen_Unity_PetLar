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
        status = this.status;
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
        status = new PetStatus(100, 51);
        maxStatus = new PetStatus(status.Hunger, status.Mood);
    }
}

[System.Serializable]
public class PetStatus
{
    public float Mood;
    public float Hunger;
    public float Sleep;
    public float Bathroom;

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