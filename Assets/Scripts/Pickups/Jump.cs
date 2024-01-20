using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Pickup
{
    protected override void Start()
    {
        base.Start();
    }

    public override bool CanUsePickup()
    {
        player.jumpUses += 1;
        Debug.Log("Jump picked up");

        return true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "player")
            return;

        base.OnTriggerEnter(other);

        CanUsePickup();
    }


}
