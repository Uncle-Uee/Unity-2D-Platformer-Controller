using System;
using PlatCtrl2D.Player.Components;
using PlatCtrl2D.Player.Entity;
using UnityEngine.InputSystem;

namespace PlatCtrl2D.Player.Behaviours
{
    [Serializable]
    public class AttackBehaviour
    {
        #region FIELDS

        private PlayerEntity _playerEntity;
        private AnimatorComponent _animator;
        private GroundCheckComponent _groundCheck;

        #endregion

        #region INIT METHOD

        public void Init(PlayerEntity playerEntity, AnimatorComponent animator, GroundCheckComponent groundCheck)
        {
            _playerEntity = playerEntity;
            _animator = animator;
            _groundCheck = groundCheck;
        }

        #endregion

        #region METHODS

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (_playerEntity.IsHurt || _playerEntity.IsDead || !_groundCheck.IsOnGround || _playerEntity.IsAttacking) return;
            if (context.ReadValueAsButton())
            {
                _animator.OnPunchTrigger();
            }
        }

        #endregion
    }
}