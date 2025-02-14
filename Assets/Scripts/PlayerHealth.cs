using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public float maxHealth = 100f;
    public float currentHealth;
    public float healthDrainRate = 1f;
    private Coroutine healthDrainCoroutine;


    void Start()
    {
        currentHealth = maxHealth;
        
    }

    public void startHealthDrain()
    {
        if (healthDrainCoroutine == null)
        {
            healthDrainCoroutine = StartCoroutine(DrainHealth());
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

    IEnumerator DrainHealth()
    {
        while (currentHealth > 0)
        {
            currentHealth -= healthDrainRate;
            // currentHealth = Mathf.Max(currentHealth, 0, maxHealth);
            Debug.Log("Current Health: " + currentHealth);
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Player is dead");
    }
}
