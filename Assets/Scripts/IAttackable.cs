using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    // Property to represent health of the attackable entity
    int Health { get; set; }

    // Method to handle taking damage
    void GetHurt(int damageAmount);

    // Method to handle death behavior
    void Die();
}
