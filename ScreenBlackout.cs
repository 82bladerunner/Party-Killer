using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBlackout : MonoBehaviour
{
    public Image blackoutPanel;
    public float fadeDuration = 1.0f;

    public bool isFading = false;
    public GameObject dudeSpawner;
    private GameController gameController;  // Add a reference to GameController
    public GameObject[] npcArray;
    public Killer killer;

    

    void Start()
    {
        GameController.KillerFoundEvent += OnGameLostOrWin;
        GameController.GameLostEvent += OnGameLostOrWin;
        // Set the initial color of the panel to fully transparent
        SetPanelAlpha(0f);
        // Get the GameController component on the GameController object
        gameController = GameObject.FindObjectOfType<GameController>();

        var npcArray = GameController.npcArray;

    }

    private void OnGameLostOrWin()
    {
        AudioManager.Instance.FadeOutAndStopMusic(fadeDuration);
        StartFadeToBlackCoroutine();
    }


    // Expose a method to start the fade externally
    public void StartFadeEffect()
    {
        if (!isFading)
        {
            StartCoroutine(FadeToBlackAndBack());
        }
    }

    public void StartFadeToBlackCoroutine()
    {
        StartCoroutine(FadeToBlack());
    }

    public IEnumerator FadeToBlackAndBack()
    {
        isFading = true;

        // Fade to black
        yield return FadeToBlack();

        // Do any actions you want while the screen is black (e.g., load a new scene, perform some task)
        if(!GameController.KillerFound){

            GameController.levelCounter += 1;

            ReduceNpcs(GameController.GetNumOfDudes(GameController.levelCounter-1) - GameController.GetNumOfDudes(GameController.levelCounter));
            ScrambleNpcs();
        }

        // Fade back to normal
        yield return FadeToNormal();

        isFading = false;
        
        if(GameController.levelCounter < 11){
            gameController.StartCountdown();
            Debug.Log("Level counter: " + GameController.levelCounter);
        }
        else {
            //Endgame actions
            Debug.Log("Game Finished");
            GameController.GameLost = true;

        }
        yield return null;
    }

    private void ScrambleNpcs()
    {
        // Scramble the positions of "NonKiller" NPCs
        foreach (GameObject npc in npcArray)
        {
            ScrambleNpcPosition(npc);
        }

        var killer = GameObject.FindGameObjectsWithTag("Killer");
        ScrambleNpcPosition(killer[0]);

    
    }

    void ScrambleNpcPosition(GameObject npc)
    {
        // Ensure the DudeSpawner is set
        if (dudeSpawner == null)
        {
            Debug.LogError("DudeSpawner reference not set!");
            return;
        }

        // Get the positions of the DudeSpawner's child objects
        Transform topLeft = dudeSpawner.transform.Find("TopLeft");
        Transform bottomRight = dudeSpawner.transform.Find("BottomRight");
        Transform bottomLeft = dudeSpawner.transform.Find("BottomLeft");
        Transform topRight = dudeSpawner.transform.Find("TopRight");

        // Ensure all child objects are found
        if (topLeft == null || bottomRight == null || bottomLeft == null || topRight == null)
        {
            Debug.LogError("One or more DudeSpawner child objects not found!");
            return;
        }

        // Randomize the position within the specified range around DudeSpawner
        Vector3 randomPosition = new Vector3(
            Random.Range(bottomLeft.position.x, topRight.position.x),
            Random.Range(bottomLeft.position.y, topRight.position.y),
            Random.Range(bottomLeft.position.z, topRight.position.z)
        );

        // Apply the new position to the NPC
        npc.transform.position = randomPosition;
    }

    private void ReduceNpcs(int amountToDestroy)
    {
        npcArray = GameObject.FindGameObjectsWithTag("NonKiller");

        // Ensure there are NPCs to destroy
        if (npcArray.Length > 0)
        {
            // Loop through the NPCs to destroy
            for (int i = 0; i < Mathf.Min(amountToDestroy, npcArray.Length); i++)
            {
                // Destroy the GameObject
                Destroy(npcArray[i]);
            }

            Debug.Log("Number of NonKiller objects found: " + npcArray.Length + " Destroying " + amountToDestroy);
        }
    }

    public IEnumerator FadeToBlack()
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
