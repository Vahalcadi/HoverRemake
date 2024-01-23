using System.Collections;
using UnityEngine;

public class HoldHover : MonoBehaviour
{

    [SerializeField] private float trapDuration;
    [SerializeField] private GameObject enemyFlagChaser;
    [SerializeField] private GameObject enemyBumper;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TriggerTrap(other));
    }
    private IEnumerator TriggerTrap(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InputManager.Instance.OnDisable();
            yield return new WaitForSeconds(trapDuration);

            InputManager.Instance.OnEnable();
        }
        else if (other.gameObject.CompareTag("EnemyBumper"))
        {
            

            //serve mappa 
        }
        else if (other.gameObject.CompareTag("EnemyFlagChaser"))
        {

        }

    }


}