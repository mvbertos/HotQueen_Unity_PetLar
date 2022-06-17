using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBox : MonoBehaviour
{
    private Action OnUpdate;
    void OnTriggerEnter2D(Collider2D other)
    {

        OnUpdate += () =>
        {
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
                    }
                    else
                    {
                        document.Reject();
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
