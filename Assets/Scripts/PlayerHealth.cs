using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float healthDrainRate = 1f;
    private Coroutine healthDrainCoroutine;
    private bool isInHealingZone = false; // Track healing zone status

    public Image currentHealthBar;  // Yellow bar (active health)
    public Image depletedHealthBar; // White bar (depleted area)

    void Start()
    {
        currentHealth = maxHealth;
        startHealthDrain();  // Start draining health by default
        updateHealthBars();
    }

    public void startHealthDrain()
    {
        if (healthDrainCoroutine == null && !isInHealingZone)
        {
            healthDrainCoroutine = StartCoroutine(drainHealth());
        }
    }

    public void stopHealthDrain()
    {
        if (healthDrainCoroutine != null)
        {
            StopCoroutine(healthDrainCoroutine);
            healthDrainCoroutine = null;
        }
    }

    IEnumerator drainHealth()
    {
        while (currentHealth > 0)
        {
            if (!isInHealingZone) // Drain only if NOT in healing zone
            {
                float previousHealth = currentHealth; // Store the previous health value
                currentHealth -= healthDrainRate;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevents negative health

                updateHealthBars(previousHealth); // Update bars properly
            }
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Player is dead");
    }

    void updateHealthBars(float previousHealth = -1)
    {
        float healthPercentage = currentHealth / maxHealth;

        // Update the yellow "Current Health" bar instantly
        currentHealthBar.fillAmount = healthPercentage;

        // Only expand the White Bar (Depleted Health) if health has decreased
        if (previousHealth > currentHealth) 
        {
            depletedHealthBar.fillAmount = 1; // Always full, showing the depleted area
        }
    }

    // When player enters a healing zone
    public void enterHealingZone()
    {
        isInHealingZone = true;
        stopHealthDrain(); // Stop losing health
        Debug.Log("Health Drain Stopped");
    }

    // When player leaves a healing zone
    public void exitHealingZone()
    {
        isInHealingZone = false;
        startHealthDrain(); // Resume health depletion
        Debug.Log("Health Drain Resumed");
    }
}
