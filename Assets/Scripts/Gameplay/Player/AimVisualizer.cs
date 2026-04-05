using System;
using UnityEngine;

namespace BasketBallTest.Gameplay.Player.Controls
{
    public class AimVisualizer : MonoBehaviour
    {
        [SerializeField]
        private AimHandle handle;

        [SerializeField]
        private LineRenderer guide;

        [SerializeField]
        private float guideLength;

        private Vector3[] guidePoints;

        private void OnAimStateChange(bool isAiming)
        {
            guide.enabled = isAiming;
        }

        private void SetGuideRenderingPositions(Vector3 startPosition, Vector3 direction)
        {
            guidePoints[0] = startPosition;
            guidePoints[1] = startPosition + (direction * guideLength);
            guide.SetPositions(guidePoints);
        }

        private void Awake()
        {
            guidePoints = new Vector3[2];
            guide.enabled = false;
        }

        private void OnEnable()
        {
            handle.OnStateChange += OnAimStateChange;
        }

        private void OnDisable()
        {
            handle.OnStateChange -= OnAimStateChange;
        }

        private void LateUpdate()
        {
            SetGuideRenderingPositions(handle.AimPivot.position, handle.AimDirection);
        }

        private void OnValidate()
        {
            if (Application.isPlaying == false)
            {
                if (handle != null && guide != null)
                {
                    guidePoints = new Vector3[2];
                    SetGuideRenderingPositions(handle.AimPivot.position, handle.AimPivot.forward);
                }
            }
        }
    }
}