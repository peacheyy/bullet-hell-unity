using UnityEngine;
using System.Collections.Generic;

public class BulletPoolManager : MonoBehaviour
{
    private static BulletPoolManager _instance;
    public static BulletPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //FindObjectsOfType is depreciated, in order to comply with unity 6 I had to change it to FindObjectsByType
                //The key difference here is that instead of setting instance to the type BulletPoolManager
                //I have to set it to an array because FindObjectsByType<BulletPoolManager> returns an array
                BulletPoolManager[] managers = FindObjectsByType<BulletPoolManager>(FindObjectsSortMode.None);
                if (managers.Length > 0)
                {
                    _instance = managers[0];
                    if (managers.Length > 1)
                    {
                        Debug.LogWarning("Multiple BulletPoolManager instances found in scene. Using the first one.");
                    }
                }
                else
                {
                    Debug.LogError("No BulletPoolManager found in scene!");
                }
            }
            return _instance;
        }
    }

    [System.Serializable]
    public class BulletPoolInfo
    {
        public string key; // bullet keys ("PlayerBullet", "EnemyBullet")
        public Bullet bulletPrefab; // the bullet prefab to pull
        public int initialPoolSize = 50; // how many bullets to instantiate
    }

    [SerializeField] private BulletPoolInfo[] bulletPools;
    private Dictionary<string, ObjectPool<Bullet>> pools;

    // before any start/update function this will run
    // Awake is especially useful for gameObjects
    // it's called only once and is used to initalize 
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            InitializePools();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // initalizes the bullet pool of the player and enemy
    private void InitializePools()
    {
        pools = new Dictionary<string, ObjectPool<Bullet>>();

        foreach (var bulletPool in bulletPools)
        {
            pools[bulletPool.key] = new ObjectPool<Bullet>(
                bulletPool.bulletPrefab,
                bulletPool.initialPoolSize,
                bulletPool.key,  // Pass the key to the pool
                transform
            );
        }
    }

    public Bullet GetBullet(string key, Vector3 position, Quaternion rotation)
    {
        if (!pools.ContainsKey(key))
        {
            Debug.LogError($"Bullet pool with key {key} doesn't exist!");
            return null;
        }

        return pools[key].Get(position, rotation);
    }

    public void ReturnBullet(string key, Bullet bullet)
    {
        if (pools.ContainsKey(key))
        {
            pools[key].Return(bullet);
        }
        else
        {
            Debug.LogError($"Attempting to return bullet to non-existent pool: {key}");
        }
    }
}