using PlatCtrl2D._Scripts.Common;
using PlatCtrl2D.Player.Behaviours;
using UnityEngine;

namespace PlatCtrl2D.Player.Components
{
    public class InteractionComponent : MonoBehaviour
    {
        #region FIELDS

        private PickupBehaviour _pickupBehaviour;
        private TalkBehaviour _talkBehaviour;

        #endregion

        #region INIT METHOD

        public void Init(PickupBehaviour pickupBehaviour, TalkBehaviour talkBehaviour)
        {
            _pickupBehaviour = pickupBehaviour;
            _talkBehaviour = talkBehaviour;
        }

        #endregion

        #region UNITY METHODS

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Item/Ground"))
            {
                _pickupBehaviour.SetPickupItem(other.gameObject, PickupType.PickUpGround);
            }
            else if (other.CompareTag("Item/Wall"))
            {
                _pickupBehaviour.SetPickupItem(other.gameObject, PickupType.PickUpWall);
            }
            if (other.CompareTag("Entity/NPC"))
            {
                _talkBehaviour.CanTalk = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_pickupBehaviour.CanPickupItem)
            {
                _pickupBehaviour.SetPickupItem(null, PickupType.None, false);
            }
            if (_talkBehaviour.CanTalk)
            {
                _talkBehaviour.CanTalk = false;
            }
        }

        #endregion
    }
}