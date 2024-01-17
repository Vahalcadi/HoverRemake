using UnityEngine;

public class Shield : Pickup
{

    protected override void Start()
    {
        base.Start();
    }

    public override void UsePickup()
    {
        base.UsePickup();

        player.isShielded = true;

        if(cooldownTimer < 0)
            player.isShielded = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        CanUsePickup();
    }
}
