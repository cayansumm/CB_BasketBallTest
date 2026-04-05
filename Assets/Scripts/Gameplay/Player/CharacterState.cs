using System;
using UnityEngine;

namespace BasketBallTest.Gameplay
{
    public class CharacterState : MonoBehaviour
    {
        //Once the Character States to track grow in number, start refactoring this
        [SerializeField]
        private GroundChecker groundChecker;

        private bool isGrounded = true;

        public bool IsGrounded => isGrounded;

        private void OnGroundCheckerUpdate(bool groundDetected)
        {
            isGrounded = groundDetected;
        }

        private void Start()
        {
            isGrounded = true;
            groundChecker.OnCheckerUpdate += OnGroundCheckerUpdate;
        }

        private void OnDestroy()
        {
            if (groundChecker != null)
            {
                groundChecker.OnCheckerUpdate -= OnGroundCheckerUpdate;
            }
        }
    }
}