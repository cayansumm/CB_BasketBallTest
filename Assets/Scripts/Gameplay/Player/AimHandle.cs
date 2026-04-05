using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BasketBallTest.Gameplay.Player.Controls
{
    public class AimHandle : MonoBehaviour
    {
        public delegate void StateChange(bool isAiming);
        public event StateChange OnStateChange;

        public delegate void AimDirectionChange(Vector3 aimDirection);
        public event AimDirectionChange OnAimDirectionChange;

        [SerializeField]
        private Transform character;
        [SerializeField]
        private Transform aimPivot;
        [SerializeField]
        private Transform defaultAimTarget;
        [SerializeField]
        private float sensitivity;

        private float localAimYOffset;
        private bool isAiming;
        private Vector3 aimDirection;

        public Vector3 AimDirection => aimDirection;
        public Transform AimPivot => aimPivot;

        public void ResetAimDirection()
        {
            localAimYOffset = GetYOffsetToAimTarget();
            aimDirection = CalculateAimDirection();
        }

        private Vector3 CalculateAimDirection()
        {
            localAimYOffset = Mathf.Clamp(localAimYOffset, -1, 1);
            return (aimPivot.forward + (aimPivot.up * localAimYOffset)).normalized;
        }

        private float GetYOffsetToAimTarget()
        {
            var aimPivotToAimTarget = defaultAimTarget.position - aimPivot.position;
            return aimPivotToAimTarget.normalized.y;
        }

        #region Input Implementation

        private void OnAim(InputValue value)
        {
            isAiming = value.Get<float>() == 1;
            ResetAimDirection();
            OnStateChange?.Invoke(isAiming);
        }

        private void OnLook(InputValue value)
        {
            if (isAiming == false)
                return;

            var inputVector = value.Get<Vector2>().normalized;
            localAimYOffset -= inputVector.y * Time.deltaTime * sensitivity;
            aimDirection = CalculateAimDirection();
            OnAimDirectionChange?.Invoke(aimDirection);
        }

        #endregion

        private void Start()
        {
            isAiming = false;
        }

        private void OnDrawGizmosSelected()
        {
            var aimStartPoint = aimPivot.transform.position;
            Gizmos.color = Color.yellow;
            var direction = CalculateAimDirection();
            Gizmos.DrawLine(aimStartPoint, aimStartPoint + (direction * 3));
        }
    }
}