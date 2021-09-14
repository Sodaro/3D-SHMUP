using UnityEngine;
using MyUtilities;

public class Powerup : MonoBehaviour
{
    [SerializeField] WeaponModifierType _modifierType;
    [SerializeField] float _modifierValue;
    [SerializeField] float _modifierDuration;

    public WeaponModifierType WeaponModifierType => _modifierType;
    public float WeaponModifierValue => _modifierValue;
    public float WeaponModifierDuration => _modifierDuration;
}
