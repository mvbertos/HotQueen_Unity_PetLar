using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Pet : MonoBehaviour
{
    [SerializeField] private EntityData data = new EntityData("", null, 0);
    public EntityData Data
    {
        private set
        {
            data = value;
        }
        get
        {
            return data;
        }
    }
    public PetSpecies Specie;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text petNameTMP;

    private void Start()
    {
        UpdatePet();
    }

    public void UpdatePerfil(string name, Sprite sprite, EntityData.Personalities personality)
    {
        Data = new EntityData(name, sprite, personality);
    }

    private void UpdatePet()
    {
        petNameTMP.text = Data.Name;
        spriteRenderer.sprite = Data.Picture;
    }
}
public enum PetSpecies
{
    DEFAULT = 0,
    POODLE = 1,
    GOLDENRETRIVER = 2,
}