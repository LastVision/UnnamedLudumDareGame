using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HP = 3f;

    public void Damage(float Damage)
    {
        HP -= Damage;
        if (HP < 0f)
        {
            gameObject.SendMessage("Kill");
        }
    }
}
