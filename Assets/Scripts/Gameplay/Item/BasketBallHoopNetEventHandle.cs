using UnityEngine;
using UnityEngine.Events;

namespace BasketBallTest.Gameplay.Items
{
    public class BasketBallHoopNetEventHandle : MonoBehaviour
    {
        [SerializeField]
        private BasketBallHoopNet hoopNet;

        [SerializeField]
        private UnityEvent reactions;

        private void OnObjectEnteredNet(GameObject objectentered)
        {
            reactions?.Invoke();
        }
        
        private void OnEnable()
        {
            hoopNet.OnObjectEntered += OnObjectEnteredNet;
        }
        
        private void OnDisable()
        {
            hoopNet.OnObjectEntered -= OnObjectEnteredNet;
        }


    }
}