using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScreen : MonoBehaviour
{
    [System.Serializable]
    private struct screens
    {
        public GameObject adoption;
    }

    [SerializeField] private Button button_ref;
    [SerializeField] private Transform content;
    [SerializeField] private screens minigame_screens;

    private void OnEnable()
    {
        InitMinigamesButton();
    }

    private void OnDisable()
    {
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }
    }

    private void InitMinigamesButton()
    {
        //Init Pet Adoption button
        Button button = Instantiate(button_ref, content);
        button.onClick.AddListener(delegate { Instantiate(minigame_screens.adoption); });
    }
}
