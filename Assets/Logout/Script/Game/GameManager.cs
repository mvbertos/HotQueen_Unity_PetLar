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
    public static List<Pet> petInstances = new List<Pet>(); //register of all pets in world currently
    [SerializeField] private Pet[] petReferences;//filled with prefabs
    public static Pet[] petArray { private set; get; }// same as petReferences but static
    private static readonly String[] petNames = { "Alberto", "Samanta", "Poly", "Nino", "Amaterasu" }; //list of names to be used

    private void Start()
    {
        LoadScene("StartMenuScene");
    }


    //Pets
    public static void AddNewPetToWorld(Pet pet)
    {
        Pet newpet = Instantiate<Pet>(pet);
        newpet.ChangeName(petNames[Random.Range(0, petNames.Length)]);
    }

    // private static void UpdatePetInstances()
    // {
    //     foreach (var pet in GameObject.FindObjectsOfType<Pet>())
    //     {
    //         petInstances.Add(pet);
    //     }
    // }


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
        if (mode == 0)
        {
            SceneManager.LoadScene(scene);
        }
        else if (mode == 1)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
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
