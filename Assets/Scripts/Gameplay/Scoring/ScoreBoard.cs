using TMPro;
using UnityEngine;

namespace BasketBallTest.Gameplay.Scoring
{
    public class ScoreBoard : ScoreVisualizer
    {
        [SerializeField]
        private TextMeshPro scoreText;

        protected override void OnScoreChanged(int score)
        {
            scoreText.text = score.ToString();
        }

        protected override void SyncWithTracker()
        {
            scoreText.text = tracker.CurrentScore.ToString();
        }
    }
}