using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * WeaponController keeps track of the data of the weapon type object it is attached to.
 * 
 * This class contains the following data about it's weapon type object:
 *  - which weapon it is.
 *  - what it's attachments are.
 *  - what it's stats are after accounting for attachments.
 *  - what the state of the weapon is (ENABLED, DISABLED, DELAYED, RELOADING).
 *  
 * Altough this class contains basic weapon functions such as Attack and Reload.
 * They do not actually perform the actions.
 * They call upon the weapons functions for excecuting these actions.
 * This is done so that weapons can have unique actions in these functions.
 * Instead they collect the necessary data to pass on to the weapons functions.
 * 
 * TO DO:
 *  [ ] Make the size of attachments randomized.
 *  [ ] Add some random attachments on generation.
 *  [ ] Add a function for disabling the weapon.
 *  [ ] Display weapon sprite.
 */

public class WeaponController : MonoBehaviour
{
    private Camera cam;

    private Weapon weapon;
    public WeaponStats weaponStats;
    private WeaponState weaponState;
    [SerializeField] private Attachment[] attachments = { null, null, null };

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    // If weapon state is enabled and clip is not empty attack.
    public void Attack()
    {
        if (weaponState != WeaponState.ENABLED)
        {
            print("WeaponController: Weaponstate is not ENABLED");
            return;
        }

        if (weaponStats.Clip <= 0)
        {
            print("WeaponController: Clip is empty");
            weapon.Reload(this);
            return;
        }

        Vector2 attackDirection = GetMouseDirectionReletiveToTransform();
        weapon.Attack(transform.position, attackDirection, weaponStats.Speed);

        weaponState = WeaponState.DELAYED;
        StartCoroutine(DelayedWeaponStateSwitch(weaponStats.AttackSpeed, WeaponState.ENABLED));
    }

    // Returns mouse direction relative to transform
    private Vector2 GetMouseDirectionReletiveToTransform()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 attackDirection = new(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        return attackDirection.normalized;
    }

    // Starts weapon reload that takes 'duration' seconds
    public void Reload(float duration)
    {
        weaponState = WeaponState.RELOADING;
        StartCoroutine(DelayedWeaponStateSwitch(duration, WeaponState.ENABLED));
    }

    // Adds attachment then updates weaponStats
    private void AddAttachment(Attachment attachment)
    {
        // adds attachment
        weaponStats = weapon.CalculateWeaponStatsWithAttachments(attachments);
    }

    // Switches weaponState to desired state after a 'delay' seconds delay.
    IEnumerator DelayedWeaponStateSwitch(float delay, WeaponState desiredState)
    {
        yield return new WaitForSeconds(delay);

        if (weaponState == WeaponState.RELOADING && desiredState == WeaponState.ENABLED)
        {
            weaponStats.Clip = weaponStats.ClipSize;
        }

        weaponState = desiredState;
    }
}
