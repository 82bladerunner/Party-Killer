using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBlackout : MonoBehaviour
{
    public Image blackoutPanel;
    public float fadeDuration = 1.0f;

    public bool isFading = false;
    public GameObject DudeSpawner;
    public GameObject ExistingKillerSpawner;

    void Start()
    {
        // Set the initial color of the panel to fully transparent
        SetPanelAlpha(0f);
    }

    // Expose a method to start the fade externally
    public void StartFadeEffect()
    {
        if (!isFading)
        {
            StartCoroutine(FadeToBlackAndBack());
        }
    }

    IEnumerator FadeToBlackAndBack()
    {
        isFading = true;

        // Fade to black
        yield return FadeToBlack();

        // Do any actions you want while the screen is black (e.g., load a new scene, perform some task)
        if(!GameController.KillerFound){
            
            DestroyAllObjectsWithTag("NonKiller");
            DestroyAllObjectsWithTag("Killer");

            GameController.levelCounter += 1;

            Instantiate(DudeSpawner, new Vector3(5.3845f, 2.0382f, 0f), Quaternion.identity);
            Instantiate(ExistingKillerSpawner, new Vector3(5.3845f, 2.0382f, 0f), Quaternion.identity);
        }
        

        // Fade back to normal
        yield return FadeToNormal();

        isFading = false;
    }

    private void DestroyAllObjectsWithTag(string tag)
    {
        // Find all GameObjects with the specified tag
        GameObject[] findObjectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        // Loop through the array and destroy each object
        foreach (GameObject obj in findObjectsWithTag)
        {
            Destroy(obj);
        }

        Debug.Log("Destroyed all objects with tag: " + tag);
    
    }

    IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        Color startColor = blackoutPanel.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            SetPanelAlpha(Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetPanelAlpha(targetColor.a); // Ensure the target alpha is set precisely
    }

    IEnumerator FadeToNormal()
    {
        float elapsedTime = 0f;
        Color startColor = blackoutPanel.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            SetPanelAlpha(Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetPanelAlpha(targetColor.a); // Ensure the target alpha is set precisely
    }

    void SetPanelAlpha(float alpha)
    {
        Color panelColor = blackoutPanel.color;
        panelColor.a = alpha;
        blackoutPanel.color = panelColor;
    }
}
