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
            pickupHandle.RemoveHeldThrowable();
            heldThrowable.Throw(aimHandle.AimDirection * throwForce, Vector3.right * spinForce);
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