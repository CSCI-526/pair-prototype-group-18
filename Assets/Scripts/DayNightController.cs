using System.Collections;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public SpriteRenderer sunSprite;  // Assign your Sun's SpriteRenderer in Inspector
    public Color dayColor = Color.yellow;
    public Color nightColor = Color.black; // Dark navy blue for night
    public Color dayBackgroundColor = new Color(0.5f, 0.8f, 1.0f); // Light blue sky
    public Color nightBackgroundColor = new Color(0.05f, 0.05f, 0.2f); // Dark night sky
    public float transitionDuration = 2.0f; // Time for smooth transition

    private bool isDay = true;
    public PlayerHealth playerHealth;
    public Camera mainCamera;

    void Start()
    {
        InvokeRepeating("ToggleDayNight", 0, 10f); // Switch every 45 seconds
    }

    void ToggleDayNight()
    {
        isDay = !isDay;
        StartCoroutine(ChangeColor(isDay ? dayColor : nightColor));
        StartCoroutine(ChangeBackgroundColor(isDay ? dayBackgroundColor : nightBackgroundColor));

        // Inform PlayerHealth about Day/Night Change
        playerHealth.setNightTime(!isDay);

        // Update Hidden Paths Visibility Based on Time
        HiddenPathController[] hiddenPaths = FindObjectsOfType<HiddenPathController>();
        foreach (HiddenPathController path in hiddenPaths)
        {
            path.SetNightMode(isDay);
        }

        LadderController[] climbablePlatforms = FindObjectsOfType<LadderController>();
        foreach (LadderController platform in climbablePlatforms)
        {
            platform.SetNightMode(isDay);
        }

        VisibleController[] visibleObstacles = FindObjectsOfType<VisibleController>();
        foreach (VisibleController obs in visibleObstacles)
        {
            obs.SetNightMode(isDay);
        }

        InvisibleController[] invisibleObstacles = FindObjectsOfType<InvisibleController>();
        foreach (InvisibleController obs in invisibleObstacles)
        {
            obs.SetNightMode(isDay);
        }
    }

    IEnumerator ChangeColor(Color targetColor)
    {
        Color startColor = sunSprite.color;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            sunSprite.color = Color.Lerp(startColor, targetColor, elapsedTime / transitionDuration);
            yield return null;
        }

        sunSprite.color = targetColor;
    }

    IEnumerator ChangeBackgroundColor(Color targetColor)
    {
        Color startColor = mainCamera.backgroundColor;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            mainCamera.backgroundColor = Color.Lerp(startColor, targetColor, elapsedTime / transitionDuration);
            yield return null;
        }

        mainCamera.backgroundColor = targetColor;
    }
}