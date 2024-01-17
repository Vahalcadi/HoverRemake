using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLight : Pickup
{
    protected override void Start()
    {
        base.Start();
    }

    public override void UsePickup()
    {
        base.UsePickup();

        player.isSlowedDown = true;

        if (cooldownTimer < 0)
            player.isSlowedDown = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        CanUsePickup();
    }
}
