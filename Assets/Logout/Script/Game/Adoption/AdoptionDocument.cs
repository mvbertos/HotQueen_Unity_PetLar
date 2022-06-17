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
    private Pet pet;
    [HideInInspector] public bool Approved;

    private void Start()
    {
        QuitButton.onClick.AddListener(delegate { Destroy(this.gameObject); });
    }


    private void OnEnable()
    {
        //SETUP MINIGAME
        InitMinigame();
    }

    private void OnDisable()
    {
        GameManager.SwitchToLastScene();
    }

    /// <summary>
    /// pick a random pet in the ong and put in the minigame to be adopted
    /// </summary>
    private void InitMinigame()
    {
        //create a list of pets found in the scene
        List<Pet> petList = GameManager.petInstances;

        if (petList.Count > 0)
        {
            //get a random pet in the list
            Pet[] petarray = petList.ToArray();
            pet = petarray[Random.Range(0, petarray.Length)];


            //put it in adoption
            //create new data to the first pet in the list
            EntityData new_data = pet.GetData();

            //apply into petfil_pet
            perfil_human.current_data = new EntityData("Robson", null, EntityData.Personalities.Playfull);
            perfil_pet.current_data = new_data;

            perfil_pet.gameObject.SetActive(true);
            perfil_human.gameObject.SetActive(true);

            //set description
            description.text = description.text.Replace("@pet_name", new_data.Name);
        }

    }

    /// <summary>
    /// it confirm the adoption
    /// </summary>
    public void Adopt()
    {
        //get pet to bet adopted
        //remove it from world
        Destroy(pet.gameObject);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// refuse adoption
    /// </summary>
    public void Reject()
    {
        Destroy(this.gameObject);
    }
}
