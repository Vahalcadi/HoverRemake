using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Pickup
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        player.jumpUses += 1;
    }


}
