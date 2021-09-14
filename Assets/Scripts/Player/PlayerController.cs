using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MyUtilities;
using TMPro;
using System;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerShooting))]
    public class PlayerController : MonoBehaviour, IHealth
    {

        private bool _moveTutorialComplete = false;

        [SerializeField] CameraMover _cameraMover;

        PlayerInputActions _inputActions;
        private PlayerInput _playerInput;
        [SerializeField] private TMP_Text _hintsText;
        private PlayerMovement _playerMovement;
        private PlayerShooting _playerShooting;

        [SerializeField] Healthbar _healthbar;
        [SerializeField] AudioClip _coinSound;
        AudioSource _audioSource;
        
        private int _totalPoints;

        [SerializeField] GameObject _menu;

        [SerializeField] private float _maxHealth = 100;
        private float _currentHealth;

        bool _isDead = false;


        private const string MOVE_HELP = "Move with {move}";
        private const string SHOOT_HELP = "Shoot with {shoot}";
        //private const string PAUSE_HELP = "Pause the game with {pause}";

        //[SerializeField] private IntEvent _totalPointsUpdated;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _currentHealth = _maxHealth;
            _playerMovement = GetComponent<PlayerMovement>();
            _playerShooting = GetComponent<PlayerShooting>();

            _playerInput = GetComponent<PlayerInput>();

            _inputActions = new PlayerInputActions();



			_inputActions.Player.Fire.canceled   += _playerShooting.OnFire;
			_inputActions.Player.Fire.started    += _playerShooting.OnFire;

			_inputActions.Player.Move.canceled   += _playerMovement.OnMove;
			_inputActions.Player.Move.performed  += _playerMovement.OnMove;


			_inputActions.Player.Rocket.canceled += _playerShooting.OnFireRocket;
			_inputActions.Player.Rocket.started  += _playerShooting.OnFireRocket;

            _inputActions.Player.Move.performed  += OnMove;
            _inputActions.Player.Fire.started    += OnFire;
            _inputActions.Player.Pause.performed += OnPause;


            string controlScheme = _playerInput.currentControlScheme;

            string bindingText = _inputActions.Player.Move.GetBindingDisplayString(group: controlScheme);

            string[] words = bindingText.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries);

            string controlsText = "";
			foreach (string word in words)
			{
                string trimmedWord = word.Trim();
                if (trimmedWord.Contains("/"))
                    continue;

                if (trimmedWord.Length > 1)
                    controlsText += Environment.NewLine;

                controlsText += trimmedWord;
            }
            _hintsText.text = MOVE_HELP.Replace("{move}", "\n"+controlsText);

            //for (int i = 0; i < )

            //_hintsText.text = text;
            //_playerInput = GetComponent<PlayerInput>();

            //EventManager.onPointsAdded += OnPointsAdded;
        }
        void OnEnable()
		{
            _inputActions.Player.Move.Enable();
            _inputActions.Player.Fire.Enable();
            _inputActions.Player.Pause.Enable();
            _inputActions.Player.Rocket.Enable();

        }
        void OnFire(InputAction.CallbackContext context)
		{
            //Debug.Log($"{_inputActions.controlSchemes}");
            // int bindingIndex = _inputActions.Player.Pause.GetBindingIndex(InputBinding.MaskByGroup("Gamepad"));
            if (!_moveTutorialComplete)
                return;
            _hintsText.gameObject.SetActive(false);

            string controlScheme = _playerInput.currentControlScheme;
            //_hintsText.text = PAUSE_HELP.Replace("{pause}", _inputActions.Player.Pause.GetBindingDisplayString(group: controlScheme));
            _inputActions.Player.Fire.performed -= OnFire;

            EventManager.RaiseOnTutorialFinish();
        }

        void OnMove(InputAction.CallbackContext context)
		{
            _moveTutorialComplete = true;
            //int bindingIndex = _inputActions.Player.Fire.GetBindingIndex(InputBinding.MaskByGroup("Gamepad"));
            string controlScheme = _playerInput.currentControlScheme;
            _hintsText.text = SHOOT_HELP.Replace("{shoot}", _inputActions.Player.Fire.GetBindingDisplayString(group: controlScheme));
            _inputActions.Player.Move.performed -= OnMove;
        }

        public void OnPause(InputAction.CallbackContext context)
		{
            if (_isDead)
                return;

            //toggle menu
            EventManager.RaiseOnGamePause(!_menu.activeInHierarchy);
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
                EventManager.RaiseOnGameOver();
                //EnableMenu();
			}
		}

		public void TakeDamage(float amount)
		{
            _currentHealth -= amount;
            _healthbar.UpdateHealthFill(_currentHealth, _maxHealth);
            if (_currentHealth <= 0)
            {
                _isDead = true;
                EventManager.RaiseOnGameOver();
                //EnableMenu();
            }
        }

		public void HealDamage(float amount)
		{
            _currentHealth += amount;
        }
	}
}