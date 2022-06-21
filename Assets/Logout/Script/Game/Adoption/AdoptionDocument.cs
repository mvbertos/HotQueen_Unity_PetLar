using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using System;

public class AdoptionDocument : MonoBehaviour
{
    [SerializeField] private Button QuitButton;
    [SerializeField] private Perfil perfil_pet;
    [SerializeField] private Perfil perfil_human;
    [SerializeField] private Canvas background;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Sprite human_picture;
    private PetData pet;
    private EntityData human;
    public PetData Pet
    {
        get
        {
            return pet;
        }
        set
        {
            pet = value;
            perfil_pet.UpdateInfo(pet);
        }
    }

    [HideInInspector]
    public bool Approved;

    private void Start()
    {
        QuitButton.onClick.AddListener(delegate { Destroy(this.gameObject); });
    }

    private void OnEnable()
    {
        //SETUP MINIGAME
        InitMinigame();
    }

    /// <summary>
    /// pick a random pet in the ong and put in the minigame to be adopted
    /// </summary>
    private void InitMinigame()
    {
        //create a list of pets found in the scene
        ONG ong = GameObject.FindObjectOfType<ONG>();
        List<PetData> petList = ong.PetDatas;

        if (petList.Count > 0)
        {
            //get a random pet in the list
            PetData[] petarray = petList.ToArray();
            pet = petarray[Random.Range(0, petarray.Length)];

            //apply into petfil_pet
            EntityData.Personality randomPersonality = (EntityData.Personality)Random.Range(0, Enum.GetNames(typeof(EntityData.Personality)).Length);
            perfil_human.UpdateInfo(new EntityData(GameManager.instance.humanNames[Random.Range(0, GameManager.instance.humanNames.Length)], human_picture, randomPersonality));
            perfil_pet.UpdateInfo(pet);

            //set description
            description.text = description.text.Replace("@pet_name", pet.name);
        }

    }

    /// <summary>
    /// it confirm the adoption
    /// </summary>
    public void Adopt()
    {
        //if the personality don´t match 
        //create new event to show the error
        // if (pet.personality != human)
        // {
        //     EventManager eventManager = FindObjectOfType<EventManager>();
        //     eventManager.ForceRescueMinigame();
        // }
        // else
        // {
        //     //if the personality match
        //     //create new event to show the success

        //get pet to bet adopted
        //remove it from world
        ONG ong = GameObject.FindObjectOfType<ONG>();
        ong.RemovePet(pet);
        GameManager.instance.SwitchToLastScene();
    }

    /// <summary>
    /// refuse adoption
    /// </summary>
    public void Reject()
    {
        GameManager.instance.SwitchToLastScene();
    }
}
