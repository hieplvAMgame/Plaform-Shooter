using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "SO/Create Character Data")]
public class PlayerData : ScriptableObject
{
    public Sprite icon;
    [Space(10)]
    public int maxHP;
    public float moveSpeed;
    public float jumpForce;
    [Space()]
    public int idStartWeapon;


    private int currentHP;
    public int CurrentHP
    {
        get => currentHP;

        set
        {
            currentHP = value <= 0 ? 0 : Mathf.Min(value, maxHP);
            if (value <= 0)
                onPlayerDie?.Invoke();
        }
    }
    Action onPlayerDie = null;

    public void InitPlayer(Action onPlayerDie = null)
    {
        currentHP = maxHP;
        this.onPlayerDie = onPlayerDie;
    }

    public void HealPlayer(bool isFull = false, int value = 0)
    {
        if(isFull)
        {
            currentHP = maxHP;
            return;
        }
        currentHP += value;
    }
}
