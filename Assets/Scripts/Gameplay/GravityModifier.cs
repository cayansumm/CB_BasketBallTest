using System;
using UnityEngine;

namespace BasketBallTest.Gameplay
{
    public class GravityModifier : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody rigidbody;

        [SerializeField]
        private float scale = 1.5f;

        private void FixedUpdate()
        {
            var gravityScale = scale - 1; //Take into Account Existing Gravity Calculation by Unity
            rigidbody.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        }
    }
}