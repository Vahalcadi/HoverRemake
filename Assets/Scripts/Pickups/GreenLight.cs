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
        {
            player.GetComponent<Player>().isSpedUp = false;
        }
    }

    public override bool CanUsePickup()
    {
        return base.CanUsePickup();
    }

    public override void UsePickup()
    {
        Debug.LogWarning("GreenLight picked up");

        base.UsePickup();

        player.GetComponent<Player>().isSlowedDown = false;

        player.GetComponent<Player>().isSpedUp = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {

        if (!other.gameObject.CompareTag("Player"))
            return;

        Debug.Log("GreeenLight");
        base.OnTriggerEnter(other);

        CanUsePickup();

    }
}
