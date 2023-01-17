using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * PlayerWeaponController keeps track of the players weapons and it's related inputs.
 * 
 * This class checks inputs for the following actions:
 *  - Attacking
 *  - Swapping weapons
 * 
 * TO DO:
 *  [ ] Better weapon collection functions
 */

public class PlayerWeaponController : MonoBehaviour
{
    private InputController inputController;

    private WeaponController[] weaponControllers = { null, null };
    private int selectedWeapon;


    private void Start()
    {
        inputController = GetComponent<InputController>();
    }

    private void Update()
    {
        CheckForAttackInput();
        CheckForWeaponSwapInput();
    }

    private void CheckForAttackInput()
    {
        if (inputController.playerInput.RetrieveAttackInput())
        {
            if (weaponControllers[selectedWeapon] == null)
            {
                print("PlayerWeaponController: selected weaponController is null");
                return;
            }
            weaponControllers[selectedWeapon].Attack();
        }
    }

    private void CheckForWeaponSwapInput()
    {
        if (inputController.playerInput.RetrieveNextWeaponInput())
        {
            SelectWeapon(selectedWeapon + 1);
        }

        if (inputController.playerInput.RetrievePreviousWeaponInput())
        {
            SelectWeapon(selectedWeapon - 1);
        }
    }

    private void SelectWeapon(int weaponIndex)
    {
        if (weaponIndex > weaponControllers.Length)
        {
            weaponIndex = 0;
        }
        if (weaponIndex < 0)
        {
            weaponIndex = weaponControllers.Length;
        }
        selectedWeapon = weaponIndex;
    }

    public void CollectWeapon(WeaponController weapon)
    {
        for (int i = 0; i < weaponControllers.Length; i++)
        {
            if (weaponControllers[i] == null)
            {
                weaponControllers[i] = weapon;
                return;
            }
        }
        print("PlayerWeaponController: No more space in weaponControllers.");
    }
}
