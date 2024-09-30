using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float thrustForce = 10f;
    [SerializeField] private float rotationSpeed = 100f;

    [SerializeField] private int score = 5;
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        
        rb.freezeRotation = true;

        UpdateScoreText();
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
            rb.AddRelativeForce(Vector3.up * thrustForce);
        }
    }

    
    void ProcessRotate()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationAmount);
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            ApplyRotation(-rotationAmount);
        }
    }

    
    void ApplyRotation(float rotationThisFrame)
    {
        
        rb.freezeRotation = true; 
        transform.Rotate(Vector3.forward * rotationThisFrame);
        rb.freezeRotation = false; 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            score--;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
