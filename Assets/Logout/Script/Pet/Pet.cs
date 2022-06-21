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
    [SerializeField] private AIMovimentation movement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }
    private Collider2D[] colliders;


    //pet status
    //mood will be reduced, when pet is nearby bad things like empty food pot, dirty corner and else.
    private PetStatus status;
    private PetStatus maxStatus;

    //petstate
    public PetState state;

    private void Start()
    {
        informationDisplayer.UpdateInformation(data.name, 100);
        state = PetState.Idle;
        InitStatus();
        InitGraphics();
    }

    private void InitGraphics()
    {
        //set sprite renderer as data picture
        spriteRenderer.sprite = data.picture;
        animator.SetInteger("Type", (int)data.specie);
    }

    //pet 
    public void ChangeName(String newName)
    {
        data.name = newName;
    }
    public void ChangePersonality(EntityData.Personality newValue)
    {
        data.personality = newValue;
    }

    public PetData GetData()
    {
        return data;
    }
    public void SetData(PetData petData)
    {
        data = petData;
        InitGraphics();
    }

    public void SetStatus(PetStatus status)
    {
        this.status = new PetStatus(status.Mood, status.Fun, status.Hunger, status.Sleep, status.Bathroom);
    }


    public void AddStatus(float mood, float fun, float hunger, float sleep, float bathroom)
    {
        PetStatus newStatus = new PetStatus(mood, fun, hunger, sleep, bathroom);

        this.status.Mood += mood;
        this.status.Fun += fun;
        this.status.Hunger += hunger;
        this.status.Sleep += sleep;
        this.status.Bathroom += bathroom;
    }

    public void EnableAI()
    {
        movement.enabled = true;
    }

    public void DisableAI()
    {
        movement.enabled = false;
    }

    public void DisableColliders()
    {
        //get all colliders in child and disable them
        colliders = this.gameObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }

    public void EnableColliders()
    {
        //get all colliders in child and disable them
        colliders = this.gameObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
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
        status = new PetStatus(100, 100, 100, 25, 100);
        maxStatus = new PetStatus(100, 100, 100, 100, 100);
    }
}

[System.Serializable]
public class PetStatus
{
    public float Mood; //mood will be reduced if in the pet range, there is unlikable stuff like dirty sandbox or no food in pot
    public float Fun; // Fun will reduce over time
    public float Hunger; //hunger will reduce over time
    public float Sleep; //sleep will reduce over time
    public float Bathroom; // Bathroom will reduce every time pet eat something

    public PetStatus(float mood, float fun, float hunger, float sleep, float bathroom)
    {
        Mood = mood;
        Fun = fun;
        Hunger = hunger;
        Sleep = sleep;
        Bathroom = bathroom;
    }
}

public enum PetState
{
    Idle,
    Walking,
    Eating,
    Sleeping,
    Bathroom,
    Playing,
    Dead,
}