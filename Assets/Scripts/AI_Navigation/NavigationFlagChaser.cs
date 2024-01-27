using UnityEngine;
using UnityEngine.AI;

public class NavigationFlagChaser : MonoBehaviour
{
    [SerializeField] Transform flag;
    [SerializeField] public NavMeshAgent agentFlagChaser;
    [SerializeField] public int flagsLeft = 3; // doing so makes final score calculation easier

    private void Update()
    {
        agentFlagChaser.destination = flag.position;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flag"))
        {
            other.gameObject.SetActive(false);
            flagsLeft--;
            Debug.Log("Flag picked by enemy");
        }
    }
}
