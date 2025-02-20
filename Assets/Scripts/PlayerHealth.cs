using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Required for UI elements

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float healthDrainRate = 1f;
    private Coroutine healthDrainCoroutine;
    private bool isInHealingZone = false; // Track healing zone status

    public Image healthBar;  // Drag & Drop your UI Image here
    public Color fullHealthColor = Color.green;
    public Color depletedHealthColor = Color.white;

    void Start()
    {
        currentHealth = maxHealth;
        startHealthDrain();  // Start draining health by default
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
                currentHealth -= healthDrainRate;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevents negative health
                updateHealthBar();
            }
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Player is dead");
    }

    void updateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;

            // Change color based on health level
            if (currentHealth <= 0)
            {
                healthBar.color = depletedHealthColor;  // White when fully depleted
            }
            else
            {
                healthBar.color = Color.Lerp(depletedHealthColor, fullHealthColor, currentHealth / maxHealth);
            }
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
