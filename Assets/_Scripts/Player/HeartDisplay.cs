using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{
    [Header("Heart Sprites")]
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite halfHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;
    
    [Header("Heart Containers")]
    [SerializeField] private Image[] heartImages;
    
    [Header("Settings")]
    [SerializeField] private int healthPerHeart = 2; // 2 health points = 1 heart
    
    private void Start()
    {
        // Subscribe to player health changes
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.OnHealthChanged += UpdateHearts;
            // Initial update to set starting hearts
            UpdateHearts(player.GetHealth(), player.GetMaxHealth());
        }
    }
    
    public void UpdateHearts(float currentHealth, float maxHealth)
    {
        // Calculate how many hearts to show (rounded up for max)
        int maxHearts = Mathf.CeilToInt(maxHealth / healthPerHeart);
        
        // Make sure we have enough heart images
        if (heartImages.Length < maxHearts)
        {
            Debug.LogWarning("Not enough heart images for max health!");
            maxHearts = heartImages.Length;
        }
        
        // Update each heart image
        for (int i = 0; i < heartImages.Length; i++)
        {
            // Hide excess hearts
            if (i >= maxHearts)
            {
                heartImages[i].enabled = false;
                continue;
            }
            
            // Make sure heart is visible
            heartImages[i].enabled = true;
            
            // Calculate health for this heart
            float heartHealthStart = i * healthPerHeart;
            float remainingHealth = currentHealth - heartHealthStart;
            
            // Set appropriate sprite
            if (remainingHealth >= healthPerHeart)
            {
                heartImages[i].sprite = fullHeartSprite;
            }
            else if (remainingHealth > 0)
            {
                heartImages[i].sprite = halfHeartSprite;
            }
            else
            {
                heartImages[i].sprite = emptyHeartSprite;
            }
        }
    }
    
    private void OnDestroy()
    {
        // Unsubscribe to prevent errors
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.OnHealthChanged -= UpdateHearts;
        }
    }
}