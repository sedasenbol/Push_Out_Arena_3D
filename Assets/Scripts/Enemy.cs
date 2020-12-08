using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Enemy : MonoBehaviour
{
    private const float MOVING_FORCE = 15f;
    private Rigidbody rb;
    private List<Transform> targetTransforms = new List<Transform>();
    private List<float> targetDistances = new List<float>();
    private bool isAlive = true;

    public static event Action OnEnemyDeath;

    private void MoveTowardsTarget()
    {
        Vector3 direction = targetTransforms[CalculateMinimumDistanceIndex()].position - transform.position;
        direction = direction.normalized;
        float forceRandomizer = UnityEngine.Random.Range(0.5f, 1.5f);
        rb.AddForce(direction * MOVING_FORCE * forceRandomizer, ForceMode.Force);
    }

    private int CalculateMinimumDistanceIndex()
    {
        targetDistances.Clear();
        for (int i=0; i < targetTransforms.Count; i++)
        {
            if (targetTransforms[i].position.y > 0.5f)
            {
                targetDistances.Add((targetTransforms[i].position - transform.position).sqrMagnitude);
            }
            else
            {
                targetDistances.Add(Mathf.Infinity);
            }
        }
        return targetDistances.IndexOf(targetDistances.Min());
    }

    private void CheckIfEnemyIsDead()
    {
        if (rb.position.y < 0.5f)
        {
            targetTransforms.Clear();
            targetDistances.Clear();
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
        targetTransforms.Add(GameObject.Find("Player").transform);
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy != this.gameObject)
            {
                targetTransforms.Add(enemy.transform);
            }
        }
    }

    private void Update()
    {
        if (isAlive)
        {
            MoveTowardsTarget();
            CheckIfEnemyIsDead();
        }
        else
        {
            StopFalling();
        }
    }
}
