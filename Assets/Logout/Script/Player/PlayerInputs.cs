using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    //INPUT CONTROLS 
    private WindowsInputs m_playerInputs;
    private WindowsInputs.PlayerControlsActions playerControls;

    //Layers
    [SerializeField] private LayerMask layerInteraction;

    //MOUSE CALLBACKS
    public Action OnMouseLeftDown;
    public Action OnMouseRightDown;
    public Action OnMouseLeftUp;
    public Action OnMouseRightUp;

    // SHORTCUT CALLBACKS
    public Action OnFirstShortcut;
    public Action OnSecondShortcut;
    public Action OnThirdShortcut;
    public Action OnForthShortcut;
    public Action OnFifthShortcut;
    public Action OnSixthhortcut;
    public Action OnSeventhShortcut;
    public Action OnEighthShortcut;

    private void Awake()
    {
        m_playerInputs = new WindowsInputs();
    }

    private void OnEnable()
    {
        m_playerInputs.Enable();
        EnablePlayerControls();
    }

    private void OnDisable()
    {
        m_playerInputs.Disable();
    }

    private void EnablePlayerControls()
    {
        playerControls = m_playerInputs.PlayerControls;
        //enable keyboard
        playerControls.Shortcuts.performed += OnShortcutsPressedCallback;

        //enable mouse
        playerControls.Mouse.performed += OnMousePressedCallback;
        playerControls.Mouse.canceled += OnMouseUpCallback;
    }

    private void OnMouseUpCallback(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() == 0)
        {
            //left click
            OnMouseLeftUp?.Invoke();
        }
        else if (obj.ReadValue<float>() == 1)
        {
            //right click
            OnMouseRightUp?.Invoke();
        }
    }

    private void OnMousePressedCallback(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() == 0)
        {
            //left click
            OnMouseLeftDown?.Invoke();
        }
        else if (obj.ReadValue<float>() == 1)
        {
            //right click
            OnMouseRightDown?.Invoke();
        }
    }



    #region INPUT CALLBACKS

    /// <summary>
    /// Handle shortcut action on pressed
    /// </summary>
    /// <param name="obj"></param>
    private void OnShortcutsPressedCallback(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<float>();
        switch (value)
        {
            case 1:
                OnFirstShortcut?.Invoke();
                break;
            case 2:
                OnSecondShortcut?.Invoke();
                break;
            case 3:
                OnThirdShortcut?.Invoke();
                break;
            case 4:
                OnForthShortcut?.Invoke();
                break;
            case 5:
                OnFifthShortcut?.Invoke();
                break;
            case 6:
                OnSixthhortcut?.Invoke();
                break;
            case 7:
                OnSeventhShortcut?.Invoke();
                break;
            case 8:
                OnEighthShortcut?.Invoke();
                break;
            default:
                Debug.LogWarning("Shortcut not registered!");
                break;
        }

    }

    #endregion


    public void Interact(Vector2 position, Action<RaycastHit2D> OnInteract, Vector2 direction = default(Vector2))
    {
        //raycast to interact with objects
        Ray ray = new Ray(position, direction);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 100, layerInteraction);

        OnInteract?.Invoke(hit);
    }
    public void Interact(Ray ray, Action<RaycastHit2D> OnInteract)
    {
        Interact(ray.origin, OnInteract, ray.direction);
    }
}