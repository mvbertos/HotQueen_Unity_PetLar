using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stamp : MonoBehaviour
{
    [SerializeField] private GameObject StampPrefab;

    public void Use()
    {
        //verify for intersection with the stamp
        //if hit instantiate stampprefab at the position of the stamp
        //else do nothing
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (hit)
        {
            Instantiate(StampPrefab, hit.point, Quaternion.identity);
        }
    }
}
