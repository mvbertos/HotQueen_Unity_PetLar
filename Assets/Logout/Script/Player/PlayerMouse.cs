using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    public Texture2D mouseSprite;
    public LayerMask petLayerMask;
    private Rigidbody2D objectBeingDragged;
    private Dictionary<string, Rigidbody2D> dragged_rigidbodies = new Dictionary<string, Rigidbody2D>();


    private void Update()
    {
        Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(mousePosition);

        if (hit.rigidbody)
        {
            //Grab rigidbody object
            if (Input.GetMouseButtonDown(0))
            {

                DragObject(hit.transform.name, hit.rigidbody);
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (hit.rigidbody.TryGetComponent<PetThePet>(out PetThePet _pet))
                {
                    _pet.StartPet();
                    Debug.Log(hit.collider.name);
                }
                else if (hit.rigidbody.TryGetComponent<FoodPot>(out FoodPot pot))
                {
                    pot.FillPot();
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                if (hit.rigidbody && hit.rigidbody.TryGetComponent<PetThePet>(out PetThePet _pet))
                {
                    _pet.StopPet();
                    Debug.Log(hit.collider.name);
                }
            }
        }


        //Drop rigidbody object
        if (objectBeingDragged)
        {
            objectBeingDragged.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonUp(0))
            {
                DropObject(objectBeingDragged.transform.name, objectBeingDragged);
            }
        }

    }

    private void DropObject(string objectName, Rigidbody2D value)
    {
        if (dragged_rigidbodies.ContainsKey(objectName) && dragged_rigidbodies.TryGetValue(objectName, out Rigidbody2D _rigidbody))
        {
            value = _rigidbody;
            objectBeingDragged = null;
            dragged_rigidbodies.Remove(objectName);
        }
    }

    private void DragObject(string objectName, Rigidbody2D value)
    {
        dragged_rigidbodies.Add(objectName, value);
        value.isKinematic = true;
        objectBeingDragged = value;
    }
}
