using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Pickup
{
    protected override void Start()
    {
        base.Start();
    }

    public override void UsePickup()
    {
        base.UsePickup();

        player.jumpUses -= 1; 
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        player.jumpUses += 1;
    }


}
