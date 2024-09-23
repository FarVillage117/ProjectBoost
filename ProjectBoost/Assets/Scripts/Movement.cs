using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float thrustForce = 10f; // Control the thrust force
    [SerializeField] private float rotationSpeed = 100f; // Control the rotation speed

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustForce); // Apply thrust force
        }
    }

    void ProcessRotate()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime; // Calculate the rotation amount

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeTorque(Vector3.forward * rotationAmount); // Rotate left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeTorque(-Vector3.forward * rotationAmount); // Rotate right
        }
    }
}