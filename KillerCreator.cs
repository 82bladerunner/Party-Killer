using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerCreator : MonoBehaviour
{
    public Sprite[] hatSprites;
    public Sprite[] headSprites;
    public Sprite[] bodySprites;
    public Sprite[] legsSprites;

    public GameObject hat;
    public GameObject head;
    public GameObject body;
    public GameObject legs;
    
    private SpriteRenderer hatRenderer;
    private SpriteRenderer legsRenderer;
    private SpriteRenderer bodyRenderer;
    private SpriteRenderer headRenderer;

    private int hatIndex;
    private int headIndex;
    private int bodyIndex;
    private int legsIndex;
         
    void Start()
    {
        hatRenderer = hat.GetComponent<SpriteRenderer>();
        legsRenderer = legs.GetComponent<SpriteRenderer>();
        bodyRenderer = body.GetComponent<SpriteRenderer>();
        headRenderer = head.GetComponent<SpriteRenderer>();

        hatIndex = GameController.killer.HatIndex;
        headIndex = GameController.killer.HeadIndex;
        bodyIndex = GameController.killer.BodyIndex;
        legsIndex = GameController.killer.LegsIndex;

        hatRenderer.sprite = hatSprites[hatIndex];
        legsRenderer.sprite = legsSprites[legsIndex];
        bodyRenderer.sprite = bodySprites[bodyIndex];
        headRenderer.sprite = headSprites[headIndex];

         // Randomly choose between 1 and -1
        float randomXScale = (Random.Range(0, 2) == 0) ? -1f : 1f;

        // Get the Transform component of the GameObject
        Transform transformComponent = GetComponent<Transform>();

        // Set the new X parameter
        transformComponent.localScale = new Vector3(randomXScale, transformComponent.localScale.y, transformComponent.localScale.z);

    }

}

