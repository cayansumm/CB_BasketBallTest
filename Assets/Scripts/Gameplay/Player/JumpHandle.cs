using System;
using UnityEngine;

namespace BasketBallTest.Gameplay.Player.Controls
{
    public class JumpHandle : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody characterBody;
        
        [SerializeField]
        private float jumpForce = 10f;

        private void OnJump()
        {
            characterBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}