using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static string lastSceneLoaded;
    private static string sceneLoaded;

    //PET THINGS
    [SerializeField] private Pet[] petReferences;//filled with prefabs
    public Pet[] PetReferences { get { return petReferences; } }
    private static readonly String[] petNames = { "Alberto", "Samanta", "Poly", "Nino", "Amaterasu" }; //list of names to be used

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadScene("StartMenuScene");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    //Pets
    public static void AddNewPetToWorld(Pet pet)
    {
        //instantiate pet in world
        Pet newpet = Instantiate<Pet>(pet, SceneManager.GetSceneByName("GameScene").GetRootGameObjects()[0].transform);
        newpet.ChangeName(petNames[Random.Range(0, petNames.Length)]);

        //add this to ong pets list
        ONG ong = GameObject.FindObjectOfType<ONG>();
        ong.AddPet(newpet);

    }

    public static void RemovePetToWorld(PetData pet)
    {
        //remove pet from ong list
        Pet[] pets = GameObject.FindObjectsOfType<Pet>();
        foreach (Pet p in pets)
        {
            if (p.GetData().Name == pet.Name)
            {
                Destroy(p);
                break;
            }
        }
    }
    public static void RemovePetToWorld(Pet pet)
    {
        //remove pet from ong list
        RemovePetToWorld(pet.GetData());
    }

    public static void GameOver()
    {
        ONG ong = GameObject.FindObjectOfType<ONG>();
        if (ong.Money <= 0)
        {
            //Game Over
            Debug.Log("Game Over");
        }
    }

    public static void LoadScene(String scene, int mode = 1)
    {
        sceneLoaded = scene;
        TimerEvent.StopAll();
        if (mode == 0)
        {
            SceneManager.LoadScene(scene);
        }
        else if (mode == 1)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);

        }
    }

    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "GameScene")
        {
            ONG ong = GameObject.FindObjectOfType<ONG>();
            ong.AddPetDatasInWorld();
        }
    }

    public static void UnloadScene(String scene)
    {
        lastSceneLoaded = sceneLoaded;
        SceneManager.UnloadSceneAsync(scene);
    }

    public static void SwitchScene(String scene, int mode = 1)
    {
        UnloadScene(sceneLoaded);
        LoadScene(scene, mode);
    }

    public static void SwitchToLastScene()
    {
        SwitchScene(lastSceneLoaded);
    }
}
