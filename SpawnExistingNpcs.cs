using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnExistingNpcs : MonoBehaviour
{
    public GameObject objectToSpawn; // Prefab of the object you want to spawn
    private int numberOfObjectsToSpawn; // Number of objects to spawn
    public float disableSpawnersTimer;
    public float minimumDistance = 2f; // Minimum distance between spawned objects

    private Transform topLeftTransform;
    private Transform bottomLeftTransform;
    private Transform bottomRightTransform;
    private Transform topRightTransform;


    void Start()
    {

        // Get the Transform of the child objects
        GetChildTransformsByName();

        // Spawn objects inside the bounds
        SpawnObjectsInsideBoundsArea();
        Destroy(gameObject, disableSpawnersTimer);
    }

    private int NumberOfObjectsByLevel(int level)
    {
        switch (level)
        {
            case 1:
                return 25;
            case 2:
                return 20;
            case 3:
                return 15;
            case 4:
                return 10;
            case 5:
                return 5;
            case 6:
                return 0;
            default:
                throw new ArgumentOutOfRangeException(nameof(level), "Invalid level number");
        }
    }


    void GetChildTransformsByName()
    {
        // Find child objects by name
        topLeftTransform = transform.Find("TopLeft");
        bottomLeftTransform = transform.Find("BottomLeft");
        bottomRightTransform = transform.Find("BottomRight");
        topRightTransform = transform.Find("TopRight");

        // Check if all child objects were found
        if (topLeftTransform == null || bottomLeftTransform == null || bottomRightTransform == null || topRightTransform == null)
        {
            Debug.LogError("One or more child objects not found.");
        }
    }

    void SpawnObjectsInsideBoundsArea()
    {   
         // Access the NonPlayerCharacters array from GameController
        NonPlayerCharacter[] npcArray = GameController.nonPlayerCharacters;

        if (npcArray != null)
        {
            // Iterate through the array and spawn NonPlayerCharacters
            int spawnedNpcAmount = 0;
            foreach (NonPlayerCharacter npc in npcArray)
            {
                if(spawnedNpcAmount == NumberOfObjectsByLevel(GameController.levelCounter)) break;

                // Get a random position inside the bounds
                Vector3 randomPosition = GetRandomPositionInsideBounds();

                // Spawn the NonPlayerCharacter at the random position
                Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

                npc.isSpawned = true;
                spawnedNpcAmount++;

            }
        }
        else
        {
            Debug.LogError("NonPlayerCharacters array is null.");
        }

    }

    Vector3 GetRandomPositionInsideBounds()
    {
        // Choose a random point inside the rectangle defined by the four child objects
        float randomX = Random.Range(bottomLeftTransform.position.x, topRightTransform.position.x);
        float randomY = Random.Range(bottomLeftTransform.position.y, topRightTransform.position.y);

        return new Vector3(randomX, randomY, 0f);
    }
}
