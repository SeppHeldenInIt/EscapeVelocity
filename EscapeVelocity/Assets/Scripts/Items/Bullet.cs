using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float cooldown;

    private Vector3 bDirection = Vector3.right;

    private void Update()
    {
        cooldown -= 1 * Time.deltaTime;
        transform.position += bDirection * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }

        else
        {
            if (cooldown >= 0)
            Destroy(gameObject);
        }
    }

    //private IEnumerator DisableAfterDelay(float delay)
    //{
     //   yield return new WaitForSeconds(delay);

    //    gameObject.SetActive(false);
    //}

}
