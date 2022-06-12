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

    private void Start()
    {
        mouseRole = MouseRole.Drag;
        mouseState = MouseState.Idle;
        SetMouseState(mouseState);

        //register inputs
        playerInputs.OnMouseLeftDown += OnMouseLeftPressedCallback;
        playerInputs.OnMouseLeftUp += OnMouseLeftUpCallback;
        playerInputs.OnMouseRightDown += OnMouseRightClickCallback;
    }

    private void Update()
    {
        HoveringTrigger();
    }

    private void HoveringTrigger()
    {
        //check if mouse is hovering over an object
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
        SetMouseState(MouseState.Idle);
    }

    private void OnMouseLeftPressedCallback()
    {
        SetMouseState(MouseState.Clicked);
        Ray mousePosition = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        playerInputs.Interact(mousePosition, (hit) =>
        {
            if (hit)
            {
                switch (mouseRole)
                {
                    case MouseRole.Drag:
                        if (hit.rigidbody)
                        {
                            SetMouseState(MouseState.Dragging);
                            playerDragObject.GrabObject(hit.rigidbody);
                        }
                        break;
                    case MouseRole.Trigger:
                        //if pot
                        Interact(hit.collider.attachedRigidbody.gameObject);
                        break;
                    default:
                        break;
                }
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

