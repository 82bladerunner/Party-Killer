using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PanelController : MonoBehaviour
{
    public GameObject[] panels; // Reference to your panels
    public Image fadeImage; // Reference to the UI image for fade effect
    public float fadeDuration = 1f; // Duration of the fade effect
    public string nextScene; // Name of the next scene to load after the third panel

    private int currentPanelIndex = -1;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            //play SFX insert coin
            if(currentPanelIndex == -1) AudioManager.Instance.PlaySFX("InsertCoin");

            //change to next panel
            currentPanelIndex++;
            
            // If there are more panels, show the next one; otherwise, load the next scene
            if (currentPanelIndex < panels.Length)
            {
                AudioManager.Instance.PlaySFX("Whoosh");
                ShowCurrentPanel();
            }
            else
            {
                StartCoroutine(FadeAndLoadNextScene());
                AudioManager.Instance.PlaySFX("StartBell");
            }
        }
    }

    void ShowCurrentPanel()
    {
        // Hide all panels
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }

        // Show the current panel
        panels[currentPanelIndex].SetActive(true);
    }

    IEnumerator FadeAndLoadNextScene()
    {
        // Start fading out
        yield return Fade(1f);

        // Load the next scene
        SceneManager.LoadScene(nextScene);

        // Start fading in (next scene)
        yield return Fade(0f);
    }

    IEnumerator Fade(float targetAlpha)
    {
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        AudioManager.Instance.FadeOutAndStopMusic(fadeDuration);

        while (elapsedTime < fadeDuration)
        {
            fadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = targetColor; // Ensure the target alpha is set precisely
    }
}
