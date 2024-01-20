using UnityEngine;

public class Shield : Pickup
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (durationTimer < 0)
            player.isShielded = false;
    }

    public override void UsePickup()
    {

        Debug.Log("Shield picked up");

        base.UsePickup();

        player.isShielded = true;

    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        base.OnTriggerEnter(other);

        CanUsePickup();
    }
}
