using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EventManager : MonoBehaviour
{

    List<Event> events = new List<Event>();
    public EventInterfaceManager eventInterface;
    [SerializeField] private AudioClip Clip_popAudio;

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
        StartCoroutine(WaitToEnable(rand_event, rand_time));
    }

    private void RemoveEvent(Event rand_event)
    {
        events.Remove(rand_event);
        if (events.Count == 0)
        {
            InitializeEventList();
        }
    }
    private IEnumerator WaitToEnable(Event rand_event, float rand_time)
    {
        while (true)
        {
            if (!eventInterface.IsActive)
            {

                TimerEvent.Create(() =>
                {
                    eventInterface.Enable(rand_event.EventDescription, rand_event.EventSprite, () => { rand_event.ConfirmEvent(); RemoveEvent(rand_event); }, () => { rand_event.DeclineEvent(); RemoveEvent(rand_event); });
                    //find object of type AudioPlayer
                    //play audio
                    AudioPlayer audioPlayer = FindObjectOfType<AudioPlayer>();
                    audioPlayer.PlayAudio(Clip_popAudio, false);
                }, rand_time);
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ForceAdoptionMinigame()
    {
        Adoption adoption = new Adoption();
        adoption.ConfirmEvent();
    }
    public void ForceRescueMinigame()
    {
        Rescue rescue = new Rescue();
        rescue.ConfirmEvent();
    }
}
