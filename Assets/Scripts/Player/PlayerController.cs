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

        private float _currentHealth;
        private int _totalPoints;

        [SerializeField] private float _maxHealth;

        
        [SerializeField] private IntEvent _totalPointsUpdated;

        private void Awake()
	    {
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

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}