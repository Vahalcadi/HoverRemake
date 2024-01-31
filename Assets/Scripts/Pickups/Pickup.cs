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
        RANDOM
    }
    public string pickupName;
    public PickupType type;

    public float effectDuration;
    [NonSerialized] public float durationTimer;

    protected virtual void Start()
    {
        player = Player.Instance.gameObject.GetComponent<Player>();
    }

    protected virtual void Update()
    {
        durationTimer -= Time.deltaTime; 
    }

    public virtual bool CanUsePickup()
    {
        if (durationTimer < 0)
        {
            UsePickup();
            durationTimer = effectDuration;
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
        if (!other.gameObject.CompareTag("Player"))
            return;

        CanUsePickup();
    }

}
