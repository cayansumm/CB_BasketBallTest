using System;
using UnityEngine;

namespace BasketBallTest.Gameplay.Environment
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField]
        private float bounceForce;
        [SerializeField]
        private AudioSource audioSource;

        private void OnCollisionEnter(Collision collision)
        {
            var collidingRigidbody = collision.rigidbody;
            if (collidingRigidbody == null)
                return;

            var contact = collision.GetContact(0);
            var isContactFromAbove = Vector3.Dot(transform.up, contact.normal) < 0;
            if (isContactFromAbove == false)
                return;

            var isCollidingRigidbodyMovingTowardsSelf =
                Vector3.Dot(transform.up, collision.relativeVelocity.normalized) < 0;
            if (isCollidingRigidbodyMovingTowardsSelf == false)
                return;

            Debug.Log($"Launching {collidingRigidbody.gameObject.name} with {transform.up * bounceForce}");
            collidingRigidbody.AddForce(transform.up * bounceForce, ForceMode.Impulse);
            audioSource.Play();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, transform.up * 3f);
        }
    }
}