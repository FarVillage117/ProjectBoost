using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    public enum RocketState { Idle, Flying, Crashed };
    [SerializeField] RocketState state = RocketState.Idle;
    Rigidbody rb;

    [SerializeField] private float thrustForce = 10f;
    [SerializeField] private float rotationSpeed = 100f;

    [SerializeField] private int score = 5;
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateScoreText();
    }

    void Update()
    {
        switch (state)
        {
            case RocketState.Idle:
                ProcessThrust();
                ProcessRotate();
                break;

            case RocketState.Flying:
                ProcessThrust();
                ProcessRotate();
                break;

            case RocketState.Crashed:
                break;
        }
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustForce);
            state = RocketState.Flying;
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
            state = RocketState.Crashed;
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
