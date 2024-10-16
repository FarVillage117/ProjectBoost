using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 2f;

    void Update()
    {
        float angle = Mathf.Sin(Time.time * spinSpeed) * 360f;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
