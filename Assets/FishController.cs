using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public Vector3 pointA;  // The starting point (or one end of the movement)
    public Vector3 pointB;  // The ending point (or the other end of the movement)
    public float speed = 2f; // Speed of movement

    private void Start()
    {
        pointA = transform.position;
        pointB = transform.position + Vector3.left * 3;
    }

    private void Update()
    {
        // Calculate the time-based interpolation between pointA and pointB
        float t = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(pointA, pointB, t);
    }
}
