using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        private IWeapon[] _weapons;
        private int _currentWeaponIndex = 0;
        internal IntEvent _onPointsAdded;

        public void StepWeapon(float direction) 
        {
            if (_weapons[_currentWeaponIndex].IsShooting)
		    {
                _weapons[_currentWeaponIndex].StopShooting();
		    }
            if (direction > 0)
                _currentWeaponIndex = (_currentWeaponIndex + 1) % _weapons.Length;
            else
                _currentWeaponIndex = (_currentWeaponIndex + _weapons.Length - 1) % _weapons.Length;
        }

	    private void Awake()
        {
            SetupWeapons();

            void SetupWeapons()
            {
                Transform weaponsTransform = transform.Find("Weapons");
                _weapons = new IWeapon[weaponsTransform.childCount];
                //_weapons = weaponsTransform.GetComponentsInChildren<IWeapon>();
			    for (int i = 0; i < weaponsTransform.childCount; i++)
			    {
                    _weapons[i] = weaponsTransform.GetChild(i).GetComponent<IWeapon>();
			    }
            }
            _onPointsAdded = new IntEvent();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
		    {
                _weapons[_currentWeaponIndex].StartShooting(OnFireHit);
                //todo start shooting
		    }
            else if (Input.GetButtonUp("Fire1"))
		    {
                _weapons[_currentWeaponIndex].StopShooting();
                //Todo stop shooting
		    }

            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (!Mathf.Approximately(scrollWheel, 0f)) 
            {
                StepWeapon(scrollWheel);
            }
        }

	    private void OnFireHit(int points)
	    {
            //_totalPoints += points;
            _onPointsAdded.Invoke(points);
        }
    }
}