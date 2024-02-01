using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationFlagChaser : EntityHover
{
    [SerializeField] Transform flag;
    [SerializeField] public NavMeshAgent agentFlagChaser;
    [SerializeField] public int flagsLeft = 3; // doing so makes final score calculation easier
    List<GameObject> spawnpoints = new List<GameObject>();
    private List<int> extractedSpawnpoints = new();
    bool hasReachedDestination;

    private int random;

    /**
     * 
     * Rolls the first destination
     * 
     * **/
    private void Start()
    {
        spawnpoints = GameManager.Instance.flagSpawnPoints;
        CheckExtractedNumber();
    }

    private void Update()
    {
        SetChaserDestination();
    }

    /**
     * 
     * The function will set a new destination when FlagChaser reaches the previous one
     * 
     * **/

    private void SetChaserDestination()
    {
        if (hasReachedDestination)
        {
            hasReachedDestination = false;
            CheckExtractedNumber();
        }

        //agentFlagChaser.destination = spawnpoints[random].transform.position;
        agentFlagChaser.destination = new Vector3(spawnpoints[random].transform.position.x, agentFlagChaser.transform.position.y, spawnpoints[random].transform.position.z);
    }

    /**
     * 
     * 
     * 
     * **/
    private void CheckExtractedNumber()
    {
        random = Random.Range(0, spawnpoints.Count);
        if (extractedSpawnpoints.Contains(random))
        {
            CheckExtractedNumber();
        }
        extractedSpawnpoints.Add(random);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerFlag"))
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.EnemyHasPickedUpFlag();
            flagsLeft--;
            Debug.Log("Flag picked by enemy");
        }

        if (other.gameObject.CompareTag("spawnpoint"))
        {
            hasReachedDestination = true;
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
