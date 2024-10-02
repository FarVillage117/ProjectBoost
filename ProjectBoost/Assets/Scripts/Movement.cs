using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float thrustForce = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private TMP_Text scoreText;
    private int score;

    void Start()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            score = 1;
        }
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
            ReduceScoreAndRespawn();
        }
        else if (collision.gameObject.CompareTag("LandingPad"))
        {
            ResetGame();
        }
    }

    void ReduceScoreAndRespawn()
    {
        if (score > 1)
        {
            score--;
            PlayerPrefs.SetInt("Score", score);
            UpdateScoreText();
            FindObjectOfType<FadeManager>().FadeOutAndReload();
        }
        else
        {
            ResetGame();
        }
    }

    void ResetGame()
    {
        score = 1;
        PlayerPrefs.SetInt("Score", score);
        UpdateScoreText();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
