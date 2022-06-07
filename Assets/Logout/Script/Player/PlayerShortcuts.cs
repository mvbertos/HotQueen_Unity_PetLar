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
    [SerializeField] private TrellowInterface trellowInterface;
    [SerializeField] private StoreManager storeInterface;

    private void Start()
    {
        playerInputs.OnSeventhShortcut += EnableMinigameScreen;
        playerInputs.OnEighthShortcut += EnableStoreScreen;
        playerInputs.OnFirstShortcut += ChangeToolToGrab;
        playerInputs.OnSecondShortcut += ChangeToolToTrigger;
    }

    private void EnableStoreScreen()
    {
        storeInterface.Show();
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
        trellowInterface.Show();
    }
}
