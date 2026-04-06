using UnityEngine;
using UnityEngine.InputSystem;

namespace BasketBallTest.Gameplay.Player.Controls
{
    public class
    
    public class CharacterMovementHandle : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody characterBody;

        [SerializeField, Min(0.01f)]
        private float speed = 10f;

        private Transform characterTransform;
        private Vector3 movementVector;

        private Vector2 inputVector;

        private void OnMove(InputValue value)
        {
            inputVector = value.Get<Vector2>();
        }

        private void Start()
        {
            characterTransform = characterBody.transform;
        }

        private void FixedUpdate()
        {
            //Preserve Character's Y Velocity
            var currentYVelcocity = characterBody.linearVelocity.y;

            var forwardVelocity = characterTransform.forward * inputVector.y;
            var rightVelocity = characterTransform.right * inputVector.x;
            movementVector = (forwardVelocity + rightVelocity) * speed;
            characterBody.linearVelocity = new Vector3(movementVector.x, currentYVelcocity, movementVector.z);
        }
    }
}