using UnityEngine;

public class Level : MonoBehaviour
{
    // Properties
    public int LevelNum { get; private set; }
    public int NumOfDudes { get; private set; }

    // Constructor
    public Level(int levelNum, int numOfDudes)
    {
        LevelNum = levelNum;
        NumOfDudes = numOfDudes;
    }
}

