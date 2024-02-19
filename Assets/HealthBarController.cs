using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Damagable))]
public class HealthBarController : MonoBehaviour
{

    GameObject boss;

    public Damagable damagable;
    public Image healthBar;
    float currentHealth;
    float maxHealth;
    float healthPercentage;


    

    void Start() {
        boss = GameObject.FindWithTag("Boss");
        damagable = boss.GetComponent<Damagable>();
        maxHealth = damagable.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = damagable.Health;
        healthPercentage = currentHealth / maxHealth;

        healthBar.fillAmount = healthPercentage;

    }
}
