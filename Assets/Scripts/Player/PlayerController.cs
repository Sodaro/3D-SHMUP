using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperClasses;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerShooting))]
    public class PlayerController : MonoBehaviour, IHealth
    {
        private PlayerMovement _playerMovement;
        private PlayerShooting _playerShooting;

        [SerializeField] AudioClip _coinSound;
        AudioSource _audioSource;
        
        private int _totalPoints;

        [SerializeField] GameObject _menu;

        [SerializeField] private float _maxHealth = 100;
        private float _currentHealth;

        bool _isDead = false;

        //[SerializeField] private IntEvent _totalPointsUpdated;

        private void Awake()
	    {
            _audioSource = GetComponent<AudioSource>();
            _currentHealth = _maxHealth;
            _playerMovement = GetComponent<PlayerMovement>();
            _playerShooting = GetComponent<PlayerShooting>();
            
            //EventManager.onPointsAdded += OnPointsAdded;
        }


        void EnableMenu()
        {
            _menu.SetActive(true);
            Time.timeScale = 0;
        }

        void DisableMenu()
		{
            _menu.SetActive(false);
            Time.timeScale = 1;
        }

	    void Start()
        {
            DisableMenu();
        }
        void Update()
		{
            if (Input.GetButtonDown("Menu") && !_isDead)
			{
                if (_menu.activeInHierarchy)
				{
                    DisableMenu();
                }
                else
				{
                    EnableMenu();
                }
			}
		}

		private void OnTriggerEnter(Collider other)
		{
            Collectible collectible = other.GetComponent<Collectible>();
            if (ReferenceEquals(collectible, null))
			{
                return;
            }
            else
			{
                //OnPointsAdded(collectible.PickUpCollectible());
                collectible.PickUpCollectible();
                _audioSource.PlayOneShot(_coinSound);
                //Debug.Log("points added");
            }

        }

		private void OnCollisionEnter(Collision collision)
		{
            //Show GameOver Screen
            if (collision.gameObject.layer == 0)
			{
                //TODO: StartCoroutine and fade before setting dead to true, play explosion sound etc

                _isDead = true;
                EnableMenu();
			}
		}

		public void TakeDamage(float amount)
		{
            _currentHealth -= amount;
            if (_currentHealth <= 0)
                Destroy(gameObject);
        }

		public void HealDamage(float amount)
		{
            _currentHealth += amount;
        }
	}
}