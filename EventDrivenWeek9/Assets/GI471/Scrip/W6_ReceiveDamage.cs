using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W6_ReceiveDamage : MonoBehaviour
{
    public W6_StatusSystem statusSystem;
    public float def = 0.0f;

    //ai
    public void TakeDamage(GameObject damageFrom, float inDamage)
    {
        inDamage -= def;

        if(inDamage <= 0.0f)
        {
            inDamage = 0.0f;
        }

        statusSystem.TakeDamage(damageFrom, inDamage);
    }



}
