using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class W6_StatusSystem : MonoBehaviour
{
    public float hp;
    [Range(0,100)]
    public float playerHp;

    public delegate void DelegateHandleTakeDamage(GameObject damageFrom, float inDamage);
    public delegate void DelegateHandleDead();

    //Ai
    public event DelegateHandleTakeDamage OnTakeDamage;
    public event DelegateHandleDead OnDead;

    //Ai take damage
    public void TakeDamage(GameObject damageFrom, float inDamage)
    {
        if(hp > 0)
        {
            hp -= inDamage;

            if(OnTakeDamage != null)
            {
                OnTakeDamage(damageFrom, inDamage);
            }

            if(hp <= 0)
            {
                if(OnDead != null)
                {
                    OnDead();
                }
            }
        }
    }

    //Ai
    public bool IsAlive()
    {
        return hp > 0;
    }
}
