using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Killer : MonoBehaviour
{
    // Sprite arrays for different body parts
    public Sprite[] hatSprites;
    public Sprite[] headSprites;
    public Sprite[] bodySprites;
    public Sprite[] legsSprites;
    public int HeadIndex { get; set; }
    public int HatIndex { get; set; }
    public int BodyIndex { get; set; }
    public int LegsIndex { get; set; }
    private SpriteRenderer[] childSpriteRenderers;

    // Constructor to initialize the Killer with sprites for each body part
    public Killer(int head, int hat, int body, int legs)
    {
        HeadIndex = head;
        BodyIndex = body;
        LegsIndex = legs;
        HatIndex = hat;
    }

    public void Start()
    {
        if(GameController.levelCounter >= 10){
        InvokeRepeating(nameof(ChangeSpriteColor), 0.1f, 0.5f);
        }
    }


    public void ChangeSpriteColor()
    {
        ChangeSpriteColors(Random.ColorHSV());
    }

    private void ChangeSpriteColors(Color newColor)
    {

        childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < childSpriteRenderers.Length; i++)
        {
            childSpriteRenderers[i].color = newColor;
        }
    }

    
}
