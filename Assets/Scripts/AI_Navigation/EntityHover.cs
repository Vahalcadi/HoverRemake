using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EntityHover : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Tag your bouncy object appropriately
            rb.AddForce(-collision.GetContact(0).normal * 10, ForceMode.Impulse);

        StartCoroutine(Collision(collision));  
    }

    private IEnumerator Collision(Collision collision)
    {  
        yield return new WaitForSeconds(3);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
