using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Player
{
	[RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerShooting))]
    public class PlayerController : MonoBehaviour, IHealth
    {
        [SerializeField] private CameraMover _cameraMover;
        [SerializeField] private TMP_Text _hintsText;
        [SerializeField] private GameObject _hintsParent;
        [SerializeField] private Healthbar _healthbar;
        [SerializeField] private AudioClip _coinSound;
        [SerializeField] private GameObject _menu;
        [SerializeField] private float _maxHealth = 100;

        private PlayerInputActions _inputActions;
        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;
        private PlayerShooting _playerShooting;
        private AudioSource _audioSource;

        private bool _moveTutorialComplete = false;
        private bool _shootTutorialComplete = false;
        private bool _isDead = false;

        private float _currentHealth;

        

        private const string MOVE_HELP = "Move using:\n{move}";

        private const string SHOOT_HELP = "Shoot your plasmagun:\n{shoot}";
        private const string ROCKET_HELP = "Rockets need 5 seconds to recharge.\nThey can be fired using\n{shoot}";

        private string currentlyDisplayedTutorial;

        private Action<InputUser, InputUserChange, InputDevice> _controllerChangedDelegate;

        private void Awake()
        {
            _currentHealth  =  _maxHealth;

            _audioSource    = GetComponent<AudioSource>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerShooting = GetComponent<PlayerShooting>();
            _playerInput    = GetComponent<PlayerInput>();
            _inputActions   = new PlayerInputActions();

            _controllerChangedDelegate = delegate { ShowTutorial(currentlyDisplayedTutorial); };

            RegisterHintCallbacks();

            ShowTutorial(MOVE_HELP);
        }

        private void ShowTutorial(string tutorialText)
        {
            if (!_hintsParent.activeInHierarchy)
                return;

            string controlScheme = _playerInput.currentControlScheme;
            if (tutorialText == SHOOT_HELP)
			{
                _hintsText.text = SHOOT_HELP.Replace("{shoot}", _inputActions.Player.Fire.GetBindingDisplayString(group: controlScheme));
                currentlyDisplayedTutorial = SHOOT_HELP;
            }
            else if (tutorialText == ROCKET_HELP)
			{
                _hintsText.text = ROCKET_HELP.Replace("{shoot}", _inputActions.Player.Rocket.GetBindingDisplayString(group: controlScheme));
                currentlyDisplayedTutorial = ROCKET_HELP;
            }
            else
			{
                //keyboard arrows are displayed weirdly so just hard coding this one.
                if (controlScheme == "Keyboard&Mouse")
                    _hintsText.text = MOVE_HELP.Replace("{move}", "WASD\nARROW KEYS");
                else
                    _hintsText.text = MOVE_HELP.Replace("{move}", _inputActions.Player.Move.GetBindingDisplayString(group: controlScheme));

                currentlyDisplayedTutorial = MOVE_HELP;

            }
            _hintsText.text = _hintsText.text.Replace(" | ", "\n");
        }

        private void RegisterHintCallbacks()
		{
            _inputActions.Player.Move.performed  += OnMove;
            _inputActions.Player.Fire.started    += OnFire;
            _inputActions.Player.Rocket.started  += OnRocket;

            
            InputUser.onChange += _controllerChangedDelegate;
        }
        private void DeregisterHintCallbacks()
        {
            _inputActions.Player.Move.performed  -= OnMove;
            _inputActions.Player.Fire.started    -= OnFire;
            _inputActions.Player.Rocket.started  -= OnRocket;

            InputUser.onChange -= _controllerChangedDelegate;
        }

        private void RegisterCallbacks()
        {
            _inputActions.Player.Fire.canceled   += _playerShooting.OnFire;
            _inputActions.Player.Fire.started    += _playerShooting.OnFire;

            _inputActions.Player.Move.canceled   += _playerMovement.OnMove;
            _inputActions.Player.Move.performed  += _playerMovement.OnMove;

            _inputActions.Player.Rocket.canceled += _playerShooting.OnFireRocket;
            _inputActions.Player.Rocket.started  += _playerShooting.OnFireRocket;


            _inputActions.Player.Pause.started += OnPause;
        }

        private void DeregisterCallbacks()
		{
            _inputActions.Player.Fire.canceled   -= _playerShooting.OnFire;
            _inputActions.Player.Fire.started    -= _playerShooting.OnFire;

            _inputActions.Player.Move.canceled   -= _playerMovement.OnMove;
            _inputActions.Player.Move.performed  -= _playerMovement.OnMove;

            _inputActions.Player.Rocket.canceled -= _playerShooting.OnFireRocket;
            _inputActions.Player.Rocket.started  -= _playerShooting.OnFireRocket;

            _inputActions.Player.Pause.started -= OnPause;
        }

        private void EnableInputs()
		{
            _inputActions.Player.Move.Enable();

            if (_moveTutorialComplete)
                _inputActions.Player.Fire.Enable();
            if (_shootTutorialComplete)
                _inputActions.Player.Rocket.Enable();

            _inputActions.Player.Pause.Enable();
        }

        private void DisableInputs()
		{
            _inputActions.Player.Move.Disable();
            _inputActions.Player.Fire.Disable();
            _inputActions.Player.Rocket.Disable();
            _inputActions.Player.Pause.Disable();
        }


        private void OnEnable()
		{
            EnableInputs();
            RegisterCallbacks();
        }
        private void OnDisable()
		{
            DisableInputs();
            DeregisterCallbacks();
            DeregisterHintCallbacks();
        }

        IEnumerator CallAfterSeconds(float seconds, Action<string> action, string tutorialText)
		{
            yield return new WaitForSeconds(seconds);
            action.Invoke(tutorialText);
		}
        private void OnFire(InputAction.CallbackContext context)
		{
            if (!_moveTutorialComplete)
                return;

            
            StartCoroutine(CallAfterSeconds(2, ShowTutorial, ROCKET_HELP));
            _inputActions.Player.Rocket.Enable();
            _inputActions.Player.Fire.started -= OnFire;
           
            EventManager.RaiseOnTutorialFinish();
        }

        private void OnMove(InputAction.CallbackContext context)
		{
            _moveTutorialComplete = true;

            StartCoroutine(CallAfterSeconds(2, ShowTutorial, SHOOT_HELP));
            _inputActions.Player.Fire.Enable(); 
            _inputActions.Player.Move.performed -= OnMove;

        }

        private void OnRocket(InputAction.CallbackContext context)
		{
            if (!_moveTutorialComplete)
                return;

            _hintsParent.SetActive(false);
            _inputActions.Player.Rocket.started -= OnRocket;
        }

        public void OnPause(InputAction.CallbackContext context)
		{
            if (_isDead)
                return;

			Debug.Log($"pause");
            //toggle menu
            EventManager.RaiseOnGamePause(!_menu.activeInHierarchy);
        }

		private void OnTriggerEnter(Collider other)
		{
            Collectible collectible = other.GetComponent<Collectible>();
            if (collectible == null)
                return;

            collectible.PickUpCollectible();
            _audioSource.PlayOneShot(_coinSound);

        }

		private void OnCollisionEnter(Collision collision)
		{
            //Show GameOver Screen
            if (collision.gameObject.layer == 0)
			{
                _isDead = true;
                _playerMovement.enabled = false;
                _playerShooting.enabled = false;
                DeregisterCallbacks();
                EventManager.RaiseOnGameOver();
			}
		}

		public void TakeDamage(float amount)
		{
            //play damage sound
            _currentHealth -= amount;
            _healthbar.UpdateHealthFill(_currentHealth, _maxHealth);
            if (_currentHealth <= 0)
            {
                _isDead = true;
                _playerMovement.enabled = false;
                _playerShooting.enabled = false;
                DeregisterCallbacks();
                EventManager.RaiseOnGameOver();
            }
        }

		public void HealDamage(float amount)
		{
            _currentHealth += amount;
        }
	}
}