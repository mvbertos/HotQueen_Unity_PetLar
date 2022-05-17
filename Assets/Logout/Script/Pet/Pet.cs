using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Pet : MonoBehaviour
{
    public EntityData petPerfil;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text petNameTMP;

    private void Start()
    {
        UpdatePet();
    }

    private void UpdatePet()
    {
        petNameTMP.text = petPerfil.Name;
        spriteRenderer.sprite = petPerfil.Picture;
    }
}
