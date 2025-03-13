using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour
{
    [Header("Heart Sprites")]
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite halfHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;
    
    Image heartImage;
    
    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch(status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeartSprite;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeartSprite;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullHeartSprite;
                break;
        }
    }
}

public enum HeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2
}