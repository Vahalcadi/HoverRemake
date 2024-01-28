using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LaunchHover : Trap
{
    [SerializeField] private Transform arrivalPosition;
    public float travelSpeed;

    protected override void Update()
    {
        base.Update();
    }

    /**
     * the trap implements a cooldown system. If cooldownTimer is greater than 0, it means it is on cooldown
     * 
     * Upon triggering contact with a gameobject, it checks if it is either a player or an enemy and disables the corresponding movement agent
     * 
     * **/
    protected override void OnTriggerEnter(Collider other)
    {
        if (cooldownTimer > 0)
            return;

        if (other.gameObject.CompareTag("Player") && !player.GetComponent<Player>().isShielded)
        {
            InputManager.Instance.OnDisable();
        }
        else if (other.gameObject.CompareTag("EnemyBumper"))
        {
            enemyBumper.GetComponent<NavMeshAgent>().enabled = false;  
        }
        else if (other.gameObject.CompareTag("EnemyFlagChaser"))
        {
            enemyFlagChaser.GetComponent<NavMeshAgent>().enabled = false; 
        }
    }


    /**
     * 
     * After OnTriggerEnter is called, either the player or the enemy is forcefully moved to a (Vector3) destination
     * 
     * the angularVelocity and Velocity of the gameobjects is set to zero every frame to prevent them from slipping away
     * 
     * **/
    private void OnTriggerStay(Collider other)
    {
        if (cooldownTimer > 0)
            return;

        if (other.gameObject.CompareTag("Player") && !player.GetComponent<Player>().isShielded)
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            player.transform.position = Vector3.MoveTowards(player.transform.position, arrivalPosition.position, travelSpeed * Time.deltaTime);
        }
        else if (other.gameObject.CompareTag("EnemyBumper"))
        {
            enemyBumper.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            enemyBumper.GetComponent<Rigidbody>().velocity = Vector3.zero;
            enemyBumper.transform.position = Vector3.MoveTowards(enemyBumper.transform.position, arrivalPosition.position, travelSpeed * Time.deltaTime);
        }
        else if (other.gameObject.CompareTag("EnemyFlagChaser"))
        {
            enemyFlagChaser.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            enemyFlagChaser.GetComponent<Rigidbody>().velocity = Vector3.zero;
            enemyFlagChaser.transform.position = Vector3.MoveTowards(enemyFlagChaser.transform.position, arrivalPosition.position, travelSpeed * Time.deltaTime);
        }
    }


    /**
     * After either the player or the enemy reaches the destination, their movement agents get set to enable
     * 
     * cooldownTimer is then set to cooldown
     * **/
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !player.GetComponent<Player>().isShielded)
        {
            InputManager.Instance.OnEnable();
        }
        else if (other.gameObject.CompareTag("EnemyBumper"))
        {
            enemyBumper.GetComponent<NavMeshAgent>().enabled = true;
        }
        else if (other.gameObject.CompareTag("EnemyFlagChaser"))
        {
            enemyFlagChaser.GetComponent<NavMeshAgent>().enabled = false;
        }

        if(cooldownTimer < 0)
            cooldownTimer = cooldown;
    }
}
