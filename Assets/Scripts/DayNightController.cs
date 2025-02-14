using System.Collections;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public SpriteRenderer sunSprite;  // Assign your Sun's SpriteRenderer in Inspector
    public Color dayColor = Color.yellow;
    public Color nightColor = Color.blue; // Dark navy blue for night
    public float transitionDuration = 2.0f; // Time for smooth transition

    private bool isDay = true;

    void Start()
    {
        InvokeRepeating("ToggleDayNight", 0, 45f); // Switch every 45 seconds
    }

    void ToggleDayNight()
    {
        isDay = !isDay;
        StartCoroutine(ChangeColor(isDay ? dayColor : nightColor));
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
}