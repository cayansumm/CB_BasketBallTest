using System;
using BasketBallTest.Gameplay.Player.Interactions;
using UnityEngine;

namespace BasketBallTest.Gameplay.Player.Controls
{
    public class ThrowHeldItemHandle : MonoBehaviour
    {
        [SerializeField]
        private PickupThrowableHandle pickupHandle;

        [SerializeField]
        private AimHandle aimHandle;
        [SerializeField]
        private Transform throwPoint;
        [SerializeField]
        private float throwForce;
        [SerializeField]
        private float spinForce;

        private void OnNewItemPickedup()
        {
            aimHandle.ResetAimDirection();
        }

        private void OnThrow()
        {
            if (pickupHandle.IsHoldingThrowable() == false)
                return;

            var heldThrowable = pickupHandle.HeldThrowable;
            heldThrowable.transform.position = throwPoint.position;
            pickupHandle.RemoveHeldThrowable();
            heldThrowable.Throw(aimHandle.AimDirection * throwForce, -heldThrowable.transform.forward * spinForce);
        }

        private void Start()
        {
            pickupHandle.NewItemPickedup += OnNewItemPickedup;
        }

        private void OnDestroy()
        {
            if (pickupHandle != null)
            {
                pickupHandle.NewItemPickedup -= OnNewItemPickedup;
            }
        }
    }
}