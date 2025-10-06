using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlatCtrl2D.Player.Components;
using PlatCtrl2D.Player.Entity;

namespace PlatCtrl2D.Player.Behaviours
{
    [Serializable]
    public class JumpBehaviour
    {
        [Header("Required Components")]
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [Header("Jump Settings")]
        [SerializeField]
        [Tooltip( "The force applied to the player when jumping." )]
        private float _jumpForce = 6;
        [SerializeField]
        [Tooltip( "The time the player has before being able to jump after falling." )]
        private float _coyoteTime = 0.25f;
        [SerializeField]
        [Tooltip( "The time elapsed before the player automatically jumps again after falling." )]
        private float _jumpBufferTime = 0.1f;
        [SerializeField]
        private int _maxJumps = 2;
        [SerializeField]
        private float _fallGravityMultiplier = 2.5f;
        [SerializeField]
        private float _lowJumpGravityMultiplier = 2f;

        private PlayerEntity _playerEntity;
        private AnimatorComponent _animator;
        private GroundCheckComponent _groundCheck;

        private float _coyoteTimeCounter;
        private float _jumpBufferCounter;
        private int _jumpCount;
        private bool _isJumpingHeld;
        private float _currentYVelocity;

        #region INIT METHOD

        public void Init(PlayerEntity playerEntity, GroundCheckComponent groundCheck, AnimatorComponent animator)
        {
            _playerEntity = playerEntity;
            _groundCheck = groundCheck;
            _animator = animator;
        }

        #endregion

        #region METHODS

        public void OnJump(InputAction.CallbackContext context)
        {
            if (_playerEntity.IsDead || _playerEntity.IsHurt) return;

            if (context.performed)
            {
                _isJumpingHeld = true;
                _jumpBufferCounter = _jumpBufferTime;
            }

            if (context.canceled)
            {
                _isJumpingHeld = false;
            }
        }

        public void HandleJump()
        {
            _currentYVelocity = _rigidbody.linearVelocityY;

            if (_groundCheck.IsOnGround && Mathf.Approximately(_currentYVelocity, 0f))
            {
                _coyoteTimeCounter = _coyoteTime;
                _jumpCount = 0;
                if (_playerEntity.IsJumping)
                {
                    _playerEntity.IsJumping = false;
                    _animator.SetIsJumping(false);
                }

                _animator.SetIsFalling(false);
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }

            _jumpBufferCounter = Mathf.Max(_jumpBufferCounter - Time.deltaTime, 0f);

            if (_jumpBufferCounter > 0f)
            {
                bool canDoGroundOrCoyote = _groundCheck.IsOnGround || _coyoteTimeCounter > 0f;
                bool canDoAirJump = _jumpCount > 0 && _jumpCount < _maxJumps;

                if (canDoGroundOrCoyote || canDoAirJump)
                {
                    PerformJump();
                    _jumpBufferCounter = 0f;
                }
            }

            ApplyBetterGravity();
            JumpAnimations();
        }

        private void PerformJump()
        {
            _rigidbody.linearVelocityY = _jumpForce;
            _playerEntity.IsJumping = true;
            _animator.SetIsJumping(_playerEntity.IsJumping);
            _animator.SetIsFalling(false);
            _jumpCount++;
        }

        private void ApplyBetterGravity()
        {
            if (_rigidbody.linearVelocityY < 0)
            {
                _rigidbody.linearVelocityY += Physics2D.gravity.y * (_fallGravityMultiplier - 1) * Time.deltaTime;
            }
            else if (_rigidbody.linearVelocityY > 0 && !_isJumpingHeld)
            {
                _rigidbody.linearVelocityY += Physics2D.gravity.y * (_lowJumpGravityMultiplier - 1) * Time.deltaTime;
            }
        }

        private void JumpAnimations()
        {
            if (_groundCheck.IsOnGround) return;
            if (_currentYVelocity > 0.05f)
            {
                // Jumping
                _animator.SetIsJumping(true);
                _animator.SetIsFalling(false);
            }
            else if (_currentYVelocity < -0.05f)
            {
                // Falling
                _animator.SetIsJumping(false);
                _animator.SetIsFalling(true);
            }
        }

        #endregion
    }
}