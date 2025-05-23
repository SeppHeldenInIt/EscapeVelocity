using UnityEngine;

public class WallMover : MonoBehaviour
{
    public float speed = 5f;
    public float destroyX = -15f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}