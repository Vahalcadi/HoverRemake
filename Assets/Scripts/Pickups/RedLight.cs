using UnityEngine;

public class RedLight : Pickup
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (durationTimer < 0)
            player.isSlowedDown = false;
    }

    public override void UsePickup()
    {
        Debug.Log("RedLight picked up");

        base.UsePickup();

        player.isSpedUp = false;
        player.isSlowedDown = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        base.OnTriggerEnter(other);

        CanUsePickup();
    }
}
