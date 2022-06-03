using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputs))]
public class PlayerTools : MonoBehaviour
{

    private PlayerInputs playerInputs;
    [SerializeField] private LayerMask layerInteraction;
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

    private void OnMouseRightClickCallback()
    {
        Debug.Log("Nothing implemented");
    }

    private void OnMouseLeftUpCallback()
    {
        if (playerDragObject.dragging)
        {
            playerDragObject.ReleaseObject();
        }
    }

    private void OnMouseLeftPressedCallback()
    {
        Ray mousePosition = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.GetRayIntersection(mousePosition, 100, layerInteraction);

        if (hit)
        {
            switch (playerInputs.mouseRole)
            {
                case MouseRole.Drag:
                    if (hit.rigidbody)
                    {
                        playerDragObject.GrabObject(hit.rigidbody);
                    }
                    break;
                case MouseRole.Trigger:
                    //if pot
                    Interact(hit.transform.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    private void Interact(GameObject otherRigidbody)
    {
        //FillPot
        if (otherRigidbody.TryGetComponent<FoodPot>(out FoodPot pot))
        {
            pot.Fill();
        }
        else if (otherRigidbody.TryGetComponent<Bathroom>(out Bathroom bathroom))
        {
            bathroom.Clean();
        }
    }
}
