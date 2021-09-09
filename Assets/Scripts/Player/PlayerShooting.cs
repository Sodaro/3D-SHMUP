using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HelperClasses;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        private IWeapon[] _weapons;
        private int _currentWeaponIndex = 0;
        //internal IntEvent _onPointsAdded;

        [SerializeField] AudioClip _powerupSound;
        AudioSource _audioSource;

        private Dictionary<int, Coroutine> _modifierCoroutines;

        public IEnumerator ActivateModifier(float value, float time, Enums.WeaponModifierType weaponModifier)
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
            _audioSource = GetComponent<AudioSource>();
            _modifierCoroutines = new Dictionary<int, Coroutine>();
            SetupWeapons();

            void SetupWeapons()
            {
                Transform weaponsTransform = transform.Find("Weapons");
                _weapons = new IWeapon[weaponsTransform.childCount];

			    for (int i = 0; i < weaponsTransform.childCount; i++)
			    {
                    _weapons[i] = weaponsTransform.GetChild(i).GetComponent<IWeapon>();
			    }
            }
            //_onPointsAdded = new IntEvent();
        }

        public void OnFire(InputAction.CallbackContext context)
		{
            if (context.started)
                _weapons[_currentWeaponIndex].StartShooting();
            else if (context.canceled)
                _weapons[_currentWeaponIndex].StopShooting();
        }

        // Update is called once per frame
        void Update()
        {
      //      if (Input.GetButtonDown("Fire1"))
		    //{
      //          //_weapons[_currentWeaponIndex].StartShooting(OnFireHit);
      //          _weapons[_currentWeaponIndex].StartShooting();
		    //}
      //      else if (Input.GetButtonUp("Fire1"))
		    //{
      //          _weapons[_currentWeaponIndex].StopShooting();
		    //}

      //      float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
      //      if (!Mathf.Approximately(scrollWheel, 0f)) 
      //      {
      //          StepWeapon(scrollWheel);
      //      }
        }

	    //private void OnFireHit(int points)
	    //{
     //       //_totalPoints += points;
     //       _onPointsAdded.Invoke(points);
     //   }

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
                Enums.WeaponModifierType type = powerup.WeaponModifierType;
                int key = (int)type;
                _modifierCoroutines[key] = StartCoroutine(ActivateModifier(value, duration, type));
                _audioSource.PlayOneShot(_powerupSound);
                Destroy(other.gameObject);
            }
        }
	}
}