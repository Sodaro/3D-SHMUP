using MyUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	public class PlayerShooting : MonoBehaviour
    {
        /// <summary>
        /// Handle weapon shooting logic of player.
        /// </summary>
        [SerializeField] private WeaponBase _rocketLauncher;
        [SerializeField] private WeaponBase _plasmaGun;
        private WeaponBase[] _weapons;

        private WeaponBase _activeWeapon;

        private List<WeaponBase> _weaponBacklog;

        [SerializeField] AudioClip _powerupSound;
        AudioSource _audioSource;

        private Dictionary<int, Coroutine> _modifierCoroutines;

        public IEnumerator ActivateModifier(float value, float time, WeaponModifierType weaponModifier)
		{
            foreach (var weapon in _weapons)
			{
                if (weapon == null)
                    continue;
                weapon.ApplyModifier(value, weaponModifier);
            }

            yield return new WaitForSeconds(time);

            foreach (var weapon in _weapons)
			{
                if (weapon == null)
                    continue;
                weapon.RemoveModifier(weaponModifier);
            }
        }

		private void Awake()
        {
            _weapons = new WeaponBase[] { _plasmaGun, _rocketLauncher };
            _audioSource = GetComponent<AudioSource>();
            _modifierCoroutines = new Dictionary<int, Coroutine>();
            _weaponBacklog = new List<WeaponBase>();
        }

        public void OnFireRocket(InputAction.CallbackContext context)
		{
            HandleWeaponSwitch(context, _rocketLauncher);
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            HandleWeaponSwitch(context, _plasmaGun);
        }

        private void HandleWeaponSwitch(InputAction.CallbackContext context, WeaponBase weapon)
		{
            //button to shoot weapon was pressed, put active weapon on backlog until newWeapon is released or old weapon button is released
            if (context.started)
            {
                if (_activeWeapon != null && _activeWeapon != weapon)
                {
                    AddToBacklog(_activeWeapon);
                    _activeWeapon.StopShooting();
                }
                _activeWeapon = weapon;
                _activeWeapon.StartShooting();
            }
            else if (context.canceled)
            {
                if (weapon != null)
				{
                    weapon.StopShooting();
                    RemoveFromBacklog(weapon);
                }
                //the currently activeweapon was canceled, check if one is on the backlog and activate that one instead
                if (_activeWeapon == weapon)
                {
                    _activeWeapon = null;

                    WeaponBase newWeapon = GetWeaponFromBacklog();
                    if (newWeapon != null)
                    {
                        _activeWeapon = newWeapon;
                        _activeWeapon.StartShooting();
                    }
                    RemoveFromBacklog(newWeapon);
                }
            }
        }
        private void RemoveFromBacklog(WeaponBase weapon)
		{
            if (!_weaponBacklog.Contains(weapon))
                return;

            _weaponBacklog.Remove(weapon);    
        }

        private void AddToBacklog(WeaponBase weapon)
		{
            if (_weaponBacklog.Contains(weapon))
                return;

            _weaponBacklog.Add(weapon);
		}

        private WeaponBase GetWeaponFromBacklog()
        {
            if (_weaponBacklog.Count > 0)
            {
                int backIndex = _weaponBacklog.Count - 1;
                WeaponBase weapon = _weaponBacklog[backIndex];
                _weaponBacklog.RemoveAt(backIndex);
                return weapon;
            }
            return null;
        }

		private void OnTriggerEnter(Collider other)
		{
            Powerup powerup = other.GetComponent<Powerup>();
            if (powerup == null)
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