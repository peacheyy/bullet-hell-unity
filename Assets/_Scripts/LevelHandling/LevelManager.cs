using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject boss;

    public static LevelManager Instance { get; private set; }
    // creates an event that other scripts can subscribe to
    public event System.Action OnLevelComplete;

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

    public void BossDefeated()
    {
        // listens for level completion, broadcasts to all subscribers (FinishLevel.cs) that it's finished 
        OnLevelComplete?.Invoke();
    }
}