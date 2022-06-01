using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //PET THINGS
    public static List<Pet> petInstances = new List<Pet>(); //register of all pets in world currently
    [SerializeField] private Pet[] petReferences;//filled with prefabs
    public static Pet[] petArray { private set; get; }// same as petReferences but static
    private static readonly String[] petNames = { "Alberto", "Samanta", "Poly", "Nino", "Amaterasu" }; //list of names to be used

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
        petArray = petReferences;
    }

    
    //Pets
    public static void AddNewPetToWorld(Pet pet)
    {
        Pet newpet = Instantiate<Pet>(pet);
        newpet.ChangeName(petNames[Random.Range(0, petNames.Length)]);
        UpdatePetInstances();
    }

    private static void UpdatePetInstances()
    {
        foreach (var pet in GameObject.FindObjectsOfType<Pet>())
        {
            petInstances.Add(pet);
        }
    }
}
