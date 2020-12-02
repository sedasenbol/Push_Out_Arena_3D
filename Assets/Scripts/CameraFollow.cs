using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player.transform.position.y > 0)
        {
            transform.position = player.transform.position + new Vector3(0f, 9f, -30f);
        }
    }
}
