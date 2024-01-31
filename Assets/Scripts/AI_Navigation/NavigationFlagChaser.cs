using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationFlagChaser : MonoBehaviour
{
    [SerializeField] Transform flag;
    [SerializeField] public NavMeshAgent agentFlagChaser;
    [SerializeField] public int flagsLeft = 3; // doing so makes final score calculation easier
    List<GameObject> spawnpoints = new List<GameObject> ();
    private List<int> extractedSpawnpoints = new();
    bool hasReachedDestination = true;

    private int random;


    private void Start()
    {
        spawnpoints = GameManager.Instance.flagSpawnPoints;
    }

    private void Update()
    {
        SetChaserDestination();
    }

    /**
     * 
     * get flag spawnpointas form gamemanager
     * 
     * send flagchaser to a random waypoint
     * save waypoint index
     * 
     * repeat
     * **/

    private void SetChaserDestination()
    {
        if (hasReachedDestination)
        {
            hasReachedDestination = false;
            CheckExtractedNumber();
        }

        agentFlagChaser.destination = spawnpoints[random].transform.position;

        Debug.Log(hasReachedDestination);
    }

    private void CheckExtractedNumber()
    {
        random = UnityEngine.Random.Range(0, spawnpoints.Count);
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
}
