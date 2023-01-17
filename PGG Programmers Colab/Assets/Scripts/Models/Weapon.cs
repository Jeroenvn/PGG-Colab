using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Weapon defines the identity of a weapon.
 * 
 * This class contains the following information about the weapon:
 *  - It's name.
 *  - It's sprite.
 *  - It's projectile.
 *  - It's base stats.
 *  - How attachments are applied onto these base stats
 *  - How it's attack behaves.
 *  - How it's reload behaves.
 *  
 *  Each of the points listed above can be altered to make for unique weapons.
 *  
 *  To alter the behaviour of a weapon create a class and make it inherit Weapon.
 *  Rewrite any function that contains virtual and replace virtual with override.
 *  Now the behaviour of this new weapon is exactly the same as this one except for the fuctions you've overridden.
 *  
 *  This class is used in the scene by adding it to the weapon variable in the WeaponController.
 * 
 * TO DO:
 *  [ ] Something
 */

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Object/Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string weaponName;
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private Transform projectile;

    [Header("Base stats")]
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseMagic;
    [SerializeField] private int baseClipSize;
    [SerializeField] private float baseAttackSpeed;
    [SerializeField] private float baseReloadDuration;
    [SerializeField] private float baseSpeed;

    // Calculates weapons stats after applying attachments.
    public virtual WeaponStats CalculateWeaponStatsWithAttachments(Attachment[] attachments)
    {
        WeaponStats stats = new(baseDamage, baseMagic, baseClipSize, baseAttackSpeed, baseReloadDuration, baseSpeed);
        foreach (Attachment attachment in attachments)
        {
            stats.Damage = (stats.Damage + attachment.Damage) * attachment.DamageMultiplier;
            stats.Magic = (stats.Magic + attachment.Magic) * attachment.MagicMultiplier;
            stats.AttackSpeed = (stats.AttackSpeed + attachment.AttackSpeed) * attachment.AttackSpeedMultiplier;
            stats.ReloadDuration = (stats.ReloadDuration + attachment.ReloadTime) * attachment.ReloadTimeMultiplier;
            stats.Speed = (stats.Speed + attachment.Speed) * attachment.SpeedMultiplier;
            stats.ClipSize += attachment.ClipSize;
        }
        return stats;
    }

    // Performs weapons attack.
    public virtual void Attack(Vector2 position, Vector2 direction, float velocity)
    {
        Transform instance = Instantiate(projectile, position, Quaternion.Euler(direction));
        instance.GetComponent<Rigidbody2D>().velocity = direction * velocity;
    }

    // Performs weapons reload.
    public virtual void Reload(WeaponController weaponController)
    {
        weaponController.Reload(weaponController.weaponStats.ReloadDuration);
    }
}
