using UnityEngine;

namespace BasketBallTest.Gameplay.Items
{
    public class Ball : MonoBehaviour, IThrowable
    {
        [SerializeField]
        private Rigidbody ballBody;


        public void Throw(Vector3 linearForce, Vector3 torque)
        {
            ResetState();
            ballBody.AddForce(linearForce, ForceMode.Impulse);
            ballBody.AddTorque(torque, ForceMode.Impulse);
        }

        public void ResetState()
        {
            ballBody.linearVelocity = Vector3.zero;
            ballBody.angularVelocity = Vector3.zero;
            ballBody.isKinematic = false;
        }

        public void SetStateToHeld()
        {
            ballBody.isKinematic = true;
        }
    }
}