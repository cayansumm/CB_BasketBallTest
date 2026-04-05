using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BasketBallTest.Gameplay.Player.Controls
{
    public class AimHandle : MonoBehaviour
    {
        public delegate void StateChange(bool isAiming);

        public event StateChange OnAiming;

        public delegate void AimDirectionChange(Vector3 aimDirection);

        public event AimDirectionChange OnAimDirectionChange;

        [SerializeField]
        private Transform character;

        [SerializeField]
        private Transform aimPivot;

        private Vector3 localAimDirection;
        private Vector3 defaultLocalAimDirection = new Vector3(0, 1, 1);
        private bool isAiming;

        private Vector3 aimDirection;

        public Vector3 AimDirection => aimDirection;

        public void ResetAimDirection()
        {
            localAimDirection = defaultLocalAimDirection;
            aimDirection = AlignLocalAimDirectionTo(aimPivot.forward);
        }

        private Vector3 AlignLocalAimDirectionTo(Vector3 normal)
        {
            var localToPivotAlignRotation = Quaternion.FromToRotation(localAimDirection, normal);
            var alignedAimDirection = (localToPivotAlignRotation * localAimDirection);
            return alignedAimDirection.normalized;
        }

        #region Input Implementation

        private void OnAim(InputValue value)
        {
            isAiming = value.Get<float>() == 1;
            ResetAimDirection();
            OnAiming?.Invoke(isAiming);
        }

        private void OnLook(InputValue value)
        {
            if (isAiming == false)
                return;

            var inputVector = value.Get<Vector2>().normalized;
            var inputAlignedToForward = new Vector3(0, inputVector.y, inputVector.x);
            localAimDirection += inputAlignedToForward;
            aimDirection = AlignLocalAimDirectionTo(aimPivot.forward);
            OnAimDirectionChange?.Invoke(aimDirection);
        }

        #endregion

        private void Start()
        {
            localAimDirection = defaultLocalAimDirection;
            isAiming = false;
        }

        private void OnDrawGizmosSelected()
        {
            var aimStartPoint = aimPivot.transform.position;
            Gizmos.color = Color.yellow;
            var direction = AlignLocalAimDirectionTo(aimPivot.forward);
            Gizmos.DrawLine(aimStartPoint, aimStartPoint + (direction * 3));
        }
    }
}