using System.Collections.Generic;
using UnityEngine;

public class PixieController : MonoBehaviour
{
    public List<Transform> pixies; // List of pixies in the flock
    public float baseSpeed = 1.0f;
    public float distanceBetweenPixies = 0.5f;
    public float centerThreshold = 1.0f; // Distance threshold to consider the mouse "at the center"
    public float shotSpeed = 5.0f; // Speed of the shot pixie
    public int damage = 1;

    private Vector3 mousePosition;
    private Vector3 lastDirection = Vector3.right; // Default direction to the right
    public static PixieController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Apply speed multiplier from the UpgradeManager
        baseSpeed *= UpgradeManager.Instance.speedMultiplier;
    }

    void Update()
    {
        // Update mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Handle left mouse click to shoot a pixie
        if (Input.GetMouseButtonDown(0))
        {
            ShootPixie();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HUDManager.Instance.PauseGame();
        }


        // Calculate speed based on flock size
        float speed = baseSpeed * Mathf.Max(0.95f, 1.0f - 0.05f * (pixies.Count - 1));

        // Determine if the mouse position is within the center threshold
        bool isNearCenter = (Mathf.Abs(mousePosition.x) < centerThreshold && Mathf.Abs(mousePosition.y) < centerThreshold);

        // Move and rotate each pixie in the flock
        for (int i = 0; i < pixies.Count; i++)
        {
            Vector3 targetPosition;
            Vector3 directionToTarget;

            if (i == 0) // Lead pixie follows the mouse
            {
                if (isNearCenter)
                {
                    // Use the last direction if the mouse is near the center
                    directionToTarget = lastDirection;
                }
                else
                {
                    // Calculate the direction from the lead pixie to the mouse position
                    directionToTarget = (mousePosition - pixies[i].position).normalized;
                    lastDirection = directionToTarget; // Update the last direction
                }

                // Move the lead pixie towards the mouse at the constant speed
                targetPosition = pixies[i].position + directionToTarget * speed * Time.deltaTime;
            }
            else // Subsequent pixies follow the pixie in front
            {
                // Calculate the target position for the current pixie based on the one in front
                targetPosition = pixies[i - 1].position - (pixies[i - 1].position - pixies[i].position).normalized * distanceBetweenPixies;
                targetPosition = Vector3.MoveTowards(pixies[i].position, targetPosition, speed * Time.deltaTime);

                // Calculate the direction to the target position
                directionToTarget = (targetPosition - pixies[i].position).normalized;
            }

            // Rotate the current pixie to face the direction it is moving towards
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            pixies[i].rotation = Quaternion.Euler(0, 0, angle);

            // Move the current pixie to the target position
            pixies[i].position = targetPosition;
        }
    }

    // Method to add a new pixie to the flock
    public void AddPixie(Transform newPixie)
    {
        pixies.Add(newPixie);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pixie") && !other.GetComponent<PixieBehavior>().isInFlock)
        {
            AddPixie(other.transform);
            // other.GetComponent<Collider2D>().enabled = false;
        }

        if (other.CompareTag("Cage"))
        {
            other.GetComponent<Cage>().GetHurt(damage);
        }

        if (other.CompareTag("Stardust"))
        {
            HUDManager.Instance.AddStardust(1);
            Destroy(other.gameObject);
        }
    }

    // Method to shoot the last pixie towards the mouse click position
    private void ShootPixie()
    {
        // Prevent shooting if there's only one pixie left
        if (pixies.Count > 1)
        {
            // Get the last pixie from the flock
            Transform pixieToShoot = pixies[pixies.Count - 1];
            pixies.RemoveAt(pixies.Count - 1); // Remove it from the flock

            // Calculate the direction and set the pixie's target
            Vector3 shootDirection = (mousePosition - pixieToShoot.position).normalized;
            StartCoroutine(MovePixieToTarget(pixieToShoot, mousePosition, shootDirection));
        }
    }

    // Coroutine to move the pixie to the target position
    private IEnumerator<WaitForEndOfFrame> MovePixieToTarget(Transform pixie, Vector3 target, Vector3 direction)
    {
        while (Vector3.Distance(pixie.position, target) > 0.1f)
        {
            pixie.position += direction * shotSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

    }
}
