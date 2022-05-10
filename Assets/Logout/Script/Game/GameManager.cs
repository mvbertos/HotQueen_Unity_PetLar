using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private PetStatusManager[] petList;
    public static PetStatusManager[] PetList;
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

    public MouseRole mouseRole = 0;

    public static ONG _ong { private set; get; }

    private void Awake()
    {
        _ong = new ONG();
        instance = this;
    }

    private void Start()
    {
        PetList = petList;
    }

    private void Update()
    {
        _ong.mood = GetGeneralMood();
    }

    #region PET HANDLER
    public static void AddNewPetToWorld(String name, String species, EntityData.Personalities personalities)
    {
        EntityData data = new EntityData();
        data.Name = name;
        data.Personality = personalities;
        data.Picture = PetList[0].petPerfil.Picture;

        PetStatusManager newpet = Instantiate<PetStatusManager>(PetList[0]);
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
public enum MouseRole
{
    Drag,
    Trigger,
}
