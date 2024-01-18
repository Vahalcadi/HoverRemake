using UnityEngine;
using UnityEngine.AI;

public class NavigationBumper : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] NavMeshAgent agentBumper;

    private void Update()
    {
        agentBumper.destination = player.position;
    }
}
