using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AdoptionDocument : MonoBehaviour
{
    [SerializeField] private Button QuitButton;
    [SerializeField] private Perfil perfil_pet;
    [SerializeField] private Perfil perfil_human;
    [SerializeField] private Canvas background;
    [SerializeField] private TMP_Text description;
    private PetData pet;
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
            perfil_human.UpdateInfo(new EntityData("Robson", null, EntityData.Personalities.Playfull));
            perfil_pet.UpdateInfo(pet);

            //set description
            description.text = description.text.Replace("@pet_name", pet.Name);
        }

    }

    /// <summary>
    /// it confirm the adoption
    /// </summary>
    public void Adopt()
    {
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
