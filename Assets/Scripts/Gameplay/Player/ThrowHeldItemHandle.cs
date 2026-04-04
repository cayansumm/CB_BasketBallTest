using BasketBallTest.Gameplay.Player.Interactions;
using UnityEngine;

namespace BasketBallTest.Gameplay.Player.Controls
{
    public class ThrowHeldItemHandle : MonoBehaviour
    {
        [SerializeField]
        private PickupThrowableHandle pickupHandle;

        [SerializeField]
        private float throwForce;

        [SerializeField]
        private float spinForce;

        private void OnAttack()
        {
            if (pickupHandle.IsHoldingThrowable() == false)
                return;

            var heldThrowable = pickupHandle.HeldThrowable;
            pickupHandle.RemoveHeldThrowable();
            heldThrowable.Throw(Vector3.one * throwForce, Vector3.right * spinForce);
        }
    }
}