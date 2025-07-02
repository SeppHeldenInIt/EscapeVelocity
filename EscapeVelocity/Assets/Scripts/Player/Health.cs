using System;
using System.Collections;
using System.Collections.Generic;
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
    
    // here is where we create the audio clip that can then be called by the manager script.
    [SerializeField] private AudioClip damageSoundClip;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Hit()
    {
        CurrentHealth -= Damage;
        // this is where play the sound fx
        SoundFXManager.instance.PLaySoundFXClip(damageSoundClip, transform, 1f);
    }

    void Update()
    {
        if (MaxHealth > numOfHearts)
        {
            MaxHealth = numOfHearts;
        }
    }
}
