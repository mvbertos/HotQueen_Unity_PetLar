using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputs))]
public class PlayerTools : MonoBehaviour
{

    private PlayerInputs playerInputs;

    [SerializeField] private PlayerDragObject playerDragObject;

    private void Start()
    {
        //find references
        playerInputs = this.gameObject.GetComponent<PlayerInputs>();
    }

}
