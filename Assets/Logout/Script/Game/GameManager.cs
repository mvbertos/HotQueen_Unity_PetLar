using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Tooltip("Drag here, any prefab related to Pet")]
    public Pet[] petArray;
    public static Dictionary<PetSpecies, Pet> PetDictionary = new Dictionary<PetSpecies, Pet>();

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

        foreach (Pet item in petArray)
        {
            if (!PetDictionary.ContainsKey(item.Specie))
                PetDictionary.Add(item.Specie, item);
        }
    }

    private void Update()
    {
        _ong.mood = GetGeneralMood();
    }

    #region PET HANDLER

    public static void AddNewPetToWorld(String name, PetSpecies species, EntityData.Personalities personalities)
    {
        Pet newpet = Instantiate<Pet>(PetDictionary[species]);

        EntityData data = new EntityData(name, PetDictionary[species].Data.Picture, personalities);
        newpet.UpdatePerfil(data.Name, data.Picture, data.Personality);
    }

    public static void AddNewPetToWorld(Pet pet)
    {
        AddNewPetToWorld(pet.Data.Name, pet.Specie, pet.Data.Personality);
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
