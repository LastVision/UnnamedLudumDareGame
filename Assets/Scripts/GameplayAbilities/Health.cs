﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float MaxHP = 100f;
    public float HPRegenPerSec = 0f;
    public float InvincibilityTime = 0f;
    private float TimeSinceDamage = 0f;
    private float CurrentHP;
    public Text DisplayHealth;
    public List<AudioClip> hurtSounds = new List<AudioClip>();
    public AudioClip deathSound;
    public float combatCooldown;
    void Start()
    {
        CurrentHP = MaxHP;
        
    }
    void Update()
    {
        TimeSinceDamage += Time.deltaTime;

        if (CurrentHP < MaxHP && CurrentHP > 0f && TimeSinceDamage > combatCooldown)
        {
            CurrentHP += HPRegenPerSec * Time.deltaTime;
            CurrentHP = Mathf.Min(CurrentHP, MaxHP);
        }

        if (DisplayHealth)
        {
            DisplayHealth.text = string.Format("Health: {0}/{1}", Mathf.Ceil(CurrentHP), MaxHP);
        }
    }
    public void Damage(float Damage)
    {
        if (CurrentHP <= 0f)
        {
            return;
        }

        if (TimeSinceDamage < InvincibilityTime)
        {
            return;
        }

        CurrentHP -= Damage;
        if (hurtSounds.Count > 0)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Count - 1)]);
        }
        CurrentHP = Mathf.Max(CurrentHP, 0f);
        TimeSinceDamage = 0f;

        if (CurrentHP <= 0f)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().PlayOneShot(deathSound);
            gameObject.SendMessage("Kill", deathSound.length);
        }
    }
}
