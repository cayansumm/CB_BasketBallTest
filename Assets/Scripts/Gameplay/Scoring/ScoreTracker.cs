using UnityEngine;

namespace BasketBallTest.Gameplay.Scoring
{
    public class ScoreTracker : MonoBehaviour
    {
        public delegate void ScoreChanged(int score);

        public event ScoreChanged OnScoreChanged;

        [SerializeField]
        private ScoreEvaluator[] evaluators;

        private int currentScore = 0;

        public int CurrentScore => currentScore;

        private void OnScoreGiven(int score)
        {
            currentScore += score;
            OnScoreChanged?.Invoke(currentScore);
        }

        private void OnEnable()
        {
            for (int i = 0; i < evaluators.Length; i++)
            {
                evaluators[i].OnScoreGiven += OnScoreGiven;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < evaluators.Length; i++)
            {
                evaluators[i].OnScoreGiven -= OnScoreGiven;
            }
        }

        public void Reset()
        {
            currentScore = 0;
            OnScoreChanged?.Invoke(currentScore);
        }
    }
}