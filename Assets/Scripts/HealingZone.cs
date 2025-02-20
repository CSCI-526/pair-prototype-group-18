using UnityEngine;

public class HealingZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Healing Zone");

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Calling EnterHealingZone()");
                playerHealth.enterHealingZone();
            }
            else
            {
                Debug.LogError("ERROR: PlayerHealth component not found on Player!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Something exited: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Left Healing Zone");

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Calling exitHealingZone()");
                playerHealth.exitHealingZone();
            }
            else
            {
                Debug.LogError("ERROR: PlayerHealth component not found on Player!");
            }
        }
    }
}
