using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Player : MonoBehaviour
{
    private const float MOVING_SPEED = 20f;
    private Vector2 beginningDragPosition;
    private Vector2 currentDragPosition;
    private Rigidbody rb;
    [SerializeField]
    private Camera cam;
    private bool isAlive = true;

    public static event Action OnPlayerDeath;

    private void OnEnable()
    {
        TouchController.OnBeginDragMovement += SetBeginningDragPosition;
        TouchController.OnDragMovement += MoveWhileDragging;
        TouchController.OnEndDragMovement += StopMoving;
    }

    private void OnDisable()
    {
        TouchController.OnBeginDragMovement -= SetBeginningDragPosition;
        TouchController.OnDragMovement -= MoveWhileDragging;
        TouchController.OnEndDragMovement -= StopMoving;
    }

    private void SetBeginningDragPosition(Vector2 position)
    {
        beginningDragPosition = position;
    }

    private void StopMoving(Vector2 position)
    {
        rb.velocity = new Vector3(0, 0, 0);
    }

    private void MoveWhileDragging(Vector2 position)
    {
        if (isAlive)
        {
            currentDragPosition = position;
            Vector2 direction = currentDragPosition - beginningDragPosition;
            Vector3 movingDirection = new Vector3(direction.x, rb.velocity.y, direction.y);
            rb.velocity = movingDirection.normalized * MOVING_SPEED;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 begin = new Vector3(beginningDragPosition.x, beginningDragPosition.y, cam.WorldToScreenPoint(transform.position).z);
        begin = cam.ScreenToWorldPoint(begin);
        
        Vector3 end = new Vector3(currentDragPosition.x, currentDragPosition.y, cam.WorldToScreenPoint(transform.position).z);
        end = cam.ScreenToWorldPoint(end);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(begin, end);
    }

    private void Die()
    {
        if(rb.position.y < 0.5f)
        {
            OnPlayerDeath?.Invoke();
            isAlive = false;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isAlive)
        {
            Die();
        }
    }
}
