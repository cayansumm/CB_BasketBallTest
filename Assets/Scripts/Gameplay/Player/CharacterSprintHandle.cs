using UnityEngine;
using UnityEngine.InputSystem;

namespace BasketBallTest.Gameplay.Player.Controls
{
    public class CharacterSprintHandle : MonoBehaviour
    {
        [SerializeField]
        private CharacterMovementHandle movementHandle;
        [SerializeField]
        private GroundChecker groundChecker;

        [SerializeField]
        private float sprintMultiplier = 1.5f;

        private bool isSprintInputHeld;
        
        private void OnGroundCheckUpdate(bool grounddetected)
        {
            if (grounddetected)
            {
                if (isSprintInputHeld)
                {
                    movementHandle.SetSpeedMultiplier(sprintMultiplier);
                }
            }
            else
            {
                movementHandle.SetSpeedMultiplier(movementHandle.DefaultSpeedMultiplier);
            }
        }
        
        private void OnSprint(InputValue value)
        {
            isSprintInputHeld = value.isPressed;
            var multiplier = isSprintInputHeld ? sprintMultiplier : movementHandle.DefaultSpeedMultiplier;
            movementHandle.SetSpeedMultiplier(multiplier);
        }

        private void OnEnable()
        {
            groundChecker.OnCheckerUpdate += OnGroundCheckUpdate;
        }

        private void OnDisable()
        {
            groundChecker.OnCheckerUpdate -= OnGroundCheckUpdate;
        }

    }
}