using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperClasses;

public class Powerup : MonoBehaviour
{
    [SerializeField] Enums.WeaponModifierType _modifierType;
    [SerializeField] float _modifierValue;
    [SerializeField] float _modifierDuration;

    public Enums.WeaponModifierType WeaponModifierType => _modifierType;
    public float WeaponModifierValue => _modifierValue;
    public float WeaponModifierDuration => _modifierDuration;
}
