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

    //DEBUG
    public bool DEBUG_MODE = false;

    //SCENE
    private string lastSceneLoaded;
    private string sceneLoaded;
    [SerializeField] private ScenesNames sceneName;
    public ScenesNames SceneName
    {
        get { return sceneName; }
    }

    //PET THINGS
    [SerializeField] private Pet petReference;//filled with prefabs
    [SerializeField] private PetData[] petData;
    public PetData[] PetData { get { return petData; } }
    public readonly String[] petNames = { "Alberto", "Samanta", "Poly", "Nino", "Amaterasu", "Thor", "Loki", "Cacau", "Morena" }; //list of names to be used
    public readonly String[] humanNames = { "Alberto", "Samanta", "Vanderlei", "Nino", "Francisco", "João", "Ana", "Fabricio" }; //list of pictures to be used
    //Sounds
    [SerializeField] private AudioClip Clip_newCat;
    [SerializeField] private AudioClip Clip_newDog;

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

    void Update()
    {
        if (DEBUG_MODE)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                EventManager eventManager = FindObjectOfType<EventManager>();
                eventManager.ForceRescueMinigame();
            }
        }
    }

    //Pets
    public void AddNewPetToWorld(Pet pet)
    {
        //instantiate pet in world
        Pet newpet = Instantiate<Pet>(pet, SceneManager.GetSceneByName("GameScene").GetRootGameObjects()[0].transform);
        newpet.ChangeName(petNames[Random.Range(0, petNames.Length)]);
        newpet.ChangePersonality((EntityData.Personality)Random.Range(0, Enum.GetNames(typeof(EntityData.Personality)).Length));

        //add this to ong pets list
        ONG ong = GameObject.FindObjectOfType<ONG>();
        ong.AddPet(newpet);

        AudioPlayer audioPlayer = FindObjectOfType<AudioPlayer>();
        if (pet.GetData().isDog)
        {
            //if is a dog play dog sound
            audioPlayer.PlayAudio(Clip_newDog, false);
        }
        else
        {
            //if is a cat play cat sound
            audioPlayer.PlayAudio(Clip_newCat, false);
        }
    }

    public void AddNewPetToWorld(PetData data)
    {
        //instantiate pet in world
        Pet newpet = Instantiate<Pet>(petReference, SceneManager.GetSceneByName("GameScene").GetRootGameObjects()[0].transform);
        newpet.SetData(data);
        newpet.ChangeName(petNames[Random.Range(0, petNames.Length)]);

        //add this to ong pets list
        ONG ong = GameObject.FindObjectOfType<ONG>();
        ong.AddPet(newpet);


        AudioPlayer audioPlayer = FindObjectOfType<AudioPlayer>();
        if (data.isDog)
        {
            //if is a dog play dog sound
            audioPlayer.PlayAudio(Clip_newDog, false);
        }
        else
        {
            //if is a cat play cat sound
            audioPlayer.PlayAudio(Clip_newCat, false);
        }
    }

    public void RemovePetToWorld(PetData pet)
    {
        //remove pet from ong list
        Pet[] pets = GameObject.FindObjectsOfType<Pet>();
        foreach (Pet p in pets)
        {
            if (p.GetData().name == pet.name)
            {
                Destroy(p);
                break;
            }
        }
    }
    public void RemovePetToWorld(Pet pet)
    {
        //remove pet from ong list
        RemovePetToWorld(pet.GetData());
    }

    public void GameOver()
    {
        ONG ong = GameObject.FindObjectOfType<ONG>();
        if (ong.Money <= 0)
        {
            //Game Over
            Debug.Log("Game Over");
        }
    }

    public void LoadScene(String scene, int mode = 1)
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

    private void UpdateSceneMusic(string scene)
    {
        //find object of type AudioPlayer
        //play a new music
        AudioPlayer audioPlayer = FindObjectOfType<AudioPlayer>();
        if (audioPlayer != null)
        {
            audioPlayer.PlaySceneMusic(scene);
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "GameScene")
        {
            ONG ong = GameObject.FindObjectOfType<ONG>();
            ong.AddPetDatasInWorld();
        }
        UpdateSceneMusic(sceneLoaded);
    }

    public void UnloadScene(String scene)
    {
        lastSceneLoaded = sceneLoaded;
        SceneManager.UnloadSceneAsync(scene);
    }

    public void SwitchScene(String scene, int mode = 1)
    {
        UnloadScene(sceneLoaded);
        LoadScene(scene, mode);
    }

    public void SwitchToLastScene()
    {

        SwitchScene(lastSceneLoaded);
    }
}
[System.Serializable]
public class ScenesNames
{
    public string Game = "GameScene";
    public string StartMenu = "StartMenuScene";
    public string Adoption = "AdoptionScene";
}
