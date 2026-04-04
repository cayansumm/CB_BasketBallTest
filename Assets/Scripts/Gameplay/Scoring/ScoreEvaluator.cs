using BasketBallTest.Gameplay.Items;
using UnityEngine;

namespace BasketBallTest.Gameplay.Scoring
{
    /// <summary>
    /// To be placed in any Trigger that can gives Player points when the ball passes through
    /// </summary>
    public class ScoreEvaluator : MonoBehaviour
    {
        public delegate void ScoreGiven(int score);

        public event ScoreGiven OnScoreGiven;

        //For Debug purposes to check if scores where evaluated;
        private int totalScoreEvaluated;

        private int EvaluateScore(Ball other)
        {
            return 0;
        }

        public void OnTriggerEnter(Collider other)
        {
            var ball = other.gameObject.GetComponentInParent<Ball>();
            if (ball == null)
                return;


            var score = EvaluateScore(ball);
            if (score > 0)
            {
                OnScoreGiven?.Invoke(score);
                totalScoreEvaluated += score;
            }
        }
    }
}