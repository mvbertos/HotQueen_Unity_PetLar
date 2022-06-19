using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Shortcuts are bade to make quicker interaction in game, preventing the player to use the mouse when the time is not he best
/// </summary>
public class PlayerShortcuts : MonoBehaviour
{
    //PLAYER REFERENCES
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private PlayerMouse playerMouse;

    //SCREEN REFERENCES

    private void Start()
    {
        playerInputs.OnSeventhShortcut += EnableMinigameScreen;
        playerInputs.OnEighthShortcut += EnableStoreScreen;
        playerInputs.OnFirstShortcut += ChangeToolToGrab;
        playerInputs.OnSecondShortcut += ChangeToolToTrigger;
    }

    private void EnableStoreScreen()
    {
        StoreManager storeManager = FindObjectOfType<StoreManager>();
        storeManager.Show();
    }

    private void ChangeToolToTrigger()
    {
        playerMouse.SetMouseRole(PlayerMouse.MouseRole.Trigger);
    }

    private void ChangeToolToGrab()
    {
        playerMouse.SetMouseRole(PlayerMouse.MouseRole.Drag);
    }

    private void EnableMinigameScreen()
    {
        TrellowInterface trellowInterface = FindObjectOfType<TrellowInterface>();
        trellowInterface.Show();
    }
}
