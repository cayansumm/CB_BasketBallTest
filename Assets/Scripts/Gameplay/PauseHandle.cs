using UnityEngine;

namespace BasketBallTest.Gameplay.UI
{
    public class PauseHandle : MonoBehaviour
    {
        [SerializeField]
        private Canvas pauseWindow;

        [SerializeField]
        private CursorToggler cursorToggler;

        private bool isGamePaused;

        public void ResumeGame()
        {
            cursorToggler.HideCursor();
            Time.timeScale = 1;
            pauseWindow.enabled = false;
            isGamePaused = false;
        }

        public void PauseGame()
        {
            cursorToggler.ShowCursor();
            Time.timeScale = 0;
            pauseWindow.enabled = true;
            isGamePaused = true;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void OnBack()
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}