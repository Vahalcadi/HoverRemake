using UnityEngine;
using UnityEngine.AI;

public class NavigationBumper : EntityHover
{
    [SerializeField] Transform player;
    [SerializeField] public NavMeshAgent agentBumper;
    [SerializeField] Player playerObject;

    private void Update()
    {
        if (playerObject.isInvisible == false)
            agentBumper.destination = player.position;
        else return;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
