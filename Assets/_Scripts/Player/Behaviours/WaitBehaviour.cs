using System;
using PlatCtrl2D.Player.Components;
using PlatCtrl2D.Player.Entity;
using UnityEngine;

namespace PlatCtrl2D
{
    [Serializable]
    public class WaitBehaviour
    {
        #region FIELDS

        [Header("Wait Settings")]
        [SerializeField]
        private float _waitTime = 10f;

        private PlayerEntity _playerEntity;
        private AnimatorComponent _animator;
        private float _waitTimeElapsed;

        #endregion

        #region INIT METHOD

        public void Init(PlayerEntity playerEntity, AnimatorComponent animatorComponent)
        {
            _playerEntity = playerEntity;
            _animator = animatorComponent;
        }

        #endregion

        #region METHODS

        public void Wait()
        {
            if (_playerEntity.IsDead || _playerEntity.IsHurt || _playerEntity.IsMoving || _playerEntity.IsJumping)
            {
                _waitTimeElapsed = 0f;
                _playerEntity.IsWaiting = false;
                return;
            }

            _waitTimeElapsed += Time.deltaTime;
            if (_waitTimeElapsed >= _waitTime && !_playerEntity.IsWaiting)
            {
                _animator.OnWaitTrigger();
                _playerEntity.IsWaiting = true;
            }
        }

        #endregion
    }
}