using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] private Vector3 movementVector;
    private float movementFactor;
    [SerializeField] private float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycle = Time.time / period;   // continually growing over time

        const float tau = Mathf.PI * 2;     // const value
        float rawSinWave = Mathf.Sin(cycle * tau);  // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;    // going from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
