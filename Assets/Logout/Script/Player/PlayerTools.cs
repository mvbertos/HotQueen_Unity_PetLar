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
        //register inputs
        playerInputs.OnMouseLeftDown += OnMouseLeftPressedCallback;
        playerInputs.OnMouseLeftUp += OnMouseLeftUpCallback;
        playerInputs.OnMouseRightDown += OnMouseRightClickCallback;
    }

    private void OnMouseLeftUpCallback()
    {
        if (playerDragObject.dragging)
        {
            playerDragObject.ReleaseObject();
        }
    }

    private void OnMouseRightClickCallback()
    {
        throw new NotImplementedException();
    }

    private void OnMouseLeftPressedCallback()
    {
        Ray mousePosition = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.GetRayIntersection(mousePosition);

        if (hit.rigidbody)
        {
            switch (playerInputs.mouseRole)
            {
                case MouseRole.Drag:
                    Debug.Log(hit.rigidbody.name);
                    playerDragObject.GrabObject(hit.rigidbody);
                    break;
                case MouseRole.Trigger:
                    //if pet
                    TryPet(hit.rigidbody);
                    //if pot
                    TryFillPot(hit.rigidbody);
                    break;
                default:
                    break;
            }
        }
    }

    private void TryStopPet(Rigidbody2D otherRigidbody)
    {
        if (otherRigidbody.TryGetComponent<PetThePet>(out PetThePet _pet))
        {
            _pet.StopPet();
        }
    }

    private void TryPet(Rigidbody2D otherRigidbody)
    {
        //Pet
        if (otherRigidbody.TryGetComponent<PetThePet>(out PetThePet _pet))
        {
            _pet.StartPet();
        }
    }

    private void TryFillPot(Rigidbody2D otherRigidbody)
    {
        //FillPot
        if (otherRigidbody.TryGetComponent<FoodPot>(out FoodPot pot))
        {
            pot.FillPot();
        }
    }
}
