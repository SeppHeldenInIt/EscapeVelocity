using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 5;
    [SerializeField] public int CurrentHealth;
    [SerializeField] private int Damage = 1;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Hit()
    {
        CurrentHealth -= Damage;
    }
}
