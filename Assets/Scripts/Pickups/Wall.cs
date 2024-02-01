using UnityEngine;

public class Wall : Pickup
{
    protected override void Start()
    {
        base.Start();
    }

    public override bool CanUsePickup()
    {
        player.wallUses += 1;
        Debug.Log("Wall picked up");

        return true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        base.OnTriggerEnter(other);
    }
}
