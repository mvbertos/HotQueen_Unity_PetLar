using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EventManager : MonoBehaviour
{

    List<Event> events = new List<Event>();
    public EventInterfaceManager eventInterface;
    public Pet pet;

    private void Start()
    {
        //Initialize list
        InitializeEventList();
    }

    public void InitializeEventList()
    {
        //if list is null
        //create a randomized event, between all the events existents
        // add to the list
        Adoption adoption = new Adoption(pet);
        events.Add(adoption);
        //Start events
        StartNewEvent();
    }

    /// <summary>
    /// it execute events in the list
    /// </summary>
    private void StartNewEvent()
    {
        //randomize event
        int rand_index = Random.Range(0, events.Count);
        Event rand_event = events[rand_index];

        //randomize timer
        int rand_time = Random.Range((int)rand_event.time_range.x, (int)rand_event.time_range.y);

        //create new timer
        TimerEvent.Create(() =>
        {
            eventInterface.Enable(rand_event.event_description, rand_event.event_sprite, rand_event.ConfirmEvent, rand_event.DeclineEvent);
        }, 1);

        // if there is no more events in the list
        // initialize list again
    }
}
