using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

//Events are classes that hold actions and information to the event that will be executed
//some of the information are
//time_range <- used to randomize it call in EVENTMANAGER
public class Event
{
    //Return a min and max int range to be used as reference in EVENTMANAGER
    public Sprite EventSprite;
    public string EventName;
    public string EventDescription;
    public Vector2 TimeRange;
    public virtual void ConfirmEvent()
    {
        Debug.Log("Confirm");
    }
    public virtual void DeclineEvent()
    {
        Debug.Log("Decline");
    }

}

/// <summary>
/// show the possibility to rescue new pets
/// </summary>
public class Rescue : Event
{
    private Pet randomPet = null;
    public Rescue()
    {
        Pet[] petArray = GameManager.instance.PetReferences.ToArray();
        randomPet = petArray[Random.Range(0, GameManager.petInstances.Count - 1)];
        //Set event_sprite as it´s appearance
        EventSprite = randomPet.GetData().Picture;
        //set name
        EventName = "Pet Rescue";
        //Set Description
        EventDescription = "A pet is getting cold out side your door, you wish to give him a temporary home?";
        //Set Time Range
        TimeRange = new Vector2(25, 50);
    }

    public override void ConfirmEvent()
    {
        GameManager.AddNewPetToWorld(randomPet);
    }

    public override void DeclineEvent()
    {
        //nothing for the moment
    }
}
/// <summary>
/// Donation when it´s triggered will return money to the player bank
/// </summary>
public class Donation : Event
{
    private float PriceValue;

    public Donation(float priceValue)
    {
        PriceValue = priceValue;
        EventSprite = null;
        EventName = "Post de doação";
        EventDescription = "Faça um post para que outros ajudem sua causa!";

        TimeRange = new Vector2(2, 4);
    }

    public override void ConfirmEvent()
    {
        RewardMoney();
        //LoopReward();
    }
    public override void DeclineEvent()
    {
        //TimerEvent.StopTimer("Reward");
    }
    private void LoopReward()
    {
        RewardMoney();
        TimerEvent.Create(() => { LoopReward(); }, 0.5f, "Reward");
    }
    private void RewardMoney()
    {
        //find object of type ong in the scene
        //increase ong money
        ONG ong = GameObject.FindObjectOfType<ONG>();
        ong.Money += PriceValue;
    }
}

public class Adoption : Event
{
    public Adoption()
    {
        EventSprite = null;
        EventName = "Doação";
        EventDescription = "Doe um bixinho resgatado!";
        TimeRange = new Vector2(1, 2);
    }
    public override void ConfirmEvent()
    {
        GameManager.SwitchScene("MG_AdoptionScene");
    }
}