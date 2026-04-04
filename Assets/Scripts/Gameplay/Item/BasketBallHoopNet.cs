using System;
using System.Collections.Generic;
using UnityEngine;

namespace BasketBallTest.Gameplay
{
    /// <summary>
    /// Simulate Physics of the ball getting snag in the net
    /// </summary>
    public class BasketBallHoopNet : MonoBehaviour
    {
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

        private void Awake()
        {
            dampeningRecords = new Dictionary<Rigidbody, DampeningRecord>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var rigidbody = other.GetComponentInParent<Rigidbody>();
            if (rigidbody == null)
                return;

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