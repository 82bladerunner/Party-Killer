using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinTextController : MonoBehaviour
{
    public Text loseText;
    public float fadeInTime = 2f;
    public float textScaleIncreaseRate = 0.5f;
    public static ScreenBlackout screenBlackoutReference;


    void Start()
    {
        // Ensure the text starts invisible
        loseText.color = new Color(1f, 1f, 1f, 0f);
        loseText.transform.localScale = Vector3.zero;

        // Subscribe to an event or condition that triggers the "You lose" state
        // For example, you might use the GameController.KillerFound or a custom event
        // Subscribe to the KillerFoundEvent
        GameController.KillerFoundEvent += OnGameWin;


        // In this example, I'll use a coroutine to simulate the trigger of losing after a delay
        //StartCoroutine(SimulateLosing());
    }

    void OnGameWin()
    {
        // Start the fade-in and scale-up effect when the Killer is found
        StartCoroutine(FadeInText());
    }

    IEnumerator FadeInText()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeInTime)
        {
            // Calculate the new alpha value based on the elapsed time
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInTime);

            // Apply the new alpha value to the text color
            loseText.color = new Color(1f, 1f, 1f, newAlpha);

            // Calculate the new scale based on the elapsed time
            float newScale = Mathf.Lerp(0f, 1f, elapsedTime * textScaleIncreaseRate);

            // Apply the new scale to the text transform
            loseText.transform.localScale = new Vector3(newScale, newScale, 1f);

            // Wait for the next frame
            yield return null;

            // Increase the elapsed time
            elapsedTime += Time.deltaTime;
        }

        // Ensure the text is fully visible and scaled after the fade-in effect
        loseText.color = Color.green;
        loseText.transform.localScale = Vector3.one;
    }
}
