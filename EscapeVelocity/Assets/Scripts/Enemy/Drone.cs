using UnityEngine;
using UnityEngine.SceneManagement;

public class Drone : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;

    void Update()
    {
        if (target == null) return;

        Vector3 pos = transform.position;
        float newY = Mathf.Lerp(pos.y, target.position.y, followSpeed * Time.deltaTime);
        transform.position = new Vector3(pos.x, newY, pos.z);
    }
}

