using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ONG : MonoBehaviour
{
    [SerializeField] private float money = 1000;
    [SerializeField] private float maxFood = 100;
    [SerializeField] private float food = 0;
    private List<PetData> petDatas = new List<PetData>(); //pets rescued by the ONG
    public List<PetData> PetDatas { get { return petDatas; } }

    public float maxFoodValue { get { return maxFood; } }

    public float Food
    {
        get { return food; }
        set { food = value; }
    }

    public float Money
    {
        get { return money; }
        set { money = value; }
    }

    public void AddPet(Pet pet)
    {
        petDatas.Add(pet.GetData());
    }

    public void RemovePet(Pet pet)
    {
        RemovePet(pet.GetData());
    }

    public void RemovePet(PetData pet)
    {
        foreach (PetData item in PetDatas)
        {
            if (item.Name == pet.Name)
            {
                petDatas.Remove(item);
                return;
            }
        }
    }

    public void AddPetDatasInWorld()
    {
        //foreach pet in list of pets, instantiate pet in world
        foreach (PetData pet in PetDatas)
        {
            Pet newpet = Instantiate<Pet>(Resources.Load<Pet>("Prefab/Pets/Pet"), SceneManager.GetSceneByName("GameScene").GetRootGameObjects()[0].transform);
            newpet.SetData(pet);
        }
    }
}
