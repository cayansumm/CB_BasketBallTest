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

        [SerializeField]
        private float spinForce = 100;

        [SerializeField]
        private bool useForward;

        private void OnEnable()
        {
            ballBody.linearVelocity = Vector3.zero;
            ballBody.angularVelocity = Vector3.zero;
            ballBody.transform.position = transform.position;
            ballBody.transform.localRotation = Quaternion.identity;
            var launchDirection = useForward ? transform.forward : ballLaunchDirection.normalized;
            ballBody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
            ballBody.AddTorque(-ballBody.transform.right * spinForce, ForceMode.Impulse);
        }

        private void OnDrawGizmosSelected()
        {
            if (ballBody != null)
            {
                var position = transform.position;

                Gizmos.color = Color.yellow;

                var launchDirection = useForward ? transform.forward : ballLaunchDirection.normalized;
                ;
                var lineEndpoint = position + launchDirection * 3f;
                Gizmos.DrawLine(position, lineEndpoint);

                var cubeSize = Vector3.one * 0.5f;
                Gizmos.DrawCube(lineEndpoint, cubeSize);
            }
        }
    }
}