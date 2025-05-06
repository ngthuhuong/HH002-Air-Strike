using UnityEngine;

public class VerticalScrollingBackground : MonoBehaviour
{
    [Header("Scroll Settings")]
    [SerializeField] private float scrollSpeed = 2f; // Speed of the vertical scroll
    [SerializeField] private float imageHeight = 10f; // Height of each image
    [SerializeField] private Transform[] backgroundImages; // Array of 3 images

    private void Update()
    {
        // Move each image downward
        foreach (var image in backgroundImages)
        {
            image.position +=  scrollSpeed * Time.deltaTime * Vector3.down;

            // Check if the image has moved out of view
            if (image.position.y < -imageHeight)
            {
                // Move the image to the top
                float highestY = GetHighestImageY();
                image.position = new Vector3(image.position.x, highestY + imageHeight, image.position.z);
            }
        }
    }

    // Get the highest Y position among the images
    private float GetHighestImageY()
    {
        float highestY = float.MinValue;
        foreach (var image in backgroundImages)
        {
            if (image.position.y > highestY)
            {
                highestY = image.position.y;
            }
        }
        return highestY;
    }
}