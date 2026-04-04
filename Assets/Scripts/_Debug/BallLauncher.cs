using System;
using UnityEngine;

namespace BasketBallTest.Debugger
{
    public class BallLauncher : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody ballBody;

        [SerializeField]
        private Vector3 ballLaunchDirection = Vector3.one;

        [SerializeField]
        private float launchForce = 100;

        private void OnEnable()
        {
            ballBody.AddForce(ballLaunchDirection.normalized * launchForce, ForceMode.Impulse);
        }

        private void OnDrawGizmosSelected()
        {
            if (ballBody != null)
            {
                var ballPosition = ballBody.transform.position;

                Gizmos.color = Color.yellow;

                var launchDirection = ballLaunchDirection.normalized;
                var lineEndpoint = ballPosition + launchDirection * 3f;
                Gizmos.DrawLine(ballPosition, lineEndpoint);

                var cubeSize = Vector3.one * 0.5f;
                Gizmos.DrawCube(lineEndpoint, cubeSize);
            }
        }
    }
}