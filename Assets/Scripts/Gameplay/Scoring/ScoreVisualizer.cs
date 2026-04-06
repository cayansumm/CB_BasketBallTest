using UnityEngine;

namespace BasketBallTest.Gameplay.Scoring
{
    public abstract class ScoreVisualizer : MonoBehaviour
    {
        [SerializeField]
        protected ScoreTracker tracker;

        protected abstract void OnScoreChanged(int score);
        protected abstract void SyncWithTracker();

        private void OnEnable()
        {
            SyncWithTracker();
            tracker.OnScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            tracker.OnScoreChanged -= OnScoreChanged;
        }
    }
}