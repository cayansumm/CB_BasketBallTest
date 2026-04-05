using System;
using UnityEngine;

namespace BasketBallTest.Gameplay
{
    public class GroundChecker : MonoBehaviour
    {
        public delegate void CheckerUpdate(bool groundDetected);

        public CheckerUpdate OnCheckerUpdate;

        [SerializeField]
        private float normalTolerance;

        private GameObject currentGround;

        private void OnCollisionEnter(Collision collision)
        {
            var contact = collision.GetContact(0);
            var collisionNormal = contact.normal;
            if (Vector3.Dot(Vector3.up, collisionNormal) > 1 - normalTolerance)
            {
                currentGround = collision.gameObject;
                OnCheckerUpdate?.Invoke(true);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (currentGround == other.gameObject)
            {
                currentGround = null;
                OnCheckerUpdate?.Invoke(false);
            }
        }
    }
}