using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{
    public float speed = 2f; // Adjust the speed of the movement
    public float amplitude = 1.5f; // Adjust the amplitude of the sine wave

    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the GameObject
        startPosition = transform.position;
    }

    void Update()
    {

        float newY = startPosition.z + Mathf.Sin(Time.time * speed) * amplitude; //move along the z axis

        transform.position = new Vector3(startPosition.x, startPosition.y, newY);
    }
}
