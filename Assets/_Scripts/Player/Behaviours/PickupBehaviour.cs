using System;
using LazyJedi.Common.Extensions;
using PlatCtrl2D._Scripts.Common;
using PlatCtrl2D.Player.Components;
using PlatCtrl2D.Player.Entity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlatCtrl2D.Player.Behaviours
{
    [Serializable]
    public class PickupBehaviour
    {
        #region FIELDS

        [Header("States")]
        public bool CanPickupItem;

        private PlayerEntity _playerEntity;
        private AnimatorComponent _animator;

        #endregion

        #region PROPERTIES

        public PickupType PickupType { get; private set; }
        public GameObject PickupItem { get; private set; }

        #endregion

        #region INIT METHOD

        public void Init(PlayerEntity playerEntity, AnimatorComponent animator)
        {
            _playerEntity = playerEntity;
            _animator = animator;
        }

        #endregion

        #region METHODS

        public void OnPickUp(InputAction.CallbackContext context)
        {
            if (CanPickupItem && context.ReadValueAsButton())
            {
                PerformPickup(PickupType);
            }
        }

        private void PerformPickup(PickupType pickupType)
        {
            if (_playerEntity.IsHurt || _playerEntity.IsDead || _playerEntity.IsAttacking ||
                _playerEntity.IsMoving || _playerEntity.IsPickingUp)
            {
                return;
            }

            switch (pickupType)
            {
                case PickupType.PickUpGround:
                    _animator.OnPickupGroundTrigger();
                    break;
                case PickupType.PickUpWall:
                    _animator.OnPickupWallTrigger();
                    break;
            }
        }

        public void SetPickupItem(GameObject item, PickupType pickUpGround, bool canPickupItem = true)
        {
            PickupType = pickUpGround;
            PickupItem = item;
            CanPickupItem = canPickupItem;
        }

        public void OnPickUpComplete()
        {
            if (PickupItem is null)
            {
                return;
            }

            CanPickupItem = false;
            PickupItem.Destroy();
        }

        #endregion
    }
}