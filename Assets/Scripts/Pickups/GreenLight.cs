using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLight : Pickup
{


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (durationTimer < 0)
            player.isSpedUp = false;
    }

    public override bool CanUsePickup()
    {
        return base.CanUsePickup();
    }

    public override void UsePickup()
    {
        Debug.Log("GreenLight picked up");

        base.UsePickup();

        player.isSlowedDown = false;
        player.isSpedUp = true;
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        base.OnTriggerEnter(other);

        CanUsePickup();
        
    }
}
