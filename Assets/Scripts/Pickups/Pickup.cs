using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Player player;

    public enum PickupType
    {
        JUMP,
        WALL,
        INVISIBILITY,
        SHIELD,
        GREEN_LIGHT,
        RED_LIGHT,
        DELETE_MAP,
        MISTERY_OBJECT
    }
    public string pickupName;
    public PickupType type;

    public float cooldown;
    [NonSerialized] public float cooldownTimer;

    protected virtual void Start()
    {
        player = Player.Instance;  
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime; 
    }

    public virtual bool CanUsePickup()
    {
        if (cooldownTimer < 0)
        {
            UsePickup();
            cooldownTimer = cooldown;
            return true;
        }

        return false;
    }
    public virtual void UsePickup()
    {
        //specific for each pickup
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "player")
            return;
    }

}
