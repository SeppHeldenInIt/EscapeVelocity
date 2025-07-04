using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 5;
    [SerializeField] public int CurrentHealth;
    [SerializeField] private int Damage = 1;
    [SerializeField] public int numOfHearts;
    [SerializeField] public Image[] hearts;
    [SerializeField] public Sprite Piskel;
    [SerializeField] public Sprite emptyheart;
    [SerializeField] public Animator anim;
    private DamageFlash damageFlash;






    private void Start()
    {
        CurrentHealth = MaxHealth;
        numOfHearts = MaxHealth;
        damageFlash = GetComponent<DamageFlash>();
        Debug.Log(gameObject.name + " starting with " + CurrentHealth + " health.");
        UpdateHearts();
    }

    public void Hit()
    {
        CurrentHealth -= Damage;

        if (damageFlash != null)
        {
            damageFlash.FlashRed();

        }

        numOfHearts = CurrentHealth;
        UpdateHearts();



    }

    public void UpdateHearts()
    {
        Debug.Log($"{gameObject.name} is updating hearts. Health: {CurrentHealth}");

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < CurrentHealth)
                hearts[i].sprite = Piskel;
            else
                hearts[i].sprite = emptyheart;

            hearts[i].enabled = (i < numOfHearts);
        }
    }

}
    
