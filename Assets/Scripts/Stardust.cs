using UnityEngine;

public class Stardust : MonoBehaviour
{
    public int value = 1; // Value of this stardust piece


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Collect stardust and add it to the player's balance
            HUDManager.Instance.AddStardust(value);
            // Destroy the stardust object after collection
            Destroy(gameObject);
        }
    }
}
