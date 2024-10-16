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
    [SerializeField] private ParticleSystem thrusterParticles;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            
            if (!thrusterParticles.isPlaying)
            {
                thrusterParticles.Play();
            }
        }
        else
        {
            thrusterParticles.Stop();
        }
    }

    void ProcessRotate()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeTorque(Vector3.forward * rotationAmount);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeTorque(-Vector3.forward * rotationAmount);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            score--;
            UpdateScoreText();
            if (score <= 0)
            {
                ReloadLevel(); // Reload level if score drops to 0
            }
        }
        else if (collision.gameObject.CompareTag("LandingPad"))
        {
            score = 5; // Reset score when landing on the pad
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void ReloadLevel()
    {
        // Add scene reloading logic here
    }
}
