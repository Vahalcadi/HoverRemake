using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickup : Pickup
{
    public List<Pickup> Pickups = new();
    int index;

    protected override void Start()
    {
        base.Start();
        Pickups.Add(FindFirstObjectByType<Wall>());
        Pickups.Add(FindFirstObjectByType<GreenLight>());
        Pickups.Add(FindFirstObjectByType<Invisibility>());
        Pickups.Add(FindFirstObjectByType<RedLight>());
        Pickups.Add(FindFirstObjectByType<Shield>());
        Pickups.Add(FindFirstObjectByType<Jump>());
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanUsePickup()
    {
        return base.CanUsePickup();
    }

    public override void UsePickup()
    {
        base.UsePickup();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        base.OnTriggerEnter(other);

        index = Random.Range(0, Pickups.Count);
      
        switch (index)
        {
            case 0:
                Pickups[index].CanUsePickup();
                break;
            case 1:
                Pickups[index].CanUsePickup();
                break;
            case 2:
                Pickups[index].CanUsePickup();
                break;
            case 3:
                Pickups[index].CanUsePickup();
                break;
            case 4:
                Pickups[index].CanUsePickup();
                break;
            case 5:
                Pickups[index].CanUsePickup();
                break;
            
        }
    }
}
