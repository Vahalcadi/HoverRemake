using UnityEngine;
using UnityEngine.AI;

public class NavigationBumper : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] NavMeshAgent agentBumper;
    [SerializeField] Player playerObject;

    private void Update()
    {
        if (playerObject.isInvisible == false)
            agentBumper.destination = player.position;
        else return;
    }
}
