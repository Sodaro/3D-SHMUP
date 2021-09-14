using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MyUtilities;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GameObject _rocketLauncher;
        [SerializeField] private GameObject _plasmaGun;
        private IWeapon[] _weapons;
        private int _currentWeaponIndex = 0;
        //internal IntEvent _onPointsAdded;

        [SerializeField] AudioClip _powerupSound;
        AudioSource _audioSource;

        private Dictionary<int, Coroutine> _modifierCoroutines;

        public IEnumerator ActivateModifier(float value, float time, WeaponModifierType weaponModifier)
		{

            foreach (var weapon in _weapons)
                weapon?.ApplyModifier(value, weaponModifier);

            yield return new WaitForSeconds(time);

            foreach (var weapon in _weapons)
                weapon?.RemoveModifier(weaponModifier);
        }

        public void StepWeapon(float direction) 
        {
      //      if (_weapons[_currentWeaponIndex].IsShooting)
		    //{
      //          _weapons[_currentWeaponIndex].StopShooting();
		    //}
      //      if (direction > 0)
      //          _currentWeaponIndex = (_currentWeaponIndex + 1) % _weapons.Length;
      //      else
      //          _currentWeaponIndex = (_currentWeaponIndex + _weapons.Length - 1) % _weapons.Length;
        }

		private void Awake()
        {
            _weapons = new IWeapon[2] { _plasmaGun.GetComponent<IWeapon>(), _rocketLauncher.GetComponent<IWeapon>() };
            _audioSource = GetComponent<AudioSource>();
            _modifierCoroutines = new Dictionary<int, Coroutine>();
       //     SetupWeapons();

       //     void SetupWeapons()
       //     {
       //         Transform weaponsTransform = transform.Find("Weapons");
       //         _weapons = new IWeapon[weaponsTransform.childCount];

			    //for (int i = 0; i < weaponsTransform.childCount; i++)
			    //{
       //             _weapons[i] = weaponsTransform.GetChild(i).GetComponent<IWeapon>();
			    //}
       //     }
        }

        public void OnFireRocket(InputAction.CallbackContext context)
		{
			Debug.Log($"shoot rockets!");
            if (context.started)
                _weapons[1].StartShooting();
            else if (context.canceled)
                _weapons[1].StopShooting();
		}

        public void OnFire(InputAction.CallbackContext context)
		{
            if (context.started)
                _weapons[0].StartShooting();
            else if (context.canceled)
                _weapons[0].StopShooting();
        }


		private void OnTriggerEnter(Collider other)
		{
            Powerup powerup = other.GetComponent<Powerup>();
            if (ReferenceEquals(powerup, null))
                return;
            else
            {
                Debug.Log("powerup picked up");
                float value = powerup.WeaponModifierValue;
                float duration = powerup.WeaponModifierDuration;
                WeaponModifierType type = powerup.WeaponModifierType;
                int key = (int)type;
                _modifierCoroutines[key] = StartCoroutine(ActivateModifier(value, duration, type));
                _audioSource.PlayOneShot(_powerupSound);
                Destroy(other.gameObject);
            }
        }
	}
}