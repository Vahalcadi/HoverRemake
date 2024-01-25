using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HoldHover : MonoBehaviour
{

    [SerializeField] private float trapDuration;
    [SerializeField] private GameObject enemyFlagChaser;
    [SerializeField] private GameObject enemyBumper;
    [SerializeField] private GameObject player;

    public float cooldown;
    [NonSerialized] public float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public bool CanActivateTrap(Collider other)
    {
        if (cooldownTimer < 0)
        {
            ActivateTrap(other);
            cooldownTimer = cooldown;
            return true;
        }

        return false;
    }

    public virtual void ActivateTrap(Collider other)
    {
        StartCoroutine(TriggerTrap(other));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(cooldownTimer < 0)
            CanActivateTrap(other);
    }
    private IEnumerator TriggerTrap(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !player.GetComponent<Player>().isShielded)
        {

            InputManager.Instance.OnDisable();
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.transform.position = gameObject.transform.position;
            yield return new WaitForSeconds(trapDuration);

            InputManager.Instance.OnEnable();
        }
        else if (other.gameObject.CompareTag("EnemyBumper"))
        {
            

            //serve mappa 
        }
        else if (other.gameObject.CompareTag("EnemyFlagChaser"))
        {
            
            enemyFlagChaser.GetComponent<NavMeshAgent>().enabled = false;
            enemyFlagChaser.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            enemyFlagChaser.transform.position = gameObject.transform.position;

            yield return new WaitForSeconds(trapDuration);

            enemyFlagChaser.GetComponent<NavMeshAgent>().enabled = true;

        }

    }
}