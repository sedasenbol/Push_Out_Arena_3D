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

    private void Update()
    {
        if (player.transform.position.y > 0) // you never check whether the player is null or not. 
        {
            // not caching the transform and player.transform is inefficient
            transform.position = player.transform.position + new Vector3(0f, 9f, -30f); // this is a magic number. You should make this a parameter.
        }
    }
}
