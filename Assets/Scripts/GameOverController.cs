using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public void GameOver()
    {
        Debug.Log("Game Over");
        Application.Quit(); // End the game
    }
}