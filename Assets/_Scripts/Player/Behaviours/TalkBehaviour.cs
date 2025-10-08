using System;
using PlatCtrl2D.Player.Components;
using PlatCtrl2D.Player.Entity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlatCtrl2D
{
    [Serializable]
    public class TalkBehaviour
    {
        #region FIELDS

        [Header("States")]
        public bool CanTalk;

        private AnimatorComponent _animator;
        private PlayerEntity _playerEntity;

        #endregion

        #region INIT METHOD

        public void Init(PlayerEntity playerEntity, AnimatorComponent animator)
        {
            _playerEntity = playerEntity;
            _animator = animator;
        }

        #endregion

        #region METHODS

        public void OnTalk(InputAction.CallbackContext context)
        {
            if (CanTalk && context.ReadValueAsButton())
            {
                PerformTalk();
            }
        }

        private void PerformTalk()
        {
            if (_playerEntity.IsHurt || _playerEntity.IsDead || _playerEntity.IsAttacking ||
                _playerEntity.IsMoving || _playerEntity.IsPickingUp)
            {
                return;
            }

            _animator.OnTalkTrigger();
        }

        public void OnTalkComplete()
        {
            CanTalk = false;
        }

        #endregion
    }
}