using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovimentation : MonoBehaviour
{
    private NavMeshAgent agent;
    private Action OnUpdate;
    public delegate void AIInteraction(RaycastHit2D hit);

    private void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        OnUpdate?.Invoke();
    }

    public void SetDestination(Vector2 position, Action OnCloseToTarget = null)
    {
        agent.SetDestination((Vector3)position);
        //print(Vector2.Distance(this.transform.position, target.position));
        if (Vector2.Distance(this.transform.position, position) <= 0.2f)
        {
            OnCloseToTarget?.Invoke();
        }
    }

    public void SetDestination(Transform target, Action OnCloseToTarget = null)
    {
        OnUpdate = () =>
        {
            SetDestination(target.position, OnCloseToTarget);
        };
    }
    public void GoInteract(Transform target, AIInteraction OnInteract, LayerMask interactionLayer)
    {
        OnUpdate = () =>
        {
            SetDestination(target.position);
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, target.position - this.transform.position, 1, interactionLayer);
            if (hit.collider)
            {
                OnInteract?.Invoke(hit);
                OnUpdate = null;
            }
        };
    }
}
