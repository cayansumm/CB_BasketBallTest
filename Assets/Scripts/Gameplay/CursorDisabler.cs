using UnityEngine;

namespace BasketBallTest.Gameplay.UI
{
    public class CursorToggler : MonoBehaviour
    {
        [SerializeField]
        private bool startAsHidden;

        public void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (startAsHidden)
            {
                HideCursor();
            }
        }
    }
}