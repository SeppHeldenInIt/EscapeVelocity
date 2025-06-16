using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private string sceneName;
    void Start()
    {
        _health = GetComponent<Health>();
    }

    
    void Update()
    {
        if(_health.CurrentHealth <= 0)
        {
            SceneManager.LoadScene(sceneName);
            GameObject.Destroy(gameObject);
        }
    }
}
