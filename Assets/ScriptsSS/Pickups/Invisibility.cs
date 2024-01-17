using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : Pickup
{
    protected override void Start()
    {
        base.Start();
    }

    public override void UsePickup()
    {
        base.UsePickup();
        player.invisibilityUses -= 1;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        player.invisibilityUses -= 1;
    }
}
