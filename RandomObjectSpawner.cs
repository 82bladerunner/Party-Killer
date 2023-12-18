using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomObjectSpawner : MonoBehaviour
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
        numberOfObjectsToSpawn = GameController.GetNumOfDudes(GameController.levelCounter);

        // Get the Transform of the child objects
        GetChildTransformsByName();

        // Spawn objects inside the bounds
        SpawnObjectsInsideBoundsArea();
        GameObject[] foundNpcs = GameObject.FindGameObjectsWithTag("NonKiller");
        var npcArray = foundNpcs;
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
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            // Get a random position inside the bounds
            Vector3 randomPosition = GetRandomPositionInsideBounds();

            // Spawn the object at the random position
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
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
