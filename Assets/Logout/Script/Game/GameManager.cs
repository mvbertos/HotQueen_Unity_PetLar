using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Pet[] PetArray;
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

    private void Awake()
    {
        _ong = new ONG();
        instance = this;
    }

    private void Update()
    {
        _ong.mood = GetGeneralMood();
    }

    #region PET HANDLER

    public static void AddNewPetToWorld(String name, Sprite picture, EntityData.Personalities personalities)
    {
        EntityData data = new EntityData();
        data.Name = name;
        data.Personality = personalities;
        data.Picture = picture;

        Pet newpet = Instantiate<Pet>(PetArray[0]);
        newpet.petPerfil = data;
    }

    #endregion
    #region STATUS HANDLER

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
        return moodMedia;
    }

    #endregion
}
