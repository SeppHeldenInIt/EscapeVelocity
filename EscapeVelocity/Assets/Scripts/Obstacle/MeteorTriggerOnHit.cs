using UnityEngine;

public class MeteorTriggerOnHit : MonoBehaviour
{
    private Collider2D col;
    private bool alreadyHit = false;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (alreadyHit) return;

        if (collision.gameObject.CompareTag("Player"))
        {
           
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.Hit();
            }

            
            col.isTrigger = true;
            alreadyHit = true;
        }
    }
}
