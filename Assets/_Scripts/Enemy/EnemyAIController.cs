using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    [SerializeField] NavMeshSurface navMeshSurface;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] GameObject player;

    void Awake()
    {
        navMeshAgent.updateUpAxis = false;
    }

    void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }
}