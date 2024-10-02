using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeOutAndReload()
    {
        StartCoroutine(FadeOutAndReloadScene());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color imageColor = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = imageColor;
            yield return null;
        }
    }

    IEnumerator FadeOutAndReloadScene()
    {
        float elapsedTime = 0f;
        Color imageColor = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = imageColor;
            yield return null;
        }

        
        PlayerPrefs.SetInt("Score", 1); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }
}
