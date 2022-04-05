using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PetMovementation : MonoBehaviour
{

    public float speed;
    private Animator animator;
    [SerializeField] private Vector2 range;
    private Vector2 targetPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = GetNewTargetPosition();
    }


    private void Update()
    {
        if (Vector2.Distance((Vector2)this.transform.position, targetPosition) > 0.1)
        {
            Vector2 direction = (targetPosition - (Vector2)this.transform.position).normalized;
            Move(direction, speed);
        }
        else
        {
            Move(Vector2.zero, 0);
            targetPosition = GetNewTargetPosition();
        }
    }

    private Vector2 GetNewTargetPosition()
    {
        return GetRandomPosition();
    }

    private Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(0, range.x), Random.Range(0, range.y));
    }

    public void Move(Vector3 direction, float speed)
    {
        direction.Normalize();
        animator.SetBool("IsMoving", direction.magnitude > 0);

        //Animate
        if (direction.x <= -0.1)
        {
            animator.SetInteger("Direction", 3);
        }
        else if (direction.x >= 0.1)
        {
            animator.SetInteger("Direction", 2);
        }
        if (direction.y >= 0.1)
        {
            animator.SetInteger("Direction", 1);
        }
        else if (direction.y <= -0.1)
        {
            animator.SetInteger("Direction", 0);
        }

        GetComponent<Rigidbody2D>().velocity = speed * direction;
    }
}

