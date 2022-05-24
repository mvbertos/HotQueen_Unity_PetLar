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
        InitButtons();
    }

    private void OnDisable()
    {
        ClearButtons();
    }

    /// <summary>
    /// It clears buttons child of content
    /// </summary>
    private void ClearButtons()
    {
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }
    }

    /// <summary>
    /// Init buttons to trigger the minigames when later pressed
    /// </summary>
    private void InitButtons()
    {
        //Init Pet Adoption button
        Button button = Instantiate(button_ref, content);
        button.onClick.AddListener(delegate { Instantiate(minigame_screens.adoption); });
    }
}
