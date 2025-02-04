using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    // creates an event that other scripts can subscribe to
    public event System.Action OnLevelComplete;
    private int remainingEnemies = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }  


    public void SetInitialEnemyCount(int count)
    {
        remainingEnemies = count;
    }

    public void EnemyDefeated()
    {
        remainingEnemies--;
        if (remainingEnemies <= 0)
        {
            // listens for level completion, broadcasts to all subscribers (FinishLevel.cs) that it's finished 
            OnLevelComplete?.Invoke();
        }
    }
}