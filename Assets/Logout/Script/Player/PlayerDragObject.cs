using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDragObject : MonoBehaviour
{
    public bool dragging { private set; get; }
    private Rigidbody2D rigidbodyDragged = null;
    private Rigidbody2D defaultRigidbody = null;

    private void Update()
    {
        if (dragging)
        {
            DragObjectToTarget(rigidbodyDragged.transform, (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        }
    }

    public void ReleaseObject()
    {
        dragging = false;
        rigidbodyDragged = null;
        defaultRigidbody = null;
    }

    public void GrabObject(Rigidbody2D value)
    {
        if (value != null)
        {
            dragging = true;
            rigidbodyDragged = value;
            defaultRigidbody = value;
            value.isKinematic = true;
        }
        else
        {
            Debug.Log("object is null");
            ReleaseObject();
        }
    }

    private void DragObjectToTarget(Transform value, Vector2 targetPosition)
    {
        value.position = targetPosition;
    }

}
