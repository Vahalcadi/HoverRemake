using UnityEngine;

public class EntityHover : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Tag your bouncy object appropriately
        {
            // Apply force based on collision info, adjust as necessary
            rb.AddForce(-collision.contacts[0].normal * 10, ForceMode.Impulse);
        }
    }

}
