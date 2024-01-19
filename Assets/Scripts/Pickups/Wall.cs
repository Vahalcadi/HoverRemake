using UnityEngine;

public class Wall : Pickup
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();


    }

    public override void UsePickup()
    {
        base.UsePickup();
        player.wallUses -= 1;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        player.wallUses += 1;
    }


}
