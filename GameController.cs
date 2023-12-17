
using System.Collections;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public static Killer killer { get; set; }
    public static NonPlayerCharacter[] nonPlayerCharacters { get; set; }
    public static bool KillerFound = false;
    public static int numOfKillersToSpawn = 1;
    
    public float countdownTime = 5f;
    public float resetCountdown;

    private Coroutine countdownCoroutine;
    public ScreenBlackout screenBlackout;
    public static int levelCounter { get; set; }

    void Start()
    {
        levelCounter = 1;
        // Start the countdown when the script is initialized
        StartCountdown();
    }

    public void StartCountdown()
    {
        resetCountdown = countdownTime;
        // Stop the previous coroutine if it's already running
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        // Start the new coroutine
        countdownCoroutine = StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        while (countdownTime > 0f && !KillerFound)
        {
            // Display the current countdown time
            Debug.Log($"Countdown: {countdownTime} seconds");

            // Wait for the next frame
            yield return null;

            // Reduce the countdown time
            countdownTime -= Time.deltaTime;
        }

        // Countdown has reached zero or KillerFound is true
        Debug.Log(KillerFound ? "Killer Found!" : "Countdown finished!");

        if(!KillerFound){screenBlackout.StartFadeEffect();}

        yield return null;

        countdownTime = resetCountdown;
    }
}