using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour, IAttackable
{
    // Health property implementation
    [SerializeField] private int health = 5; // Initial health of the cage, adjustable in the Inspector

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    // Method to handle taking damage
    public void GetHurt(int damageAmount)
    {
        Health -= damageAmount; // Reduce health by the damage amount
        Debug.Log("Cage took damage. Current health: " + Health);

        if (Health <= 0)
        {
            Die(); // Call Die() if health reaches zero or below
        }
    }

    // Method to handle the death of the cage
    public void Die()
    {
        HUDManager.Instance.AddFreedPixies();
        Destroy(gameObject); // Destroy the cage object
    }
}
