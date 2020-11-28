using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class W9_HpBar : MonoBehaviour
{
    private Image playerhpGuid;
    public float currentHp;
    private float MaxHp = 100.0f;
    private W6_StatusSystem PHp;

    private void Start()
    {
        playerhpGuid = GetComponent<Image>();
        PHp = FindObjectOfType<W6_StatusSystem>();
    }

    private void Update()
    {
        currentHp = PHp.playerHp;

        while (currentHp < 100)
        {
            PHp.playerHp += 2 * Time.deltaTime;
            break;
        }

        playerhpGuid.fillAmount = currentHp / MaxHp;

    }

}
