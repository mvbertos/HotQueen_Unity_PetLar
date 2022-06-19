using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EventManager : MonoBehaviour
{

    List<Event> events = new List<Event>();
    public EventInterfaceManager eventInterface;

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
        Rescue rescues = new Rescue();
        events.Add(rescues);

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
        int rand_time = Random.Range((int)rand_event.TimeRange.x, (int)rand_event.TimeRange.y);

        //create new timer
        TimerEvent.Create(() =>
        {
            eventInterface.Enable(rand_event.EventDescription, rand_event.EventSprite, () => { rand_event.ConfirmEvent(); RemoveEvent(rand_event); }, () => { rand_event.DeclineEvent(); RemoveEvent(rand_event); });
        }, rand_time);

    }

    private void RemoveEvent(Event rand_event)
    {
        events.Remove(rand_event);
        if (events.Count == 0)
        {
            InitializeEventList();
        }
    }
}
