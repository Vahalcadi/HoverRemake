using UnityEngine;

public class Invisibility : Pickup
{
    protected override void Start()
    {
        base.Start();
    }
    public override bool CanUsePickup()
    {
        player.invisibilityUses += 1;
        Debug.Log("Invis picked up");

        return true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        CanUsePickup();
    }
}
