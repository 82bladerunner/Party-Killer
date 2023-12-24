using System.Collections;
using UnityEngine;

public class WobbleAnimation : MonoBehaviour
{
    // Adjust the range for randomness in wobble speed and amount
    public float minWobbleSpeed = 1f;
    public float maxWobbleSpeed = 2f;
    public float minWobbleAmount = 0.01f;
    public float maxWobbleAmount = 0.05f;

    private Vector3 originalScale;
    private float wobbleSpeed;
    private float wobbleAmount;

    void Start()
    {
        // Store the original scale of the GameObject
        originalScale = transform.localScale;

        // Set random wobble speed and amount within the specified range
        wobbleSpeed = Random.Range(minWobbleSpeed, maxWobbleSpeed);
        wobbleAmount = Random.Range(minWobbleAmount, maxWobbleAmount);

         // Start the randomizeXScale coroutine
        StartCoroutine(RandomizeXScale());
    }

    void Update()
    {
        // // Calculate the wobble factor based on time and random speed
        // float wobbleFactor = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;

        // // Apply the wobble to the scale of the GameObject
        // transform.localScale = originalScale + new Vector3(wobbleFactor, wobbleFactor, wobbleFactor);
    }

    IEnumerator RandomizeXScale()
    {
        while (true)
        {
            // Randomize x scale with -1 or 1
            transform.localScale = new Vector3(Random.Range(0, 2) == 0 ? -1f : 1f, originalScale.y, originalScale.z);

            // Wait for a random time before the next randomization
            yield return new WaitForSeconds(Random.Range(0f, 5f));
        }
    }
}
