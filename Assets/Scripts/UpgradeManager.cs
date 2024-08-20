using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    // Upgrade variables
    public float speedMultiplier = 1.0f;
    public int speedUpgradeCost = 100; // Cost of the speed upgrade

    public bool hasSpeedUpgrade = false;

    public bool hasKamikazeUpgrade = false;
    public bool hasSleepdustUpgrade = false;
    public bool hasShieldUpgrade = false;
    public bool hasReplicationUpgrade = false;

    void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persist across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure there's only one instance
        }
    }

    public void UpgradeSpeed()
    {
        if (HUDManager.Instance.SpendStardust(speedUpgradeCost))
        {
            hasSpeedUpgrade = true;

            speedMultiplier += 0.1f;
            Debug.Log("Speed upgraded! New multiplier: " + speedMultiplier);
        }
        else
        {
            Debug.Log("Not enough stardust to upgrade speed!");
        }
    }

    public void ApplyKamikazeUpgrade()
    {
        hasKamikazeUpgrade = true;
    }

    public void ApplySleepdustUpgrade()
    {
        hasSleepdustUpgrade = true;
    }

    public void ApplyShieldUpgrade()
    {
        hasShieldUpgrade = true;
    }

    public void ApplyReplicationUpgrade()
    {
        hasReplicationUpgrade = true;
    }

    // Add other upgrade methods as needed
}
