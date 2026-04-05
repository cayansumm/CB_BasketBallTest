using System;
using BasketBallTest.Gameplay.Items;
using UnityEngine;

namespace BasketBallTest.Gameplay
{
    public class ThrowableDetector : MonoBehaviour
    {
        public delegate void ThrowableDetected(IThrowable throwableItem);

        public event ThrowableDetected OnThrowableDetected;

        private void OnTriggerEnter(Collider other)
        {
            var throwableItem = other.attachedRigidbody?.GetComponent<IThrowable>() ?? null;
            if (throwableItem == null)
                return;

            Debug.Log($"Detected throwable item: {other.gameObject.name}");
            OnThrowableDetected?.Invoke(throwableItem);
        }
    }
}