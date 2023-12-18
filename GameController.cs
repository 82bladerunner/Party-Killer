
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public static Killer killer { get; set; }
    public static GameObject[] npcArray { get; set; }
    public static bool KillerFound = false;
    public static int numOfKillersToSpawn = 1;
    
    public float countdownTime = 5f;
    public float resetCountdown;

    private Coroutine countdownCoroutine;
    public ScreenBlackout screenBlackout;
    internal static int guessCounter = 0;
    public int maxGuesses = 3;

    public static int levelCounter { get; set; }

    void Start()
    {
        levelCounter = 1;
        
        // Start the countdown when the script is initialized
        StartCountdown();
        
    }

    void Update()
    {
        if(guessCounter == maxGuesses) 
        {
            levelCounter = 10;
        }       
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
            //Debug.Log($"Countdown: {countdownTime} seconds");

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

    public static int GetNumOfDudes(int levelNum)
    {
        switch (levelNum)
        {
            case 1:
                return 50;
            case 2:
                return 40;
            case 3:
                return 30;
            case 4:
                return 20;
            case 5:
                return 15;
            case 6:
                return 10;
            case 7:
                return 5;
            case 8:
                return 3;
            case 9:
                return 2;
            case 10:
                return 0;
            default:
                return 0; // Default value for other cases
        }
    }
}