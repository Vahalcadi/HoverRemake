using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject player;

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

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

        if (durationTimer > 0)
        {
            durationTimer -= Time.deltaTime;
            Debug.Log(durationTimer);
        }

    }

    public virtual bool CanUsePickup()
    {
        if (durationTimer <= 0)
        {
            UsePickup();
            Debug.Log("Used");
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

        GameManager.Instance.pickupsToRemove.Add(gameObject);
        GameManager.Instance.numberOfPickupsToRemove++;

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }

}
