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
    private List<PetStatusManager> petList = new List<PetStatusManager>();
    private PetStatusManager adoption;

    private void Start()
    {
        QuitButton.onClick.AddListener(delegate { Destroy(this.gameObject); });


    }


    private void OnEnable()
    {
        petList = FindObjectsOfType<PetStatusManager>().ToList();

        if (petList.Count > 0)
        {
            petList.Sort();

            PetStatusManager[] petarray = petList.ToArray();
            adoption = petarray[0];


            //create new data to the first pet in the list
            Perfil.Data new_data = new Perfil.Data();
            new_data = adoption.perfil;

            //apply into petfil_pet
            perfil_human.current_data = newData[0];
            perfil_pet.current_data = new_data;

            perfil_pet.gameObject.SetActive(true);
            perfil_human.gameObject.SetActive(true);
        }

    }

    public void Adopt()
    {
        //get pet to bet adopted
        //remove it from world
        Destroy(adoption.gameObject);
        Destroy(this.gameObject);
    }
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
