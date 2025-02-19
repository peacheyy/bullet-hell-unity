using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    [SerializeField] Player player;

    private bool isGameOver = false;

    void Update()
    {
        // Only check if we haven't already triggered game over
        if (!isGameOver && player != null)
        {
            // Check if player health is zero or below
            if (player.GetHealth() <= 0)
            {
                TriggerGameOver();
            }
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        gameOver.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene(0);
    }
}