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

    [SerializeField] private AudioSource thrustAudio;
    [SerializeField] private AudioSource collisionAudio;
    [SerializeField] private AudioSource landingPadAudio;

    private int score;

    void Start()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            score = 5;
        }
        rb = GetComponent<Rigidbody>();
        UpdateScoreText();
        if (thrustAudio == null)
        {
            Debug.LogWarning("Thrust AudioSource has not been assigned.");
        }
        if (collisionAudio == null)
        {
            Debug.LogWarning("Collision AudioSource has not been assigned.");
        }
        if (landingPadAudio == null)
        {
            Debug.LogWarning("Landing Pad AudioSource has not been assigned.");
        }
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
            if (thrustAudio != null && !thrustAudio.isPlaying)
            {
                thrustAudio.Play();
            }
        }
        else
        {
            if (thrustAudio != null)
            {
                thrustAudio.Stop();
            }
        }
    }

    void ProcessRotate()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotating Left");
            transform.Rotate(0, -rotationAmount, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Rotating Right");
            transform.Rotate(0, rotationAmount, 0);
        }
        Debug.Log("Current Rotation: " + transform.rotation.eulerAngles);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (collisionAudio != null)
            {
                collisionAudio.Play();
            }
            ReduceScoreAndRespawn();
        }
        else if (collision.gameObject.CompareTag("LandingPad"))
        {
            if (landingPadAudio != null)
            {
                landingPadAudio.Play();
            }
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
        score = 5;
        PlayerPrefs.SetInt("Score", score);
        UpdateScoreText();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
