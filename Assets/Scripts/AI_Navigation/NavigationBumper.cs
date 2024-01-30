using UnityEngine;
using UnityEngine.AI;

public class NavigationBumper : EntityHover
{
    [SerializeField] Transform player;
    [SerializeField] public NavMeshAgent agentBumper;
    [SerializeField] Player playerObject;

    private void Update()
    {
        if (playerObject.isInvisible == true)
            return;

        else agentBumper.destination = player.position;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
