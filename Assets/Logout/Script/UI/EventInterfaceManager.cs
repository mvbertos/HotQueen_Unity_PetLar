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

    public void Enable(String text, Sprite image, Action OnAccept = null, Action OnDecline = null)
    {
        event_description.text = text;
        event_image.sprite = image;

        //Register actions on OnEventDeclined
        OnEventDeclined += () =>
        {
            OnDecline?.Invoke();
            Disable();
        };

        //Register actions on OnEventAccepted
        OnEventAccepted += () =>
        {
            OnAccept?.Invoke();
            Disable();
        };

        acceptButton.onClick.AddListener(delegate { OnEventAccepted(); });
        declineButton.onClick.AddListener(delegate { OnEventDeclined(); });

        event_interface.SetActive(true);
    }
    public void Disable()
    {
        OnEventAccepted = null;
        OnEventDeclined = null;
        event_interface.SetActive(false);
    }
}
