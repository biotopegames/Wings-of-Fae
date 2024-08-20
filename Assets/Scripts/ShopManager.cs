using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public void PurchaseKamikazeUpgrade()
    {
        UpgradeManager.Instance.ApplyKamikazeUpgrade();
        // Update any UI or provide feedback to the player here
    }

    public void PurchaseSleepdustUpgrade()
    {
        UpgradeManager.Instance.ApplySleepdustUpgrade();
        // Update any UI or provide feedback to the player here
    }

    public void PurchaseShieldUpgrade()
    {
        UpgradeManager.Instance.ApplyShieldUpgrade();
        // Update any UI or provide feedback to the player here
    }

    public void PurchaseReplicationUpgrade()
    {
        UpgradeManager.Instance.ApplyReplicationUpgrade();
        // Update any UI or provide feedback to the player here
    }

    // Add other methods for different upgrades
}
