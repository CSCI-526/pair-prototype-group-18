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
    private bool isNight = false; // Tracks whether it's night

    public Image currentHealthBar;  // Yellow bar (active health)
    public Image depletedHealthBar; // White bar (depleted area)

    void Start()
    {
        currentHealth = maxHealth;
        updateHealthBars();
    }

    void Update()
    {
        // Start/Stop Health Drain Based on Nighttime
        if (isNight && !isInHealingZone && healthDrainCoroutine == null)
        {
            startHealthDrain();
        }
        else if (!isNight && healthDrainCoroutine != null)
        {
            stopHealthDrain();
        }
    }

    public void startHealthDrain()
    {
        if (healthDrainCoroutine == null)
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
        while (currentHealth > 0 && isNight) // Health drains only at night
        {
            if (!isInHealingZone)
            {
                float previousHealth = currentHealth;
                currentHealth -= healthDrainRate;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                updateHealthBars(previousHealth);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void updateHealthBars(float previousHealth = -1)
    {
        float healthPercentage = currentHealth / maxHealth;
        currentHealthBar.fillAmount = healthPercentage;

        // Only expand the White Bar (Depleted Health) if health has decreased
        if (previousHealth > currentHealth) 
        {
            depletedHealthBar.fillAmount = 1; // Always full, showing depleted area
        }
    }

    // Called when Night Starts (From DayNightController)
    public void setNightTime(bool night)
    {
        isNight = night;
        Debug.Log(night ? "It's Night! Health will start draining." : "☀️ It's Day! Health drain stops.");
    }

    // When player enters a healing zone
    public void enterHealingZone()
    {
        isInHealingZone = true;
        stopHealthDrain();
        Debug.Log("Health Drain Stopped in Healing Zone");
    }

    // When player leaves a healing zone
    public void exitHealingZone()
    {
        isInHealingZone = false;
        Debug.Log("Left Healing Zone");
    }
}
