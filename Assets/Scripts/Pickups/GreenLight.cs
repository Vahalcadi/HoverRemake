using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLight : Pickup
{


    protected override void Start()
    {
        base.Start();
    }

    public override void UsePickup()
    {
        base.UsePickup();

        player.isSpedUp = true;

        if (cooldownTimer < 0)
            player.isSpedUp = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        CanUsePickup();
    }
}
