using UnityEngine;

public class PixieBehavior : MonoBehaviour
{
    public bool isInFlock = false;

    void Update()
    {
        // Optional: you can add some animation or behavior before the pixie is added to the flock
        if (!isInFlock)
        {
            // Example: Make the pixie slowly move or hover to show it's free
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isInFlock)
        {
            // Once added to the flock, disable any individual behaviors
            isInFlock = true;
            // Call the AddPixie method on the PixieController attached to the player
            other.GetComponent<PixieController>().AddPixie(transform);
        }
    }
}
