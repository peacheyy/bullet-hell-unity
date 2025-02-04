using UnityEngine;

public class FinishLevel : MonoBehaviour
{

    private void Start()
    {
        // "subscribes" to the event 
        LevelManager.Instance.OnLevelComplete += HandleLevelComplete;
    }

    private void OnDestroy()
    {
        if (LevelManager.Instance != null)
        {
            // this "unsubscribes" and prevents memory leaks
            LevelManager.Instance.OnLevelComplete -= HandleLevelComplete;
        }
    }

    private void HandleLevelComplete()
    {
        // calls NextLevel in SceneController
        SceneController.instance.NextLevel();
    }
}