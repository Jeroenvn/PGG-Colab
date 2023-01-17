using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Input scriptable object", menuName = "Scriptable Object/Player input", order = 0)]
public class PlayerInput : ScriptableObject
{
    [SerializeField] private KeyCode attackButton;
    [SerializeField] private KeyCode nextWeaponButton;
    [SerializeField] private KeyCode previousWeaponButton;

    public bool RetrieveAttackInput()
    {
        return Input.GetKeyDown(attackButton);
    }

    public bool RetrieveNextWeaponInput()
    {
        return Input.GetKeyDown(nextWeaponButton);
    }

    public bool RetrievePreviousWeaponInput()
    {
        return Input.GetKeyDown(nextWeaponButton);
    }
}
