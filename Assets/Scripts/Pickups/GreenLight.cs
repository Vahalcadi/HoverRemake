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

        if (cooldownTimer < 0)
            player.isSpedUp = false;
    }

    public override bool CanUsePickup()
    {
        return base.CanUsePickup();
    }

    public override void UsePickup()
    {
        Debug.Log("contact");
        Debug.Log(cooldown);
        Debug.Log(cooldownTimer);

        base.UsePickup();

        player.isSpedUp = true;
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        CanUsePickup();
        
    }
}
