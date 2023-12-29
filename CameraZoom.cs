using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomInterval = 5f; // Time interval for zooming
    public float zoomAmount = 0.2f; // Adjust the zoom amount as needed

    private Camera mainCamera;
    private float originalSize;

    void Start()
    {
        mainCamera = Camera.main;

        // Store the original camera size
        originalSize = mainCamera.orthographicSize;

        // Start invoking the Zoom method every zoomInterval seconds
        InvokeRepeating("Zoom", 0f, zoomInterval);
    }

    void Zoom()
    {
        // Toggle between zooming in and out
        if (mainCamera.orthographicSize == originalSize)
        {
            // Zoom in
            mainCamera.orthographicSize -= zoomAmount;
        }
        else
        {
            // Zoom out
            mainCamera.orthographicSize = originalSize;
        }
    }

    // Call this method to stop the zoom effect
    public void StopZoom()
    {
        CancelInvoke("Zoom");
    }
}
