using System;
using UnityEngine;
using UnityEngine.LowLevelPhysics2D;
using UnityEngine.Serialization;

namespace BasketBallTest.Gameplay.Environment
{
    public class MovingHoop : MonoBehaviour
    {
        [FormerlySerializedAs("positions")]
        [SerializeField, Tooltip("Needs at least 2 positions to function properly")]
        private Vector3[] localPositions = new Vector3[2];

        [SerializeField, Min(0)]
        private int startingPositionIndex = 0;

        [SerializeField]
        private float duration;

        [SerializeField]
        private bool goingForward = true;

        private int destinationPositionIndex;
        private float lerpValue;
        private float lerpSpeed;

        private Vector3 currentLerpStartPosition;
        private Vector3 currentLerpDestinationPosition;

        private void CalculateLerpRate()
        {
            var timeTravelPerPosition = duration / localPositions.Length;
            lerpSpeed = 1 / timeTravelPerPosition;
        }

        private void UpdateNextLerpData()
        {
            var currentPositionIndex = destinationPositionIndex;
            if (destinationPositionIndex == 0 && goingForward == false)
            {
                goingForward = true;
            }
            else if (destinationPositionIndex == localPositions.Length - 1 && goingForward)
            {
                goingForward = false;
            }

            destinationPositionIndex += goingForward ? 1 : -1;
            currentLerpStartPosition = localPositions[currentPositionIndex];
            currentLerpDestinationPosition = localPositions[destinationPositionIndex];
            lerpValue = 0;
        }

        private void Start()
        {
            if (localPositions.Length < 2)
            {
                Debug.LogError("Not Enough Positions, need at least 2 positions", this);
                enabled = false;
            }

            if (startingPositionIndex > localPositions.Length - 1)
            {
                Debug.LogError("Starting Position Index exceeds listed Positions", this);
            }

            transform.localPosition = localPositions[startingPositionIndex];
            CalculateLerpRate();
            UpdateNextLerpData();
        }

        private void Update()
        {
            if (lerpValue > 1)
            {
                UpdateNextLerpData();
            }

            lerpValue += lerpSpeed * Time.deltaTime;
            transform.localPosition = Vector3.Lerp(currentLerpStartPosition, currentLerpDestinationPosition,
                lerpValue);
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                CalculateLerpRate();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < localPositions.Length - 1; i++)
            {
                Gizmos.DrawLine(localPositions[i], localPositions[i + 1]);
            }
        }
    }
}