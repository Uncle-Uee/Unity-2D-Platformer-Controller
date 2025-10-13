using System;
using PlatCtrl2D.Player.Behaviours;

namespace PlatCtrl2D.Player.Components
{
    [Serializable]
    public class PlayerInputComponent
    {
        #region FIELDS

        private MovementBehaviour _movementBehaviour;
        private JumpBehaviour _jumpBehaviour;
        private AttackBehaviour _attackBehaviour;
        private PickupBehaviour _pickupBehaviour;
        private TalkBehaviour _talkBehaviour;

        #endregion

        #region PROPERTIES

        public PlayerInputSystem_Actions InputSystemActions { get; private set; }
        public PlayerInputSystem_Actions.PlayerActions PlayerActions { get; private set; }

        #endregion

        #region DESTRUCTOR

        public void Destroy()
        {
            UnregisterInputActions();
            InputSystemActions.Dispose();
        }

        #endregion

        #region INIT METHOD

        public void Init(
            MovementBehaviour movementBehaviour,
            JumpBehaviour jumpBehaviour,
            AttackBehaviour attackBehaviour,
            PickupBehaviour pickupBehaviour,
            TalkBehaviour talkBehaviour
        )
        {
            InputSystemActions = new PlayerInputSystem_Actions();
            PlayerActions = InputSystemActions.Player;

            _movementBehaviour = movementBehaviour;
            _jumpBehaviour = jumpBehaviour;
            _attackBehaviour = attackBehaviour;
            _pickupBehaviour = pickupBehaviour;
            _talkBehaviour = talkBehaviour;
        }

        #endregion

        #region METHODS

        public void Enable() => PlayerActions.Enable();

        public void Disable() => PlayerActions.Disable();

        public void RegisterInputActions()
        {
            PlayerActions.Move.performed += _movementBehaviour.OnMove;
            PlayerActions.Move.canceled += _movementBehaviour.OnMove;

            PlayerActions.Sprint.performed += _movementBehaviour.OnRun;
            PlayerActions.Sprint.canceled += _movementBehaviour.OnRun;

            PlayerActions.Jump.performed += _jumpBehaviour.OnJump;
            PlayerActions.Jump.canceled += _jumpBehaviour.OnJump;

            PlayerActions.Interact.started += _pickupBehaviour.OnPickUp;
            PlayerActions.Interact.canceled += _pickupBehaviour.OnPickUp;

            PlayerActions.Interact.started += _talkBehaviour.OnTalk;
            PlayerActions.Interact.canceled += _talkBehaviour.OnTalk;

            PlayerActions.Attack.performed += _attackBehaviour.OnAttack;
            PlayerActions.Attack.canceled += _attackBehaviour.OnAttack;
        }

        public void UnregisterInputActions()
        {
            PlayerActions.Move.performed -= _movementBehaviour.OnMove;
            PlayerActions.Move.canceled -= _movementBehaviour.OnMove;

            PlayerActions.Sprint.performed -= _movementBehaviour.OnRun;
            PlayerActions.Sprint.canceled -= _movementBehaviour.OnRun;

            PlayerActions.Jump.performed -= _jumpBehaviour.OnJump;
            PlayerActions.Jump.canceled -= _jumpBehaviour.OnJump;

            PlayerActions.Interact.started -= _pickupBehaviour.OnPickUp;
            PlayerActions.Interact.canceled -= _pickupBehaviour.OnPickUp;

            PlayerActions.Interact.started -= _talkBehaviour.OnTalk;
            PlayerActions.Interact.canceled -= _talkBehaviour.OnTalk;

            PlayerActions.Attack.performed -= _attackBehaviour.OnAttack;
            PlayerActions.Attack.canceled -= _attackBehaviour.OnAttack;
        }

        #endregion
    }
}