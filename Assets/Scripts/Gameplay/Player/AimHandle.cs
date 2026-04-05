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

        public Vector3 AimDirection
        {
            get
            {
                if (isAiming == false)
                {
                    ResetAimDirection();
                }

                return aimDirection;
            }
        }

        public Transform AimPivot => aimPivot;

        public void ResetAimDirection()
        {
            aimDirection = CalculateAimDirection();
        }

        private Vector3 CalculateAimDirection()
        {
            var aimPivotToAimTarget = defaultAimTarget.position - aimPivot.position;
            return aimPivotToAimTarget.normalized;
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
            
            ResetAimDirection();
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

            //To Aim Direction
            Gizmos.color = Color.yellow;
            var direction = CalculateAimDirection();
            Gizmos.DrawLine(aimStartPoint, aimStartPoint + (direction.normalized * 10));

            //To Aim Target
            Gizmos.color = Color.blue;
            var aimPivotToAimTarget = defaultAimTarget.position - aimPivot.position;
            Gizmos.DrawLine(aimStartPoint, aimStartPoint + (aimPivotToAimTarget.normalized * 10));
        }
    }
}