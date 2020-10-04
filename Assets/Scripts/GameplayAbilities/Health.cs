using System.Collections;
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
    void Start()
    {
        CurrentHP = MaxHP;
        
    }
    void Update()
    {
        TimeSinceDamage += Time.deltaTime;

        if (CurrentHP < MaxHP && CurrentHP > 0f)
        {
            CurrentHP += HPRegenPerSec * Time.deltaTime;
        }

        if (DisplayHealth)
        {
            DisplayHealth.text = string.Format("{0}/{1}", Mathf.Ceil(CurrentHP), MaxHP);
        }
    }
    public void Damage(float Damage)
    {
        if (CurrentHP < 0f)
        {
            return;
        }
        if (TimeSinceDamage < InvincibilityTime)
        {
            return;
        }

        CurrentHP -= Damage;
        CurrentHP = Mathf.Max(CurrentHP, 0f);
        TimeSinceDamage = 0f;

        if (CurrentHP <= 0f)
        {
            gameObject.SendMessage("Kill");
        }
    }
}
