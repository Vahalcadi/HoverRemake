using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HoldHover : Trap
{

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

            StartCoroutine(EnsnarePlayer());
        }
        else if (other.gameObject.CompareTag("EnemyBumper"))
        {
            StartCoroutine(EnsnareEnemy(enemyBumper));
        }
        else if (other.gameObject.CompareTag("EnemyFlagChaser"))
        {
            StartCoroutine(EnsnareEnemy(enemyFlagChaser));
        }

    }

    private IEnumerator EnsnareEnemy(GameObject enemyHover)
    {
        enemyHover.GetComponent<NavMeshAgent>().enabled = false;
        enemyHover.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        enemyHover.GetComponent<Rigidbody>().velocity = Vector3.zero;

        enemyHover.transform.position = gameObject.transform.position;

        yield return new WaitForSeconds(trapDuration);

        enemyHover.GetComponent<NavMeshAgent>().enabled = true;
    }

    private IEnumerator EnsnarePlayer()
    {
        InputManager.Instance.OnDisable();
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(trapDuration);

        InputManager.Instance.OnEnable();
    }
}