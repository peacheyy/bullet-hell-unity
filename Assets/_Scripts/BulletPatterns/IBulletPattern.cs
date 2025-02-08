using UnityEngine;

public interface IBulletPattern
{
    // methods needed for bullet pattern interface 
    void Execute(Transform spawner, Transform[] firePoints);
    void Reset();
}