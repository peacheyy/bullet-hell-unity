using UnityEngine;

public class PauseMenu : MonoBehaviour
{   
    public static PauseMenu instance { get; private set; }
    [SerializeField] GameObject pauseMenu;
    private bool gamePaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            // ternary operator. basically changes the value of timeScale to 0 if gamePaused
            gamePaused = !gamePaused;
            
            Time.timeScale = gamePaused ? 0 : 1;
            pauseMenu.SetActive(gamePaused);

        }
    }

    public bool getGameStatus()
    {
        return gamePaused;
    }

}
