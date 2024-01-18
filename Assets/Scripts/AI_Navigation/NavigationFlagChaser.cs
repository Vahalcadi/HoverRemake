using UnityEngine;
using UnityEngine.AI;

public class NavigationFlagChaser : MonoBehaviour
{
    [SerializeField] Transform flag;
    [SerializeField] NavMeshAgent agentFlagChaser;

    private void Update()
    {
        agentFlagChaser.destination = flag.position;
    }
}
