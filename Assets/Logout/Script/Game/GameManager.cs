using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public class ONG
    {
        public float MAX_VALUE = 100;
        public float mood, food, energy;
        public ONG()
        {
            this.mood = MAX_VALUE;
            this.food = MAX_VALUE;
            this.energy = MAX_VALUE;
        }
    }

    public static ONG _ong { private set; get; }

    void Start()
    {
        _ong = new ONG();
        StartEnergyLoop();
    }
    private void Update()
    {
        _ong.mood = GetGeneralMood();
    }

    //Energy infinte loop to reduce value
    private void StartEnergyLoop()
    {
        TimerEvent.Create(() => { EnergyLoop(); }, 1);
    }

    private void EnergyLoop()
    {
        _ong.energy = _ong.energy - 1;
        TimerEvent.Create(() => { EnergyLoop(); }, 1);
    }

    //return media from all the pets in the game
    private float GetGeneralMood()
    {
        int count = 0;
        float moodCount = 0;

        foreach (PetStatusManager item in FindObjectsOfType<PetStatusManager>())
        {
            count += 1;
            moodCount += item.status.Mood;
        }

        //MoodBar
        float moodMedia = moodCount / count;
        return moodMedia;
    }
}
