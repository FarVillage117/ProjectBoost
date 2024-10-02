using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public enum RocketState { Idle, Flying, Crashed };
    [SerializeField] RocketState state = RocketState.Idle;
    Rigidbody rb;

    [SerializeField] private float thrustForce = 10f;
    [SerializeField] private float rotationSpeed = 300f;

    [SerializeField] private int initialScore = 5; 
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", initialScore);
        }

        UpdateScoreText();
    }

    void Update()
    {
        switch (state)
        {
            case RocketState.Idle:
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

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                ReduceScore();
                break;
            case "LandingPad":
                ResetScore(); 
                LoadNextLevel();
                break;
            default:
                break;
        }
    }

    void ReduceScore()
    {
        int currentScore = PlayerPrefs.GetInt("Score");

        if (currentScore > 1)
        {
            currentScore--; 
            PlayerPrefs.SetInt("Score", currentScore);
            UpdateScoreText();
            ReloadLevel();
        }
        else
        {
            ResetGame();
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    void ResetScore()
    {
        PlayerPrefs.SetInt("Score", initialScore); 
        UpdateScoreText();
    }

    void ResetGame()
    {
        PlayerPrefs.SetInt("Score", initialScore);
        SceneManager.LoadScene(0);
    }

    void UpdateScoreText()
    {
        int currentScore = PlayerPrefs.GetInt("Score");
        scoreText.text = "Score: " + currentScore;
    }
}
