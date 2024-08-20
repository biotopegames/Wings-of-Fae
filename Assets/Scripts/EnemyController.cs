using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum State
    {
        Patrolling,
        Chasing,
        Eating
    }

    public State currentState; // Current state of the enemy

    public float patrollingSpeed = 2f;
    public float chasingSpeed = 4f;
    public int damage = 2;
    public float attentionRange = 5f;

    private Transform target;
    private Vector3 patrolPoint;
    private bool facingRight = true; // Tracks the enemy's facing direction

    void Start()
    {
        // Start in the patrolling state
        ChangeState(State.Patrolling);
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                CheckForTargetsInRange();
                break;

            case State.Chasing:
                Chase();
                break;

            case State.Eating:
                // Eating behavior handled in OnCollisionEnter2D
                break;
        }
    }

    private void Patrol()
    {
        // Implement patrolling behavior (e.g., move to a set of points)
        Vector3 newPosition = Vector3.MoveTowards(transform.position, patrolPoint, patrollingSpeed * Time.deltaTime);

        // Flip the enemy's localScale based on movement direction
        FlipDirection(newPosition.x - transform.position.x);

        transform.position = newPosition;

        // If the patrol point is reached, choose a new one
        if (Vector3.Distance(transform.position, patrolPoint) < 0.1f)
        {
            SetNewPatrolPoint();
        }
    }

    private void SetNewPatrolPoint()
    {
        // Define a new random patrol point within some bounds (e.g., the area where the enemy patrols)
        patrolPoint = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), transform.position.z);
    }

    private void Chase()
    {
        if (target != null)
        {
            // Chase the target (Player or Pixie)
            Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, chasingSpeed * Time.deltaTime);

            // Flip the enemy's localScale based on movement direction
            FlipDirection(newPosition.x - transform.position.x);

            transform.position = newPosition;

            // If the target exits the attention range, go back to patrolling
            if (Vector3.Distance(transform.position, target.position) > attentionRange)
            {
                ChangeState(State.Patrolling);
            }
        }
        else
        {
            // If the target is lost, return to patrolling
            ChangeState(State.Patrolling);
        }
    }

    private void ChangeState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.Patrolling:
                SetNewPatrolPoint();
                break;

            case State.Chasing:
                // Target should already be set when changing to Chasing
                break;

            case State.Eating:
                // Eating behavior handled in OnCollisionEnter2D
                break;
        }
    }

    private void CheckForTargetsInRange()
    {
        // Check for pixies in the flock or the player within the attention range
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= attentionRange)
        {
            target = player.transform;
            ChangeState(State.Chasing);
            return;
        }

        foreach (Transform pixie in PixieController.Instance.pixies)
        {
            if (Vector3.Distance(transform.position, pixie.position) <= attentionRange)
            {
                target = pixie;
                ChangeState(State.Chasing);
                return;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pixie"))
        {
            PixieBehavior pixieBehavior = collision.gameObject.GetComponent<PixieBehavior>();

            if (pixieBehavior != null && pixieBehavior.isInFlock)
            {
                HandleEatingPixies();
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            HandleEatingPixies();
        }
    }

    private void HandleEatingPixies()
    {
        // Switch to the eating state
        ChangeState(State.Eating);

        PixieController pixieController = PixieController.Instance;

        if (pixieController != null)
        {
            // Calculate the number of pixies to "eat"
            int pixiesToEat = Mathf.Min(damage, pixieController.pixies.Count);

            // Remove the pixies from the list
            for (int i = 0; i < pixiesToEat; i++)
            {
                Destroy(pixieController.pixies[pixieController.pixies.Count - 1].gameObject); // Destroy the pixie GameObject
                pixieController.pixies.RemoveAt(pixieController.pixies.Count - 1); // Remove from the list
            }

            // Check if there are no more pixies left in the flock
            if (pixieController.pixies.Count == 0)
            {
                HUDManager.Instance.LostGame();
            }

            // Return to patrolling after eating
            ChangeState(State.Patrolling);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // If the target exits the enemy's range, return to patrolling
        if (other.transform == target && (other.CompareTag("Pixie") || other.CompareTag("Player")))
        {
            target = null;
            ChangeState(State.Patrolling);
        }
    }

    private void FlipDirection(float moveDirection)
    {
        if (moveDirection > 0 && !facingRight || moveDirection < 0 && facingRight)
        {
            facingRight = !facingRight;

            // Flip the enemy's localScale on the x-axis
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the attention range as a red wireframe sphere in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attentionRange);
    }
}
