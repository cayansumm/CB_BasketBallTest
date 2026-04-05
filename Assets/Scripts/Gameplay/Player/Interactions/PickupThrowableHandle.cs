using System;
using BasketBallTest.Gameplay.Items;
using UnityEngine;

namespace BasketBallTest.Gameplay.Player.Interactions
{
    public class PickupThrowableHandle : MonoBehaviour
    {
        public event Action NewItemPickedup;

        [SerializeField]
        private ThrowableDetector detector;

        [SerializeField]
        private Transform heldThrowableParent;

        private IThrowable heldThrowable = null;

        public IThrowable HeldThrowable => heldThrowable;

        public void SetHeldThrowable(IThrowable throwable)
        {
            if (IsHoldingThrowable())
            {
                RemoveHeldThrowable();
            }

            throwable.transform.SetParent(heldThrowableParent);
            throwable.transform.localPosition = Vector3.zero;
            throwable.transform.localRotation = Quaternion.identity;
            throwable.ResetState();
            throwable.SetStateToHeld();
            heldThrowable = throwable;
            NewItemPickedup?.Invoke();
        }

        public bool IsHoldingThrowable() => heldThrowable != null;

        public void RemoveHeldThrowable()
        {
            if (IsHoldingThrowable() == false)
                return;

            heldThrowable.transform.parent = null;
            heldThrowable = null;
        }

        private void OnThrowableDetected(IThrowable throwableitem)
        {
            if (IsHoldingThrowable())
                return;
            SetHeldThrowable(throwableitem);
        }

        private void Start()
        {
            detector.OnThrowableDetected += OnThrowableDetected;
            detector.enabled = IsHoldingThrowable() == false;
        }

        private void OnDestroy()
        {
            if (detector != null)
            {
                detector.OnThrowableDetected -= OnThrowableDetected;
            }
        }
    }
}