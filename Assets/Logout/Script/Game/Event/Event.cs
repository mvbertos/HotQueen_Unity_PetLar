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
    public Sprite event_sprite;
    public string event_description;
    public Vector2 time_range;
    public virtual void ConfirmEvent()
    {
        Debug.Log("Confirm");
    }
    public virtual void DeclineEvent()
    {
        Debug.Log("Decline");
    }

}
public class Adoption : Event
{
    Pet petReference;
    public Adoption()
    {
        //get a random pet
        var values = Enum.GetValues(typeof(PetSpecies));
        PetSpecies species = (PetSpecies)values.GetValue(Random.Range(0, values.Length));
        petReference = GameManager.PetDictionary[species];

        //Set event_sprite as it´s appearance
        event_sprite = petReference.Data.Picture;
        //Set Description
        event_description = "A pet is getting cold out side your door, you wish to give him a temporary home?";
        //Set Time Range
        time_range = new Vector2(5, 10);
    }
    public void Adopt()
    {
        GameManager.AddNewPetToWorld(petReference);
    }

    public override void ConfirmEvent()
    {
        Adopt();
    }

    public override void DeclineEvent()
    {
        Debug.Log("You disserve hell!!");
    }
}