using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stamp : MonoBehaviour
{
    [SerializeField] private GameObject StampPrefab;
    [SerializeField] private Transform StampPoint;
    [SerializeField] private bool approve;

    public void Use()
    {
        //verify for intersection with the stamp
        //if hit instantiate stampprefab at the position of the stamp
        //else do nothing
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));

        GameObject stamp = Instantiate(StampPrefab, StampPoint.position, StampPoint.rotation);

        foreach (var hit in hits)
        {
            //if is a movable object 
            //set instance parent as child of the object
            if (hit.rigidbody && hit.rigidbody.gameObject != this.gameObject)
            {
                GameObject parent = hit.rigidbody.gameObject;
                stamp.transform.parent = parent.transform;

                //if parent is a AdoptionDocument 
                //set document as aproved
                if (parent.GetComponent<AdoptionDocument>())
                {
                    AdoptionDocument document = parent.GetComponent<AdoptionDocument>();
                    document.Approved = approve;
                }
                return;
            }
        }

    }
}

