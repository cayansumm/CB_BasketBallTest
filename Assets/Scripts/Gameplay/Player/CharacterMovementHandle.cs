using UnityEngine;
using UnityEngine.InputSystem;

namespace BasketBallTest.Gameplay.Player.Controls
{
    public class CharacterMovementHandle : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody characterBody;
        [SerializeField, Min(0.01f)]
        private float speed = 10f;

        private Transform characterTransform;
        private Vector3 movementVector;

        private void OnMove(InputValue value)
        {
            var inputVector = value.Get<Vector2>();
            var forwardVelocity = characterTransform.forward * inputVector.y;
            var rightVelocity = characterTransform.right * inputVector.x;
            movementVector = (forwardVelocity + rightVelocity) * speed;
        }

        private void Start()
        {
            characterTransform = characterBody.transform;
        }

        private void FixedUpdate()
        {
            characterBody.linearVelocity = movementVector;
        }
    }
}