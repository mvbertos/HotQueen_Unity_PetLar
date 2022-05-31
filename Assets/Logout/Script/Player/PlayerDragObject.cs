using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDragObject : MonoBehaviour
{
    public bool dragging { private set; get; }
    [SerializeField] private float dragSpeed = 3.5f;
    private Rigidbody2D rigidbodyDragged = null;
    private DefaultRigidBody defaultRigidBody = new DefaultRigidBody();

    public class DefaultRigidBody
    {
        public float drag = 0;
        public RigidbodyType2D bodyType = RigidbodyType2D.Dynamic;
    }

    private void Update()
    {
        if (dragging)
        {
            DragObjectToTarget(rigidbodyDragged, (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        }
    }

    public void ReleaseObject()
    {
        dragging = false;
        rigidbodyDragged.drag = defaultRigidBody.drag;
        rigidbodyDragged.bodyType = defaultRigidBody.bodyType;
        rigidbodyDragged = null;
    }

    public void GrabObject(Rigidbody2D value)
    {
        if (value != null)
        {
            dragging = true;
            rigidbodyDragged = value;
            defaultRigidBody = new DefaultRigidBody();

            //body type
            defaultRigidBody.bodyType = rigidbodyDragged.bodyType;
            rigidbodyDragged.bodyType = RigidbodyType2D.Dynamic;
            
            //drag
            defaultRigidBody.drag = rigidbodyDragged.drag;
            rigidbodyDragged.drag = 0;
        }
        else
        {
            ReleaseObject();
        }
    }

    private void DragObjectToTarget(Rigidbody2D objectToDrag, Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - (Vector2)objectToDrag.transform.position;
        objectToDrag.velocity = direction * dragSpeed;
    }

}
