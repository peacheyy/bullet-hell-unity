using Unity.Mathematics;
using UnityEngine;

public class RotateBulletSpawner : MonoBehaviour {
    
    [SerializeField] Transform bulletSpawner;

    private float timer = 0f;
    private float degrees = 360f;

    void Update() {
        transform.Rotate(0, 0, degrees * Time.deltaTime);
    }
}
