using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLights : MonoBehaviour
{
    private SpriteRenderer discoSprite; // Assign your SpriteRenderer component to this in the Inspector
    public float colorChangeInterval = 0.5f; // Time interval for changing colors
    public float lowAlpha = 0.2f; // Adjust the low alpha value as needed


    private void Start()
    {
        discoSprite = gameObject.GetComponent<SpriteRenderer>();
        // Start invoking the ChangeColor method every colorChangeInterval seconds
        InvokeRepeating("ChangeColor", 0f, colorChangeInterval);
    }

    private void ChangeColor()
    {
        // Generate a random color with low alpha
        Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f, 1f, 1f);
        randomColor.a = lowAlpha;

        // Change the color of the sprite
        discoSprite.color = randomColor;
    }
}

