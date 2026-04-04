using BasketBallTest.Gameplay.Items;
using UnityEngine;

namespace BasketBallTest.Gameplay
{
    public class ThrowableDetector : MonoBehaviour
    {
        public delegate void ThrowableDetected(IThrowable throwableItem);

        public event ThrowableDetected OnThrowableDetected;
        
        private void OnCollisionEnter(Collision collision)
        {
            var throwableItem = collision.gameObject.GetComponent<IThrowable>();
            if (throwableItem == null)
                return;

            Debug.Log($"Detected throwable item: {collision.gameObject.name}");
            OnThrowableDetected?.Invoke(throwableItem);
        }
    }
}