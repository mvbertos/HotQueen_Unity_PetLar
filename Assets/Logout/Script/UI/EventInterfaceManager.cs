using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EventInterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject event_interface;

    //Event DATA
    [SerializeField] private TMP_Text event_description;
    [SerializeField] private Image event_image;
    public Action OnEventAccepted;
    public Action OnEventDeclined;

    [SerializeField] private Button acceptButton;
    [SerializeField] private Button declineButton;
    public bool IsActive { get { return event_interface.activeInHierarchy; } }

    public void Enable(String text, Sprite image, Action OnAccept = null, Action OnDecline = null)
    {
        event_description.text = text;
        event_image.sprite = image;

        acceptButton.interactable = true;
        declineButton.interactable = true;

        acceptButton.onClick.AddListener(delegate
        {
            OnAccept?.Invoke();
            Disable();
        });

        declineButton.onClick.AddListener(delegate
        {

            OnDecline?.Invoke();
            Disable();
        });

        event_interface.SetActive(true);
    }
    public void Disable()
    {
        declineButton.onClick.RemoveAllListeners();
        acceptButton.onClick.RemoveAllListeners();
        event_interface.SetActive(false);
    }
}
