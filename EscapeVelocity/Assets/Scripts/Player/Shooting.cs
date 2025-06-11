using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    Animator anim;
    [SerializeField] private Transform gun;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            Shoot();
        anim.SetTrigger("shoot");
    }

    private void Shoot()
    {
      
        // instatiate the game object bullet with the Bullet script add gun.position
        //newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
        GameObject bullet = ItemPool.instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = gun.position;
            bullet.SetActive(true);
        }
    }
}
