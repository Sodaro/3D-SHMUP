using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerShooting))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private PlayerShooting _playerShooting;

        
        private int _totalPoints;

        [SerializeField] private float _maxHealth = 100;
        private float _currentHealth;

        [SerializeField] private IntEvent _totalPointsUpdated;

        private void Awake()
	    {
            _currentHealth = _maxHealth;
            _playerMovement = GetComponent<PlayerMovement>();
            _playerShooting = GetComponent<PlayerShooting>();
            
        }

        void OnPointsAdded(int points)
		{
            _totalPoints += points;
            _totalPointsUpdated.Invoke(_totalPoints);
		}
	    // Start is called before the first frame update
	    void Start()
        {
            _playerShooting?._onPointsAdded.AddListener(OnPointsAdded);
        }

        public void ReduceHealth(float amount)
		{
            _currentHealth -= amount;
            if (_currentHealth <= 0)
                Destroy(gameObject);
        }

		private void OnTriggerEnter(Collider other)
		{
            Collectible collectible = other.GetComponent<Collectible>();
            if (ReferenceEquals(collectible, null))
                return;
            else
                OnPointsAdded(collectible.PickUpCollectible());
            Debug.Log("points added");
        }
	}
}