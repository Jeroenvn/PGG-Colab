using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float Damage;
    public float Magic;
    public float AttackSpeed;
    public float ReloadDuration;
    public float Speed;
    public int ClipSize;
    public int Clip;

    public WeaponStats(float damage, float magic, int clipSize, float attackSpeed, float reloadTime, float speed)
    {
        Damage = damage;
        Magic = magic;
        AttackSpeed = attackSpeed;
        ReloadDuration = reloadTime;
        Speed = speed;
        ClipSize = clipSize;
        Clip = clipSize;
    }
}
