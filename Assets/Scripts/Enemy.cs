using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyDeath; 
    private const float MOVING_FORCE = 15f;
    private Rigidbody rb;
    private List<Transform> targetTransform = new List<Transform>();
    private List<float> targetDistance = new List<float>();
    private bool isAlive = true;

    private void MoveTowardsTarget()
    {
        Vector3 direction = targetTransform[CalculateMinimumDistanceIndex()].position - transform.position;
        direction = direction.normalized;
        rb.AddForce(direction * MOVING_FORCE, ForceMode.Force);
    }

    private int CalculateMinimumDistanceIndex()
    {
        targetDistance.Clear();
        for (int i=0; i < targetTransform.Count; i++)
        {
            if (targetTransform[i].position.y > 0.5f)
            {
                targetDistance.Add((targetTransform[i].position - transform.position).sqrMagnitude);
            }
            else
            {
                targetDistance.Add(Mathf.Infinity);
            }
        }
        return targetDistance.IndexOf(targetDistance.Min());
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
        targetTransform.Add(GameObject.Find("Player").transform);
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy != this.gameObject)
            {
                targetTransform.Add(enemy.transform);
            }
        }
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
