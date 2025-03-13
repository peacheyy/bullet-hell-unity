using System.Collections.Generic;
using UnityEngine;

public class HeartDisplay : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;
    [SerializeField] Player player;

    List<HeartController> hearts = new List<HeartController>();

    void Start()
    {
        InitializeHearts();
    }

    void Update()
    {
        // Update heart visuals based on current health
        UpdateHeartVisuals();
    }

    public void InitializeHearts()
    {
        ClearHearts();

        // Calculate how many hearts to create (1 heart = 2 health points)
        int heartsToMake = Mathf.CeilToInt(player.GetMaxHealth() / 2f);

        // Create the appropriate number of hearts
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateHeart();
        }

        // Update visuals to show current health
        UpdateHeartVisuals();
    }

    private void CreateHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HeartController heartComponent = newHeart.GetComponent<HeartController>();
        hearts.Add(heartComponent);
    }

    public void UpdateHeartVisuals()
    {
        float currentHealth = player.GetHealth();

        for (int i = 0; i < hearts.Count; i++)
        {
            // Calculate which health points this heart represents
            int heartStartHealth = i * 2;

            if (currentHealth >= heartStartHealth + 2)
            {
                // Full heart
                hearts[i].SetHeartImage(HeartStatus.Full);
            }
            else if (currentHealth > heartStartHealth)
            {
                // Half heart
                hearts[i].SetHeartImage(HeartStatus.Half);
            }
            else
            {
                // Empty heart
                hearts[i].SetHeartImage(HeartStatus.Empty);
            }
        }
    }

    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HeartController>();
    }
}