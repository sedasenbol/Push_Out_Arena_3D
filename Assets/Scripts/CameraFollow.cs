using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 offset = new Vector3(0f, 9f, -30f);

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (player && player.position.y > 0)
        {
            transform.position = player.position + offset;
        }
    }
}
