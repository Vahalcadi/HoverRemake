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
            player.GetComponent<Player>().isShielded = false;
            
    }

    public override void UsePickup()
    {

        Debug.Log("Shield picked up");

        base.UsePickup();

        player.GetComponent<Player>().isShielded = true;

    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        base.OnTriggerEnter(other);

        CanUsePickup();
    }
}
