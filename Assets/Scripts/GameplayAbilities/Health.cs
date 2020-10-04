using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float MaxHP = 100f;
    public float HPRegenPerSec = 0f;
    private float CurrentHP;
    public Text DisplayHealth;
    void Start()
    {
        CurrentHP = MaxHP;
        
    }
    void Update()
    {
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
        CurrentHP -= Damage;
        if (CurrentHP < 0f)
        {
            gameObject.SendMessage("Kill");
        }
    }
}
