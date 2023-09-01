using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;

    private Rigidbody bulletRb;

    private void Awake()
    {
        bulletRb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        bulletRb.velocity = transform.right * speed;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            gameObject.SetActive(false); // Deactivate the bullet
        }
    }
}