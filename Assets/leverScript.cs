using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add function for unflipping lever when level reset or player dies
public class leverScript : MonoBehaviour
{
    public bool buttonFlipped = false;
    public GameObject player;
    public GameObject unflipped;
    public GameObject flipped;
    public GameObject objects;
    public bool isTouchingLever;
    public bool startingForm;
    public AudioSource leverSFX;
    private void Start()
    {
        buttonFlipped = startingForm;
    }

    private void Update()
    {
        // Check for input only if the player is colliding with the lever
        if (buttonFlipped)
        {
            // Set the lever state based on buttonFlipped
            unflipped.SetActive(false);
            flipped.SetActive(true);
            objects.SetActive(true);
        }
        else
        {
            unflipped.SetActive(true);
            flipped.SetActive(false);
            objects.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && isTouchingLever)
        {
            buttonFlipped = !buttonFlipped;
            leverSFX.Play();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingLever = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingLever = false;
        }
    }

    // Optional: reset the lever (unflip it) based on some condition (e.g., when the player dies)
    public void ResetLever()
    {
        buttonFlipped = false;
    }
}
