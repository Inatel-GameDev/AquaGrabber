using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleInvoker : MonoBehaviour
{
    public GameObject bubble;  // The prefab to instantiate
    public float interval = 3f; // Time interval in seconds

    private void Start()
    {
        // Start the instantiation process
        InvokeRepeating(nameof(InstantiateObject), 0f, interval);
    }

    void InstantiateObject()
    {
        // Instantiate the prefab at the same position and rotation as the script's GameObject
        Instantiate(bubble, transform.position, transform.rotation);
    }
}
