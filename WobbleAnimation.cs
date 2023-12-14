using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleAnimation : MonoBehaviour
{
    
    public float wobbleSpeed = 2f; // Adjust the speed of the wobble
    public float wobbleAmount = 0.1f; // Adjust the amount of the wobble

    private Vector3 originalScale;

    void Start()
    {
        // Store the original scale of the GameObject
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Calculate the wobble factor based on time
        float wobbleFactor = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;

        // Apply the wobble to the scale of the GameObject
        transform.localScale = originalScale + new Vector3(wobbleFactor, wobbleFactor, wobbleFactor);
    }
}
