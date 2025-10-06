using LazyJedi.Components.Base;
using PlatCtrl2D.Player.Behaviours;
using PlatCtrl2D.Player.Components;
using UnityEngine;

namespace PlatCtrl2D.Player.Entity
{
    public class PlayerEntity : EntityBase
    {
        #region FIELDS

        [Header("Components")]
        [SerializeField]
        private AnimatorComponent _animator;
        [SerializeField]
        private PlayerInputComponent _playerInput;
        [SerializeField]
        private GroundCheckComponent _groundCheck;

        [Header("Behaviours")]
        [SerializeField]
        private MovementBehaviour _movementBehaviour;
        [SerializeField]
        private JumpBehaviour _jumpBehaviour;
        [SerializeField]
        private AttackBehaviour _attackBehaviour;
        [SerializeField]
        private WaitBehaviour _waitBehaviour;

        [Header("Status")]
        public bool IsWaiting;
        public bool IsMoving;
        public bool IsRunning;
        public bool IsJumping;
        public bool IsAttacking;
        public bool IsDead;
        public bool IsHurt;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            DoOnAwake();
        }

        private void OnEnable()
        {
            DoOnEnable();
        }

        private void Update()
        {
            _movementBehaviour.HandleMovement();
            _jumpBehaviour.HandleJump();
            _waitBehaviour.Wait();
        }

        private void FixedUpdate()
        {
            _groundCheck.CheckOnGround();
        }

        private void OnDisable()
        {
            DoOnDisable();
        }

        private void OnDestroy()
        {
            DoOnDestroy();
        }

        #endregion

        #region DO METHODS

        protected override void DoOnAwake()
        {
            _playerInput.Init(_movementBehaviour, _jumpBehaviour, _attackBehaviour);
            _groundCheck.Init(this, transform);
            _movementBehaviour.Init(this, transform, _groundCheck, _animator);
            _jumpBehaviour.Init(this, _groundCheck, _animator);
            _attackBehaviour.Init(this, _animator, _groundCheck);
            _waitBehaviour.Init(this, _animator);
            _playerInput.RegisterInputActions();
        }

        protected override void DoOnEnable()
        {
            _playerInput.Enable();
        }

        protected override void DoOnDisable()
        {
            _playerInput.Disable();
        }

        protected override void DoOnDestroy()
        {
            _playerInput.Destroy();
        }

        #endregion

        #region REFERENCE METHODS

        public void SetIsAttacking(int isAttacking) => _attackBehaviour.SetIsAttacking(isAttacking);

        #endregion

        #region GIZMOS

        private void OnDrawGizmos()
        {
            _groundCheck.DrawGizmos();
        }

        #endregion
    }
}