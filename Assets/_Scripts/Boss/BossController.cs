using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private int currentPhase = 1;
    private float maxHealth;
    private bool isInitialized = false;

    // Define the health percentages for phase transitions
    private const float PHASE_2_THRESHOLD = 0.50f; // 75%
    private const float PHASE_3_THRESHOLD = 0.25f;  // 50%

    private void Start()
    {
        Debug.Log("BossController initialized");

        // Get reference to the Enemy component if not set in inspector
        if (enemy == null)
        {
            enemy = GetComponent<Enemy>();
        }

    }

    private void Update()
    {
        // Initialize on first update to ensure Enemy component has initialized
        if (!isInitialized)
        {
            maxHealth = enemy.GetHealth();
            Debug.Log($"Initializing max health: {maxHealth}");
            isInitialized = true;

            // Return early to avoid checking transitions before we're ready
            return;
        }

        CheckPhaseTransitions();
    }

    private void CheckPhaseTransitions()
    {
        float currentHealth = enemy.GetHealth();
        float healthPercentage = currentHealth / maxHealth;

        if (healthPercentage <= PHASE_3_THRESHOLD && currentPhase < 3)
        {
            TransitionToPhase(3);
        }
        else if (healthPercentage <= PHASE_2_THRESHOLD && currentPhase < 2)
        {
            TransitionToPhase(2);
        }
    }

    private void TransitionToPhase(int newPhase)
    {
        Debug.Log($"Boss entering Phase {newPhase}!");
        currentPhase = newPhase;
    }
}