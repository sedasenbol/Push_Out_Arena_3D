using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    private void MoveWithInput()
    {
        if (Input.GetAxis("Horizontal") == 1)
        {

        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveWithInput();
    }
}
