using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LaunchHover : Trap
{
    public float travelSpeed;
    public float trapEnsnaringDuration;
    [SerializeField] Transform throwDirection;

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
    /* protected override void OnTriggerEnter(Collider other)
     {
         if (cooldownTimer > 0)
             return;

         if (other.gameObject.CompareTag("Player") && !player.GetComponent<Player>().isShielded)
         {
             StartCoroutine(LaunchPlayer());
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
 */

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override bool CanActivateTrap(Collider other)
    {
        return base.CanActivateTrap(other);
    }

    protected override void ActivateTrap(Collider other)
    {
        base.ActivateTrap(other);
        TrapTrigger(other);
    }

    private void TrapTrigger(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !player.GetComponent<Player>().isShielded)
        {
            StartCoroutine(LaunchPlayer());
        }
        else if (other.gameObject.CompareTag("EnemyBumper"))
        {
            StartCoroutine(LaunchEnemy(enemyBumper));
        }
        else if (other.gameObject.CompareTag("EnemyFlagChaser"))
        {
            StartCoroutine(LaunchEnemy(enemyFlagChaser));
        }
    }

    private IEnumerator LaunchPlayer()
    {
        InputManager.Instance.OnDisable();

        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);

        player.transform.LookAt(throwDirection);

        yield return new WaitForSeconds(trapEnsnaringDuration);

        player.GetComponent<Rigidbody>().AddForce(player.transform.forward * travelSpeed, ForceMode.Impulse);
    }

    private IEnumerator LaunchEnemy(GameObject enemyHover)
    {
        enemyHover.GetComponent<NavMeshAgent>().enabled = false;

        enemyHover.GetComponent<Rigidbody>().velocity = Vector3.zero;
        enemyHover.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        enemyHover.transform.position = new Vector3(transform.position.x, enemyHover.transform.position.y, transform.position.z);

        enemyHover.transform.LookAt(throwDirection);

        yield return new WaitForSeconds(trapEnsnaringDuration);

        enemyHover.GetComponent<Rigidbody>().AddForce(enemyHover.transform.forward * travelSpeed, ForceMode.Impulse);
    }

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
            enemyFlagChaser.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
