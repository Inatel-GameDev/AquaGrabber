using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Assign the player via the Inspector
    public Vector2 minBounds; // Minimum x and y values for the camera
    public Vector2 maxBounds; // Maximum x and y values for the camera

    public float smoothSpeed = 0.125f; // The smoothing speed for the camera's movement

    private Vector3 offset; // The offset from the player position

    void Start()
    {
        transform.position = player.position + Vector3.back*10;
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = player.position + offset;

        // Clamp the desired position to the given x and y bounds
        float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        // Smoothly move the camera towards the clamped position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(clampedX, clampedY, desiredPosition.z), smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;
    }
}