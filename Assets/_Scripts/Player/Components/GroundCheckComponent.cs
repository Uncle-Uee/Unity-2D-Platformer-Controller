using System;
using UnityEngine;

namespace PlatCtrl2D.Player.Components
{
    [Serializable]
    public class GroundCheckComponent
    {
        #region FIELDS

        [Header("Circle Cast Settings")]
        [SerializeField]
        private float _radius = 0.2f;
        [SerializeField]
        private Vector3 _colliderOffset;

        [Header("Layer Masks")]
        [SerializeField]
        private LayerMask _groundLayer;

        [Header("State")]
        [SerializeField]
        private bool _onGround;

        private Transform _transform;

        #endregion

        #region PROPERTIES

        public bool IsOnGround => _onGround;

        #endregion

        #region INIT METHOD

        public void Init(Transform transform)
        {
            _transform = transform;
        }

        #endregion

        #region METHODS

        public void CheckOnGround()
        {
            _onGround = Physics2D.OverlapCircle(_transform.position + _colliderOffset, _radius, _groundLayer);
        }


        public void DrawGizmos()
        {
            if (!_transform)
            {
                return;
            }

            Gizmos.DrawWireSphere(_transform.position + _colliderOffset, _radius);
        }

        #endregion
    }
}