using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerateMap : Pickup
{
    public List<GameObject> partsOfMap;

    protected override void Start()
    {
        base.Start();
    }

    public override void UsePickup()
    {
        base.UsePickup();

        foreach (var part in partsOfMap)
        {
            part.SetActive(true);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        CanUsePickup();
    }
}
