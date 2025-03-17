using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> where T : Component
{
    private T prefab;
    private Transform parent;
    private Queue<T> pool; // stores inactive bullets
    private int defaultSize;
    private string poolKey;

    public ObjectPool(T prefab, int initialSize, string key, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        this.defaultSize = initialSize;
        this.poolKey = key;
        pool = new Queue<T>();
        
        // Pre-instantiate objects
        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject();
        }
    }

    // if there is no bullets in the pool queue, this method is called to instantiate a new bullet
    private void CreateNewObject()
    {
        T obj = GameObject.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        
        if (obj is Bullet bullet)
        {
            bullet.InitializePool(poolKey);
        }
        
        pool.Enqueue(obj);
    }

    // When the game needs a bullet, if there's a bullet in the pool grab that instead of creating a new one
    public T Get(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            CreateNewObject();
        }

        // resuse existing bullets instead of creating new ones
        T obj = pool.Dequeue();
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        // activates the bullet
        obj.gameObject.SetActive(true);
        
        return obj;
    }

    // deactivate and store for future use
    public void Return(T obj)
    {   
        // deactivates the bullet and adds it to the pool queue
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    public void ReturnAll()
    {
        // Find all active instances and return them to pool
        T[] activeInstances = GameObject.FindObjectsByType<T>(FindObjectsSortMode.None);
        foreach (T obj in activeInstances)
        {
            Return(obj);
        }
    }
}