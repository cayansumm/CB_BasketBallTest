using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace BasketBallTest.Gameplay.Player.Controls
{
    public class LookAroundHandle : MonoBehaviour
    {
        [SerializeField]
        private Transform character;

        [SerializeField]
        private Transform cameraPivot;

        [SerializeField, Min(0)]
        private float sensitivity = 0.001f;

        [SerializeField]
        private float minCameraPivotPitch = -90;

        [SerializeField]
        private float maxCameraPivotPitch = 90;

        //Is Use to make conditions make more natural than having to consider Unity's normalized Rotation
        private float currentCameraPivotPitch;

        private void OnLook(InputValue value)
        {
            var inputValue = value.Get<Vector2>();

            character.transform.Rotate(Vector3.up, inputValue.x * sensitivity * Time.deltaTime);

            UpdateCameraPitch(inputValue.y);
        }

        private void UpdateCameraPitch(float inputValue)
        {
            var pitchDelta = inputValue * sensitivity * Time.deltaTime;
            currentCameraPivotPitch += pitchDelta;

            if (currentCameraPivotPitch < minCameraPivotPitch || currentCameraPivotPitch > maxCameraPivotPitch)
            {
                currentCameraPivotPitch =
                    Mathf.Clamp(currentCameraPivotPitch, minCameraPivotPitch, maxCameraPivotPitch);
            }
            cameraPivot.localEulerAngles = new Vector3(currentCameraPivotPitch, 0, 0);
        }

        private void Start()
        {
            currentCameraPivotPitch = cameraPivot.localEulerAngles.x;
        }

        private void Reset()
        {
            cameraPivot.localRotation = Quaternion.identity;
        }
    }
}