using System;
using UnityEngine;

namespace PlatCtrl2D.Player.Components
{
    [Serializable]
    public class AnimatorComponent
    {
        #region FIELDS

        [Header("Required Components")]
        [SerializeField]
        private Animator _animator;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");
        private static readonly int OnWait = Animator.StringToHash("OnWait");
        private static readonly int OnPunch = Animator.StringToHash("OnAttack");
        private static readonly int OnPickupGround = Animator.StringToHash("OnPickupGround");
        private static readonly int OnPickupWall = Animator.StringToHash("OnPickupWall");
        private static readonly int OnTalk = Animator.StringToHash("OnTalk");
        private static readonly int OnDamage = Animator.StringToHash("OnDamage");
        private static readonly int OnDeath = Animator.StringToHash("OnDeath");

        #endregion

        #region ANIMATION STATES

        public void SetIsMoving(bool isMoving) => _animator?.SetBool(IsMoving, isMoving);
        public void SetIsRunning(bool isRunning) => _animator?.SetBool(IsRunning, isRunning);
        public void SetIsJumping(bool isJumping) => _animator?.SetBool(IsJumping, isJumping);
        public void SetIsFalling(bool isFalling) => _animator?.SetBool(IsFalling, isFalling);

        #endregion

        #region TRIGGERS

        public void OnWaitTrigger() => _animator?.SetTrigger(OnWait);
        public void OnDamageTrigger() => _animator?.SetTrigger(OnDamage);
        public void OnDeathTrigger() => _animator?.SetTrigger(OnDeath);
        public void OnPunchTrigger() => _animator?.SetTrigger(OnPunch);
        public void OnPickupGroundTrigger() => _animator?.SetTrigger(OnPickupGround);
        public void OnPickupWallTrigger() => _animator?.SetTrigger(OnPickupWall);
        public void OnTalkTrigger() => _animator?.SetTrigger(OnTalk);

        #endregion
    }
}