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

    //SCREEN REFERENCES
    [SerializeField] private MinigameScreen MinigameScreen;

    private void Start()
    {
        playerInputs.OnSeventhShortcut += EnableMinigameScreen;
        playerInputs.OnFifthShortcut += ChangeToolToGrab;
        playerInputs.OnSecondShortcut += ChangeToolToTrigger;
    }

    private void ChangeToolToTrigger()
    {
        playerInputs.mouseRole = MouseRole.Trigger;
    }

    private void ChangeToolToGrab()
    {
        playerInputs.mouseRole = MouseRole.Drag;
    }

    private void EnableMinigameScreen()
    {
        MinigameScreen.gameObject.SetActive(!MinigameScreen.isActiveAndEnabled);
    }
}