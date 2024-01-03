using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    public GameObject[] heartsArray;
    void Update()
    {
        switch (GameController.guessCounter)
        {
            case 3:
                // Tasks to perform when guessCounter is 3
                break;

            case 2:
                Destroy(heartsArray[heartsArray.Length-1]);
                break;

            case 1:
                Destroy(heartsArray[heartsArray.Length-2]);
                break;

            case 0:
                Destroy(heartsArray[heartsArray.Length-3]);
                break;

            default:
                // Default case (optional): Tasks to perform for other values of guessCounter
                break;
        }
    }
}
