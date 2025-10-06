using System;
using PlatCtrl2D.Player.Components;
using PlatCtrl2D.Player.Entity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlatCtrl2D.Player.Behaviours
{
    [Serializable]
    public class MovementBehaviour
    {
        #region FIELDS

        [Header("Required Components")]
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [Header("Ground Movement Settings")]
        [SerializeField]
        private float _walkSpeed = 5f;
        [SerializeField]
        private float _runSpeed = 7.5f;

        [Header("Air Movement Settings")]
        [SerializeField]
        [Tooltip("The rate at which the player decelerates in the air when no input is given.")]
        private float _airDeceleration = 2f;
        [SerializeField]
        private float _airSpeed = 3.5f;
        [SerializeField]
        private float _runningAirSpeed = 5.5f;

        private PlayerEntity _playerEntity;
        private AnimatorComponent _animator;
        private GroundCheckComponent _groundCheck;
        private Transform _transform;
        private Vector2 _velocity;
        private bool _wasRunningOnJump;
        private bool _wasOnGround;
        private float targetXVelocity;
        private float XDirection;

        #endregion

        #region INIT METHODS

        public void Init(PlayerEntity playerEntity, Transform transform, GroundCheckComponent groundCheck, AnimatorComponent animator)
        {
            _playerEntity = playerEntity;
            _transform = transform;
            _groundCheck = groundCheck;
            _animator = animator;
        }

        #endregion

        #region METHODS

        public void HandleMovement()
        {
            // If player is attacking, don't move
            if (_playerEntity.IsAttacking)
            {
                _rigidbody.linearVelocityX = 0f;
                return;
            }

            ChangeFacingDirection();

            // Check if the Player was Sprinting before Jumping
            if (_wasOnGround && !_groundCheck.IsOnGround)
            {
                _wasRunningOnJump = _playerEntity.IsRunning;
            }

            _wasOnGround = _groundCheck.IsOnGround;

            // Calculate Ground Velocity
            if (_groundCheck.IsOnGround)
            {
                targetXVelocity = (_playerEntity.IsRunning ? _runSpeed : _walkSpeed) * XDirection;
            }
            else
            {
                // Calculate Air Velocity
                float currentAirSpeed = _wasRunningOnJump ? _runningAirSpeed : _airSpeed;
                targetXVelocity = currentAirSpeed * XDirection;

                // Preserve some momentum when XDirection is 0
                if (Mathf.Approximately(XDirection, 0f))
                {
                    // Can switch Lerp to MoveTowards if we want to preserve momentum
                    targetXVelocity = Mathf.Lerp(_rigidbody.linearVelocityX, 0f, Time.deltaTime * _airDeceleration);
                }
            }

            _rigidbody.linearVelocityX = targetXVelocity;

            if (_groundCheck.IsOnGround && !_wasOnGround)
            {
                // Reset WasRunningOnJump after the Player lands
                _wasRunningOnJump = false;
            }

            _animator.SetIsRunning(_playerEntity.IsMoving && _playerEntity.IsRunning);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (_playerEntity.IsDead || _playerEntity.IsHurt)
            {
                return;
            }

            XDirection = context.ReadValue<Vector2>().x;
            _playerEntity.IsMoving = XDirection != 0f;
            _animator.SetIsMoving(_playerEntity.IsMoving = XDirection != 0);
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            if (_playerEntity.IsDead || _playerEntity.IsHurt)
                return;

            if (context.performed)
            {
                _playerEntity.IsRunning = true;
            }

            if (context.canceled)
            {
                _playerEntity.IsRunning = false;
            }
        }

        #endregion

        #region HELPER METHODS

        private void ChangeFacingDirection()
        {
            if (Mathf.Approximately(XDirection, 0f)) return;
            _transform.rotation = Quaternion.Euler(0f, XDirection > 0 ? 0f : 180f, 0f);
        }

        #endregion
    }
}