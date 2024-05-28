using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{
    private float _speed = 2f; // Adjust the speed of the movement
    private float _amplitude = 1.5f; // Adjust the amplitude of the sine wave
    private float _time;
    private Vector3 startPosition;

    void Start()
    {
        _time = 0;
        // Store the initial position of the GameObject
        startPosition = transform.position;
    }

    void Update()
    {
        _time += Time.deltaTime;
        float newY = startPosition.z + Mathf.Sin(_time * (_speed * KitchenStates.SpeedMultiplier)) * _amplitude; //move along the z axis

        transform.position = new Vector3(startPosition.x, startPosition.y, newY);
    }
}
