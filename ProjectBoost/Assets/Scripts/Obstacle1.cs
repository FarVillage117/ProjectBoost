using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle1 : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float movementDistance = 5f;
    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, movementDistance * 2) - movementDistance;
        transform.position = new Vector3(startingPosition.x + movement, startingPosition.y, startingPosition.z);
    }
}
