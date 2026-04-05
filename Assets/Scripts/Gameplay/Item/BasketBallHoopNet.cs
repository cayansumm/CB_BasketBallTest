using System;
using System.Collections.Generic;
using UnityEngine;

namespace BasketBallTest.Gameplay.Items
{
    /// <summary>
    /// Simulate Physics of the ball getting snag in the net
    /// </summary>
    public class BasketBallHoopNet : MonoBehaviour
    {
        public delegate void ObjectEntered(GameObject objectEntered);
        public event ObjectEntered OnObjectEntered;
        
        private struct DampeningRecord
        {
            public float linear;
            public float angular;

            public DampeningRecord(float linear, float angular)
            {
                this.linear = linear;
                this.angular = angular;
            }
        }

        [SerializeField]
        private float linearDampening;
        [SerializeField]
        private float angularDampening;

        private Dictionary<Rigidbody, DampeningRecord> dampeningRecords = new Dictionary<Rigidbody, DampeningRecord>();

        private bool IsObjectGoingInFromTheAbove(GameObject objectToCheck, Rigidbody objectBody)
        {
            var toBallDirection = (objectToCheck.transform.position - transform.position).normalized;
            var toBallDotProduct = Vector3.Dot(transform.up, toBallDirection);
            Debug.Log(
                $"transform.up: {transform.up},toBallDirection {toBallDirection}, ToBallDotProduct: {toBallDotProduct}",
                gameObject);
            var isBallFromTheTop = toBallDotProduct > 0;
            if (isBallFromTheTop == false)
                return false;

            var ballNormalLinearVelocity = objectBody.linearVelocity.normalized;
            var ballVelocityDotProduct = Vector3.Dot(transform.up, ballNormalLinearVelocity);
            var isBallGoingToTheBottom = ballVelocityDotProduct < 0;
            Debug.Log($"ToBallVelocityDotProduct: {ballVelocityDotProduct}", gameObject);
            return isBallGoingToTheBottom;
        }
        
        private void Awake()
        {
            dampeningRecords = new Dictionary<Rigidbody, DampeningRecord>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var rigidbody = other.GetComponentInParent<Rigidbody>();
            if (rigidbody == null)
                return;

            if(IsObjectGoingInFromTheAbove(rigidbody.gameObject, rigidbody) == false)
                return;
            
            OnObjectEntered?.Invoke(rigidbody.gameObject);
            
            if (dampeningRecords.ContainsKey(rigidbody) == false)
            {
                dampeningRecords.Add(rigidbody, new DampeningRecord(rigidbody.linearDamping, rigidbody.angularDamping));
            }

            rigidbody.linearDamping = linearDampening;
            rigidbody.angularDamping = angularDampening;
        }

        private void OnTriggerExit(Collider other)
        {
            var rigidbody = other.GetComponentInParent<Rigidbody>();
            if (rigidbody == null)
                return;

            if (dampeningRecords.ContainsKey(rigidbody))
            {
                var recordedDampening = dampeningRecords[rigidbody];
                rigidbody.linearDamping = recordedDampening.linear;
                rigidbody.angularDamping = recordedDampening.angular;
                dampeningRecords.Remove(rigidbody);
            }
        }
    }
}