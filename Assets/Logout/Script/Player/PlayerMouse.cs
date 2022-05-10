using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    public KeyCode key01 = KeyCode.Q;
    public KeyCode key02 = KeyCode.W;
    public KeyCode key03 = KeyCode.E;
    public MouseSpriteLibrary mouseSprite;
    public LayerMask petLayerMask;
    private Rigidbody2D objectBeingInteracted;
    private Dictionary<string, Rigidbody2D> dragged_rigidbodies = new Dictionary<string, Rigidbody2D>();

    public Action OnMouseLeftDown;
    public Action OnMouseLeftUp;
    public Action OnMouseRightDown;
    public Action OnMouseRightUp;
    public Action UpdateEvents;
    private void Start()
    {
        OnMouseLeftDown += () =>
        {
            //set cursor sprite to hold
            //Cursor.SetCursor(SpriteToTexture2D(mouseSprite.holdMouseSprite), new Vector2(0, 0), CursorMode.ForceSoftware);

            //cast ray to find a interactable gameobject
            Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(mousePosition);

            if (hit.rigidbody)
            {
                objectBeingInteracted = hit.rigidbody;

                switch (GameManager.instance.mouseRole)
                {
                    case MouseRole.Drag:
                        GrabObject(objectBeingInteracted.name, objectBeingInteracted);
                        break;
                    case MouseRole.Trigger:
                        //if pet
                        TryPet(objectBeingInteracted);
                        //if pot
                        TryFillPot(objectBeingInteracted);
                        break;
                    default:
                        break;
                }
            }

        };

        OnMouseRightDown += () =>
        {
            //set cursor sprite to hold
            // Cursor.SetCursor(SpriteToTexture2D(mouseSprite.holdMouseSprite), new Vector2(0, 0), CursorMode.ForceSoftware);

            if (objectBeingInteracted)
            {
                TryStopPet(objectBeingInteracted);
            }
        };

        OnMouseLeftUp += () =>
        {
            // Cursor.SetCursor(SpriteToTexture2D(mouseSprite.mouseSprite), new Vector2(0, 0), CursorMode.ForceSoftware);

            if (objectBeingInteracted)
                ReleaseObject(objectBeingInteracted.transform.name, objectBeingInteracted);
        };

        OnMouseRightUp += () =>
        {
            //Cursor.SetCursor(SpriteToTexture2D(mouseSprite.mouseSprite), new Vector2(0, 0), CursorMode.ForceSoftware);
        };

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnMouseLeftDown?.Invoke();
        if (Input.GetMouseButtonUp(0))
            OnMouseLeftUp?.Invoke();

        if (Input.GetMouseButtonDown(1))
            OnMouseRightDown?.Invoke();
        if (Input.GetMouseButtonUp(1))
            OnMouseRightUp?.Invoke();

        UpdateEvents?.Invoke();

        //change mouse role
        if (Input.GetKeyDown(key01))
        {
            GameManager.instance.mouseRole = MouseRole.Drag;
        }
        else if (Input.GetKeyDown(key02))
        {
            GameManager.instance.mouseRole = MouseRole.Trigger;
        }
        else if (Input.GetKeyDown(key03))
        {
            Debug.Log("Role not implemented yet");
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

    public Texture2D SpriteToTexture2D(Sprite sprite)
    {
        Texture2D croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                        (int)sprite.textureRect.y,
                                        (int)sprite.textureRect.width,
                                        (int)sprite.textureRect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();
        return croppedTexture;
    }


    private void ReleaseObject(string objectName, Rigidbody2D value)
    {
        if (dragged_rigidbodies.ContainsKey(objectName) && dragged_rigidbodies.TryGetValue(objectName, out Rigidbody2D _rigidbody))
        {
            value = _rigidbody;
            UpdateEvents -= OnGrabCallback;
            dragged_rigidbodies.Remove(objectName);
        }
    }

    private void GrabObject(string objectName, Rigidbody2D value)
    {
        UpdateEvents += OnGrabCallback;

        dragged_rigidbodies.Add(objectName, value);
        value.isKinematic = true;
    }

    private void OnGrabCallback()
    {
        DragObjectToTarget(objectBeingInteracted.transform, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void DragObjectToTarget(Transform value, Vector2 targetPosition)
    {
        value.position = targetPosition;
    }
}

[System.Serializable]
public struct MouseSpriteLibrary
{
    public Sprite mouseSprite;
    public Sprite ClickMouseSprite;
    public Sprite holdMouseSprite;
}