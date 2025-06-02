using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private Health _health;
    void Start()
    {
        _health = GetComponent<Health>();
    }

    
    void Update()
    {
        if(_health.CurrentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
