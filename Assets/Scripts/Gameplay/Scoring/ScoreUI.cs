using TMPro;
using UnityEngine;

namespace BasketBallTest.Gameplay.Scoring
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField]
        private ScoreTracker tracker;

        [SerializeField]
        private TextMeshProUGUI scoreText;

        private void OnScoreChanged(int score)
        {
            scoreText.text = score.ToString();
        }

        private void OnEnable()
        {
            scoreText.text = tracker.CurrentScore.ToString();
            tracker.OnScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            tracker.OnScoreChanged -= OnScoreChanged;
        }
    }
}