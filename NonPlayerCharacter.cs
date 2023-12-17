using UnityEngine;

public class NonPlayerCharacter
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
    public bool isSpawned { get; set; }
    public bool isConstructed { get; set; }

    // Constructor to initialize the Dude with sprites for each body part
    public NonPlayerCharacter(int head, int hat, int body, int legs)
    {
        HeadIndex = head;
        BodyIndex = body;
        LegsIndex = legs;
        HatIndex = hat;
    }

}
