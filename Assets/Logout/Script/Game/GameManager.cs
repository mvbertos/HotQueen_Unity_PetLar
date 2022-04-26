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
        public float mood, food, money;
        public ONG()
        {
            this.mood = MAX_VALUE;
            this.food = MAX_VALUE;
            this.money = 1000;
        }
    }

    public static ONG _ong { private set; get; }

    void Start()
    {
        _ong = new ONG();
    }
    private void Update()
    {
        _ong.mood = GetGeneralMood();
    }

    //return media from all the pets in the game
    private float GetGeneralMood()
    {
        float moodCount = 0;

        PetStatusManager[] petsList = FindObjectsOfType<PetStatusManager>();
        foreach (PetStatusManager item in petsList)
        {
            moodCount += item.status.Mood;
        }

        float moodMedia = moodCount / petsList.Length;
        Debug.Log(moodCount);
        return moodMedia;
    }
}
