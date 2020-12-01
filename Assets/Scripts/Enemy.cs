using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyDeath; 
    private const float SPEED = 5f;
    private Rigidbody rb;
    private Transform targetTransform;
    private bool isAlive = true;

    private void MoveTowardsTarget()
    {
        transform.LookAt(targetTransform);
        rb.AddRelativeForce(Vector3.forward * SPEED, ForceMode.Force);
    }

    private void Die()
    {
        if(rb.position.y <= -1)
        {
            Destroy(this.gameObject);
            OnEnemyDeath?.Invoke();
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
    }
}
