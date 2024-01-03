using UnityEngine;

public class MouseHover : MonoBehaviour
{
    private SpriteRenderer[] childSpriteRenderers;
    private Color[] originalColors;

    void Start()
    {
        // Get all SpriteRenderers of children
        childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Store the original colors
        originalColors = new Color[childSpriteRenderers.Length];
        for (int i = 0; i < childSpriteRenderers.Length; i++)
        {
            originalColors[i] = childSpriteRenderers[i].color;
        }
    }

    // Called when the mouse enters the collider of the GameObject
    private void OnMouseEnter()
    {
        //Debug.Log("Mouse entered!");

        // Change sprite colors to a new color (e.g., red)
        ChangeSpriteColors(Color.red);
        AudioManager.Instance.PlaySFX("Pick");
    }

    // Called when the mouse exits the collider of the GameObject
    private void OnMouseExit()
    {
        //Debug.Log("Mouse exited!");

        // Revert sprite colors to the original colors
        ChangeSpriteColors(originalColors);
    }

    // Helper method to change sprite colors
    private void ChangeSpriteColors(Color newColor)
    {
        for (int i = 0; i < childSpriteRenderers.Length; i++)
        {
            childSpriteRenderers[i].color = newColor;
        }
    }

    // Helper method to revert sprite colors to the original colors
    private void ChangeSpriteColors(Color[] colors)
    {
        for (int i = 0; i < childSpriteRenderers.Length; i++)
        {
            childSpriteRenderers[i].color = colors[i];
        }
    }
}