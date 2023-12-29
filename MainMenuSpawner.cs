using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainMenuSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Prefab of the object you want to spawn
    public int numberOfObjectsToSpawn; // Number of objects to spawn

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
