using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyDeath; 
    private const float MOVING_FORCE = 15f;
    private Rigidbody rb;
    private Transform targetTransform;
    private bool isAlive = true;

    private void MoveTowardsTarget()
    {
        //Vector3 direction = targetTransform.position - transform.position;
        //direction = direction.normalized;
        //rb.velocity = new Vector3(direction.x * MOVING_FORCE, rb.velocity.y, direction.z * MOVING_FORCE);
        transform.LookAt(targetTransform);
        rb.AddRelativeForce(Vector3.forward * MOVING_FORCE, ForceMode.Force);
    }

    private void Die()
    {
        if (rb.position.y < 0.5f)
        {
            OnEnemyDeath?.Invoke();
            isAlive = false;
        }
    }

    private void StopFalling()
    {
        if (rb.position.y <= -20 && rb.useGravity == true)
        {
            rb.useGravity = false;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetTransform = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (isAlive)
        {
            MoveTowardsTarget();
            Die();
        }
        else
        {
            StopFalling();
        }
    }
}
