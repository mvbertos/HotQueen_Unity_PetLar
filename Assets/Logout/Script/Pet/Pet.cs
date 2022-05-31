using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Pet : MonoBehaviour
{
    [SerializeField] private PetData data;
    [SerializeField] private InformationDisplayer informationDisplayer;

    private void Start()
    {
        informationDisplayer.UpdateInformation(data.Name, 100);
    }
    
    public void ChangeName(String newName){
        data.Name = newName;
    }
    
    public PetData GetData(){
        return data;
    } 
}
