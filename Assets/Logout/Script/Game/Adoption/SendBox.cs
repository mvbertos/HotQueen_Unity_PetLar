using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBox : MonoBehaviour
{
    private Action OnUpdate;
    [SerializeField] private AudioClip Clip_Adop;
    [SerializeField] private AudioClip Clip_happyDog;
    [SerializeField] private AudioClip Clip_happyCat;
    [SerializeField] private AudioClip Clip_Reject;

    void OnTriggerEnter2D(Collider2D other)
    {

        OnUpdate += () =>
        {
            AudioPlayer audioPlayer = FindObjectOfType<AudioPlayer>();
            //if other has a ridgidbody and a componend of type AdoptionDocument
            //verify if rigidbody is Static 
            //if is static, set document as aproved
            //else set document as denied
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<AdoptionDocument>(out AdoptionDocument document))
            {
                if (other.attachedRigidbody.bodyType == RigidbodyType2D.Static)
                {
                    if (document.Approved)
                    {
                        document.Adopt();
                        audioPlayer.PlayAudio(Clip_Adop, false);
                        if (document.Pet.isDog)
                        {
                            audioPlayer.PlayAudio(Clip_happyDog, false);
                        }
                        else
                        {
                            audioPlayer.PlayAudio(Clip_happyCat, false);
                        }
                    }
                    else
                    {
                        document.Reject();
                        audioPlayer.PlayAudio(Clip_Reject, false);
                    }
                }
            }
        };
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnUpdate = null;
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }
}
