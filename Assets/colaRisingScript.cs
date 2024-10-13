using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colaRisingScript : MonoBehaviour
{
    public float riseSpeed = 1.0f; // Speed at which the cola rises
    public Vector3 initialPosition; // Initial position to reset to

    public void Start()
    {
        initialPosition = new Vector3(15, -28, 0); // Set the initial position
        gameObject.transform.position = initialPosition; // Start at the initial position
    }

    private void Update()
    {
        // Make the cola rise smoothly over time
        gameObject.transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);
        if(Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = initialPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.transform.position = initialPosition;
        }
    }
}
