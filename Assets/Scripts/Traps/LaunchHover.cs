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

    protected override bool CanActivateTrap(Collider other)
    {
        return base.CanActivateTrap(other);
    }


    protected override void ActivateTrap(Collider other)
    {
        base.ActivateTrap(other);
        TriggerTrap(other);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    private void TriggerTrap(Collider other)
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
        player.transform.position = Vector3.MoveTowards(player.transform.position, arrivalPosition.position, travelSpeed * Time.deltaTime);
        yield return new WaitForSeconds(trapDuration);

        InputManager.Instance.OnEnable();
    }

    private IEnumerator LaunchEnemy(GameObject enemyHover)
    {
        enemyHover.GetComponent<NavMeshAgent>().enabled = false;
        enemyHover.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        enemyHover.transform.position = Vector3.MoveTowards(enemyHover.transform.position, arrivalPosition.position, travelSpeed * Time.deltaTime);

        yield return new WaitForSeconds(trapDuration);

        enemyHover.GetComponent<NavMeshAgent>().enabled = true;
    }
}
