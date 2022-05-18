using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MG_Adoption : MonoBehaviour
{
    [SerializeField] private Button QuitButton;
    [SerializeField] private EntityData[] newData = new EntityData[2];
    [SerializeField] private Perfil perfil_pet;
    [SerializeField] private Perfil perfil_human;
    private List<Pet> petList = new List<Pet>();
    private Pet adoption;

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
        petList = FindObjectsOfType<Pet>().ToList();

        if (petList.Count > 0)
        {
            //get a random pet in the list
            Pet[] petarray = petList.ToArray();
            adoption = petarray[Random.Range(0, petarray.Length)];


            //put it in adoption
            //create new data to the first pet in the list
            EntityData new_data = new EntityData(adoption.Data.Name, adoption.Data.Picture, adoption.Data.Personality);

            //apply into petfil_pet
            perfil_human.current_data = newData[0];
            perfil_pet.current_data = new_data;

            perfil_pet.gameObject.SetActive(true);
            perfil_human.gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// it confirm the adoption
    /// </summary>
    public void Adopt()
    {
        //get pet to bet adopted
        //remove it from world
        Destroy(adoption.gameObject);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// refuse adoption
    /// </summary>
    public void Reject()
    {
        Destroy(this.gameObject);
    }

    private void OnDisable()
    {

        perfil_pet.gameObject.SetActive(false);
        perfil_human.gameObject.SetActive(false);
    }
}
