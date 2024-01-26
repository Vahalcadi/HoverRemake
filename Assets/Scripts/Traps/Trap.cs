using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] protected float trapDuration;
    [SerializeField] protected GameObject enemyFlagChaser;
    [SerializeField] protected GameObject enemyBumper;
    [SerializeField] protected GameObject player;

    public float cooldown;
    [NonSerialized] public float cooldownTimer;

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    protected virtual bool CanActivateTrap(Collider other)
    {
        if (cooldownTimer < 0)
        {
            ActivateTrap(other);
            cooldownTimer = cooldown;
            return true;
        }

        return false;
    }

    protected virtual void ActivateTrap(Collider other)
    {
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (cooldownTimer < 0)
            CanActivateTrap(other);
    }
}
