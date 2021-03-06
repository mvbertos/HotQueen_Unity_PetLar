using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMouse : MonoBehaviour
{
    public enum MouseRole
    {
        Drag,
        Trigger,
    }
    public enum MouseState
    {
        Idle,
        Hovering,
        Dragging,
        Clicked,
    }
    [System.Serializable]
    public struct MouseCursor
    {
        public Texture2D Pressing;
        public Texture2D Hovering;
        public Texture2D Idle;
        public Texture2D Grabbing;
    }

    [SerializeField] private MouseCursor mouseCursor;
    [SerializeField] private MouseRole mouseRole;
    [SerializeField] private MouseState mouseState;
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private PlayerDragObject playerDragObject;
    [SerializeField] private AudioClip mouseClick;
    [SerializeField] private AudioClip mouseUp;

    private void Start()
    {
        mouseRole = MouseRole.Drag;
        mouseState = MouseState.Idle;
        SetMouseState(mouseState);

        //register inputs
        playerInputs.OnMouseLeftDown += OnMouseLeftPressedCallback;
        playerInputs.OnMouseLeftDown += MouseClickSound;
        playerInputs.OnMouseLeftUp += OnMouseLeftUpCallback;
        playerInputs.OnMouseLeftUp += MouseUpSound;
        playerInputs.OnMouseRightDown += OnMouseRightClickCallback;
        playerInputs.OnMouseRightDown += MouseClickSound;
        playerInputs.OnMouseRightUp += MouseUpSound;
    }

    private void MouseClickSound()
    {
        //find object of AudioPlayer
        //play mouseClick
        AudioPlayer audioPlayer = FindObjectOfType<AudioPlayer>();
        if (audioPlayer != null)
        {
            audioPlayer.PlayAudio(mouseClick, false);
        }
    }

    private void MouseUpSound()
    {
        //find object of AudioPlayer
        //play mouseClick
        AudioPlayer audioPlayer = FindObjectOfType<AudioPlayer>();
        if (audioPlayer != null)
        {
            audioPlayer.PlayAudio(mouseUp, false);
        }
    }

    private void Update()
    {
        HoveringTrigger();
    }

    private void HoveringTrigger()
    {
        //check if mouse is hovering over an object
        if (GameObject.FindObjectsOfType<Camera>().Length > 0)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics2D.GetRayIntersection(mouseRay, 10, playerInputs.LayerInteraction))
            {
                if (mouseState == MouseState.Idle)
                {
                    SetMouseState(MouseState.Hovering);
                }
            }
            else
            {
                if (mouseState == MouseState.Hovering)
                {
                    SetMouseState(MouseState.Idle);
                }
            }
        }

    }

    private void OnMouseRightClickCallback()
    {
        Ray mousePosition = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        playerInputs.Interact(mousePosition, (hit) =>
        {
            //if pot
            Interact(hit.collider.attachedRigidbody.gameObject);
        }
        );
    }

    private void OnMouseLeftUpCallback()
    {
        if (playerDragObject.dragging)
        {
            playerDragObject.ReleaseObject();
        }
        SetMouseState(MouseState.Idle);
    }

    private void OnMouseLeftPressedCallback()
    {
        SetMouseState(MouseState.Clicked);
        Ray mousePosition = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        playerInputs.Interact(mousePosition, (hit) =>
        {
            if (hit.rigidbody)
            {
                SetMouseState(MouseState.Dragging);
                playerDragObject.GrabObject(hit.rigidbody);
            }
        });
    }

    private void Interact(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<FoodPot>(out FoodPot pot))
        {
            pot.Fill();
        }
        else if (gameObject.TryGetComponent<Bathroom>(out Bathroom bathroom))
        {
            bathroom.Clean();
        }
        else if (gameObject.TryGetComponent<Stamp>(out Stamp stamp))
        {
            stamp.Use();
        }
    }

    public void SetMouseRole(MouseRole role)
    {
        mouseRole = role;
    }

    public void SetMouseState(MouseState state)
    {
        mouseState = state;
        SetMouseCursor(mouseState);
    }

    public void SetMouseCursor(MouseState state)
    {
        Vector2 offset = new Vector2(-10, -10);
        switch (state)
        {
            case MouseState.Idle:
                Cursor.SetCursor(mouseCursor.Idle, offset, CursorMode.Auto);
                break;
            case MouseState.Hovering:
                Cursor.SetCursor(mouseCursor.Hovering, offset, CursorMode.Auto);
                break;
            case MouseState.Dragging:
                Cursor.SetCursor(mouseCursor.Grabbing, offset, CursorMode.Auto);
                break;
            case MouseState.Clicked:
                Cursor.SetCursor(mouseCursor.Pressing, offset, CursorMode.Auto);
                break;
        }
    }

}

