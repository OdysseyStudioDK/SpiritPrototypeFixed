using UnityEngine;
using UnityEngine.AI;

public class NPCWalkingController : MonoBehaviour
{
    [Tooltip("List of Transforms serving as checkpoints for the NPC to visit.")]
    [SerializeField]
    private Transform[] checkpoints;

    [Tooltip("Movement speed of the NPC (in units per second).")]
    [SerializeField]
    private float speed = 3.5f;

    private NavMeshAgent agent;
    private int lastCheckpointIndex = -1;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NPCWalkingController requires a NavMeshAgent component.");
            enabled = false;
            return;
        }

        agent.speed = speed;

        if (checkpoints == null || checkpoints.Length == 0)
        {
            Debug.LogWarning("No checkpoints assigned for NPCWalkingController.");
            return;
        }

        MoveToRandomCheckpoint();
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            MoveToRandomCheckpoint();
        }
    }

    private void MoveToRandomCheckpoint()
    {
        if (checkpoints == null || checkpoints.Length == 0)
            return;

        int index = Random.Range(0, checkpoints.Length);
        if (checkpoints.Length > 1)
        {
            while (index == lastCheckpointIndex)
            {
                index = Random.Range(0, checkpoints.Length);
            }
        }
        lastCheckpointIndex = index;

        agent.SetDestination(checkpoints[index].position);
    }
}